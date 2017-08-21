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
		ClientRepository repoConn = ClientRepository.Instance;
		public void AcceptClients()
		{
			IPAddress ip = IPAddress.Any;
			TcpListener listener = new TcpListener(ip, 5000);
			listener.Start();

			while(Active)
			{
				TcpClient client = listener.AcceptTcpClient();
				repoConn.CreateClient(client);
				Console.WriteLine("A Client has joined."); // TODO > Refactor into CLI.
			}
		}
	}
}
