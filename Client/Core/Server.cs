using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Core
{
	public class Server
	{
		public static bool Active = true;

		private Connection Connection;

		public Server(string ip)
		{
			TcpClient client = new TcpClient(ip, 5000);
			Connection = new Connection(client);			
		}

		public string ReadMessage()
		{
			return Connection.messageRead();
		}

		public void SendMessage(string msg)
		{
			Connection.messangeSend(msg);
		}
	}
}
