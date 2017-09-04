using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class AHPHandler
    {
        public static string Message(string msg, Client client)
        {
            Debug.Write("AHPHandler Recived message > " + msg);
            string result = "";
            string[] msgArray = msg.Split(' ');
            if (msgArray[0] == "AHP/1.0")
            {
                switch (msgArray[1])
                {
                    default: throw new NotImplementedException();
                    case "bid":
                        if(BidHandler(msgArray, client))
                        {
                            ClientRepository.Instance.Broadcast(msg);
                        }
                        break;


                }
            }
            return result;
        }
        private static bool BidHandler(string[] msgarray, Client client)
        {
            bool result = false;
            int price = int.Parse(msgarray[3]);
            if (Auctioneer.item.Price < price)
            {
                Auctioneer.ResetGavel();
                Auctioneer.waitForFirstBid.Set();
                Auctioneer.item.Price = price;
                Auctioneer.item.HighestBidClient = client;
                result = true;
            }
            return result;
        }

        public static void Gavel(int gavelsleft)
        {
            ClientRepository.Instance.Broadcast("AHP/1.0 gavel " + gavelsleft);
        }

        public static void ItemSold(Item item)
        {
            ClientRepository.Instance.Broadcast("AHP/1.0 sold " + item.ID + " " + item.HighestBidClient.ClientName + " " + item.Price);
        }
    }
}
