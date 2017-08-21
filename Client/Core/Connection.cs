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

		private Queue<string> WriteBuffer = new Queue<string>();
		private Queue<string> ReadBuffer = new Queue<string>();

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
				return ReadBuffer.Dequeue();
			else return null;
		}

		private void ConnWritter(StreamWriter sw)
		{
			while (Server.Active)
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
			while (Server.Active)
			{
				string message = sr.ReadLine();
				ReadBuffer.Enqueue(message);
			}
		}

	}
}
