using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Core
{
	internal class Connection
	{
		private Thread Writter;
		private Thread Reader;

		public Queue<string> WriteBuffer = new Queue<string>();
		public Queue<string> ReadBuffer = new Queue<string>();

		//private object WriteBufferLock = new object();
		//private object ReadBufferLock = new object();

		public Connection(TcpClient client)
		{
			StreamWriter sw = new StreamWriter(client.GetStream());
			StreamReader sr = new StreamReader(client.GetStream());

			Writter = new Thread(() => ConnWritter(sw));
			Reader = new Thread(() => ConnReader(sr));

			Writter.Start();
			Reader.Start();
		}

		private void ConnWritter(StreamWriter sw)
		{
			while(Auctioneer.Active)
			{
				if (WriteBuffer.Count > 0)
				{
					string message = WriteBuffer.Dequeue();
					sw.WriteLine(message);
					sw.Flush();
				}
				else Thread.Sleep(100);
			}
		}

		private void ConnReader(StreamReader sr)
		{
			while(Auctioneer.Active)
			{
				string message = sr.ReadLine();
				ReadBuffer.Enqueue(message);
			}
		}

	}
}
