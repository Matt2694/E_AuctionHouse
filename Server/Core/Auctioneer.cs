using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Core
{
	public class Auctioneer
	{
		public static bool Active = true;
		private List<Connection> Clients = new List<Connection>();
		public void AcceptClients()
		{
			IPAddress ip = IPAddress.Any;
			TcpListener listener = new TcpListener(ip, 5000);
			listener.Start();

			while(Active)
			{
				TcpClient client = listener.AcceptTcpClient();
				Connection newClient = new Connection(client);
				Clients.Add(newClient);

				Console.WriteLine("A new client has connected.");
				string message = "Hi";
				newClient.WriteBuffer.Enqueue(message);
			}
		}
	}
}
