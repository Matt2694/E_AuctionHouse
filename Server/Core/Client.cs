using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
	/// <summary>
	/// Client: TCP link to the server from another process
	/// </summary>
	public class Client
	{
		public int ClientID { get; private set; }
		public string ClientName { get
			{
				return "Client_#" + this.ClientID;
			}
		}

		private Connection Connection { get; set; }

		/// <summary>
		/// Constrcutor for the client class.
		/// </summary>
		/// <param name="id">Identification number for the client</param>
		/// <param name="conn">Connection class, handles the actual connection.</param>
		public Client(int id, Connection conn) {
			this.ClientID = id;
			this.Connection = conn;
		}

		/// <summary>
		/// Proxy method for the connection.messageRead.
		/// See "Connection" class for more details.
		/// </summary>
		/// <returns></returns>
		public string ReadMessage()
		{
			return Connection.messageRead();
		}

		/// <summary>
		/// Proxy method for the connection.messageSend.
		/// See "Connection" class for more details.
		/// </summary>
		/// <param name="msg">Message to send to the client</param>
		public void SendMessage(string msg)
		{
			Connection.messangeSend(msg);
		}
	}
}
