using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace UI.CLI
{
	class Program
	{
		static void Main(string[] args)
		{
			Auctioneer auctioneer = new Auctioneer();
			auctioneer.AcceptClients();
		}
	}
}
