using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Core
{
	public class Auctioneer
	{
        public static Item item = new Item(1, "Chair", 100);
		public static bool Active = true;
        private static int gavelCount = 18;
        public static EventWaitHandle waitForFirstBid = new AutoResetEvent(false);
		ClientRepository repoClient = ClientRepository.Instance;

        public Auctioneer()
        {
            Thread incomming = new Thread(HandleBids);
            incomming.Start();
            Thread gavelThread = new Thread(GavelHandler);
            gavelThread.Start();
        }

		/// <summary>
		/// Method that should be started to accept clients
		/// To shut down, set the "Active" bool to false.
		/// </summary>
		public void AcceptClients()
		{
			IPAddress ip = IPAddress.Any;
			TcpListener listener = new TcpListener(ip, 5000);
			listener.Start();

			while(Active)
			{
				TcpClient tcpClient = listener.AcceptTcpClient();
				Client client = repoClient.CreateClient(tcpClient);
				Console.WriteLine("A Client has joined."); // TODO > Refactor into CLI.
                client.SendMessage("AHP/1.0 item " + item.ID+ " " + item.Name +" " + item.StartingPrice +" " + item.Price);
			}
		}

        public void HandleBids()
        {
            List<Client> clientList;
            while (Active)
            {
                clientList = repoClient.GetList();
                foreach(Client client in clientList)
                {
                    string msg = client.ReadMessage();
                    if(msg != null) AHPHandler.Message(msg, client);
                }
            }
        }

        public static void ResetGavel()
        {
            gavelCount = 18;
        }

        public void GavelHandler()
        {
            waitForFirstBid.WaitOne();
            while (Active && gavelCount > -1)
            {
                if (gavelCount == 8)
                {
                    AHPHandler.Gavel(2);
                }
                else if (gavelCount == 3)
                {
                    AHPHandler.Gavel(1);
                }
                else if (gavelCount == 0)
                {
                    AHPHandler.Gavel(0);
                    AHPHandler.ItemSold(item);
                }
				Debug.Write("Gavel Count = " + gavelCount);
				gavelCount--;
                Thread.Sleep(1000);
            }
        }
	}
}
