using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Core;

namespace UI.CLI
{
	class Program
	{
		static void Main(string[] args)
		{
			Program p = new Program();
			p.Run();
		}

		Server server;

		private void Run()
		{
			Console.WriteLine("Input Auction Server IP (127.0.0.1):");
			string ip = Console.ReadLine();

			if (ip == "") ip = "127.0.0.1";

			server = new Server(ip);

			Thread reader = new Thread(ReadMessages);
			reader.Start();
			Thread writer = new Thread(SendMessages);
			writer.Start();
		}

		private void ReadMessages()
		{
			string msg;
			while (Server.Active)
			{
				do
				{
					msg = server.ReadMessage();
					Thread.Sleep(10);
				} while (msg == null);

				Console.WriteLine(AHPHandler.Message(msg));
			}
		}

		private void SendMessages()
		{
			int price;
			string msg;
			while (Server.Active)
			{
				try
				{
					int.TryParse(Console.ReadLine(), out price);
					if (price > 0)
					{
						msg = AHPHandler.MakeBid(Item.ID, price);
						server.SendMessage(msg);
					}
					else Console.WriteLine("Positive numbers only please.");
				}
				catch (ArgumentNullException)
				{
					Console.WriteLine("How even? No input...");
				}
				catch (FormatException)
				{
					Console.WriteLine("Numbers, please, numbers only!");
				}
				catch (OverflowException)
				{
					Console.WriteLine("Number overflow, please be resonable!");
				}

			}
		}
	}
}
