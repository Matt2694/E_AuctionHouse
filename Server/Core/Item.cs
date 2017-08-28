using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Item
    {
		public int ID { get; set; }
		public string Name { get; set; }
		public int Price { get; set; } //TODO:check to save only the highest bid 
        public Client HighestBidClient { get; set; }
		public int StartingPrice { get; set; }

        public Item(int id,string name, int price)
        {
            this.ID = id;
            this.Name = name;
            this.Price = price;
            this.StartingPrice = price;
        }
	}

    
}
