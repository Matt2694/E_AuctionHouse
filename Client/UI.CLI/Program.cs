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

		private void Run()
		{
			Console.WriteLine("Input Auction Server IP (127.0.0.1):");
			string ip = Console.ReadLine();

			if(ip == "")
			{
				ip = "127.0.0.1";
			}

			Server server = new Server(ip);

			string msg;
			do
			{
				msg = server.ReadMessage();
				Thread.Sleep(10);
			} while (msg == null);

			Console.WriteLine(msg);

			Console.ReadKey();
		}
	}
}
