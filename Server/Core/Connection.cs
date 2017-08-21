using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Core
{
	class Connection
	{
		private Thread self;

		Connection()
		{
			self = new Thread();
			self.Start();
		}

	}
}
