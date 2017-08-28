using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class AHPHandler
    {
        public static string Message(string msg)
        {
            string result = "";
            string[] msgArray = msg.Split(' ');
            if (msgArray[0] == "AHP/1.0")
            {
                switch(msgArray[1])
                {
                    default: throw new NotImplementedException();
                    case "item":
                        result = "Current item is: " + msgArray[3] + ". Current price: " + msgArray[5] + ". Starting price was: " + msgArray[4];
                        Item.ID = msgArray[2];
                        break;
                    case "bid":
                        result = "Current prise: " + msgArray[3];
                        break;

                    
                }
            }
            return result;
        }

        public static string MakeBid(string id, string price)
        {
            return "AHP/1.0 bid " + id + " " + price;
        }
    }
}
