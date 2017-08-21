using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Core
{
	class ClientRepository
	{
		private List<Client> Clients = new List<Client>();

		private static ClientRepository instance;

		private int NextID { get {
				return CurID++;
			} }

		private int CurID = 0;

		public static ClientRepository Instance {
			get
			{
				if (instance == null) instance = new ClientRepository();
				return instance;
			}
		}

		private ClientRepository() { }

		public void CreateClient(TcpClient conn)
		{
			Connection newConnection = new Connection(conn);
			Client newClient = new Client(this.NextID, newConnection);
			this.AddClient(newClient);
			newClient.SendMessage("Hi Client");
		}

		private void AddClient(Client client)
		{
			Clients.Add(client);
		}
	}
}
