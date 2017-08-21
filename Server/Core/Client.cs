using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
	class Client
	{
		public int ClientID { get; private set; }
		public string ClientName { get
			{
				return "Client_#" + this.ClientID;
			}
		}

		private Connection Connection { get; set; }

		public Client(int id, Connection conn) {
			this.ClientID = id;
			this.Connection = conn;
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
