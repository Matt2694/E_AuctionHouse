﻿using System;
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

		/// <summary>
		/// Creates the client and connection class for a connecting user
		/// </summary>
		/// <param name="conn">TCPClient from the incomming connection.</param>
		public Client CreateClient(TcpClient conn)
		{
			Connection newConnection = new Connection(conn);
			Client newClient = new Client(this.NextID, newConnection);
			this.AddClient(newClient);
			
            return newClient;
		}

		/// <summary>
		/// Add a client to the repository.
		/// </summary>
		/// <param name="client">Client to be added.</param>
		private void AddClient(Client client)
		{
			Clients.Add(client);
		}

        public void Broadcast(string msg)
        {
            foreach(Client client in Clients)
            {
                client.SendMessage(msg);
            }
        }

        public List<Client> GetList()
        {
            return Clients.ToList();
        }
	}
}
