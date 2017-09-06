using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
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

		private ConcurrentQueue<string> WriteBuffer = new ConcurrentQueue<string>();
		private ConcurrentQueue<string> ReadBuffer = new ConcurrentQueue<string>();

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

		public void messangeSend(string msg)
		{
			WriteBuffer.Enqueue(msg);
		}

		public string messageRead()
		{
			if (ReadBuffer.Count > 0)
			{
				ReadBuffer.TryDequeue(out string result);
				return result;
			}
			else return null;
		}

		private void ConnWritter(StreamWriter sw)
		{
			while (Server.Active)
			{
				if (WriteBuffer.Count > 0)
				{
					WriteBuffer.TryDequeue(out string message);
					sw.WriteLine(message);
					sw.Flush();
				}
				else Thread.Sleep(100);
			}
		}

		private void ConnReader(StreamReader sr)
		{
			while (Server.Active)
			{
				string message = sr.ReadLine();
				ReadBuffer.Enqueue(message);
			}
		}

	}
}
