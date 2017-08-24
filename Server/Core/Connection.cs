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
	/// <summary>
	/// Handles sending and recieving TCP messages to/from a client.
	/// </summary>
	internal class Connection
	{
		private Thread Writter;
		private Thread Reader;

		// Buffers for messages to be sent & recived by the client
		private Queue<string> WriteBuffer = new Queue<string>(); // These strings are sent to the client
		private Queue<string> ReadBuffer = new Queue<string>(); // recived from the client

		//private object WriteBufferLock = new object();
		//private object ReadBufferLock = new object();

		public Connection(TcpClient client)
		{
			// Put the stream reader and writer in the own threads
			// They then pull and push messages into the queue buffers as nessesarry.
			StreamWriter sw = new StreamWriter(client.GetStream());
			StreamReader sr = new StreamReader(client.GetStream());

			Writter = new Thread(() => ConnWritter(sw));
			Reader = new Thread(() => ConnReader(sr));

			Writter.Start();
			Reader.Start();
		}

		/// <summary>
		/// Put a message into the writebuffer queue.
		/// These are messages that will be sent to the client
		/// </summary>
		/// <param name="msg">Message to send to the client</param>
		public void messangeSend(string msg)
		{
			WriteBuffer.Enqueue(msg);
		}
		/// <summary>
		/// Returns oldest message from the read buffer.
		/// These are messages recived from a client
		/// </summary>
		/// <returns>Oldest message recived from client. Null if no messages.</returns>
		public string messageRead()
		{
			if (ReadBuffer.Count > 0)
				return ReadBuffer.Dequeue();
			else return null;
		}

		/// <summary>
		/// Takes a message from the writebuffer queue, and actually sends it to the client.
		/// Runs in its own thread.
		/// </summary>
		/// <param name="sw">The streamwriter that was created from the TCPclient in the constructor</param>
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
				else Thread.Sleep(100); // Should we reduce this? OR replace with wait handles, stackoverflow says thats better...
			}
		}

		/// <summary>
		/// Recives messages from the client, and stores these in readbuffer.
		/// Runs in its own thread.
		/// </summary>
		/// <param name="sr">The streamreader that was created from the TCPClient in the constructor</param>
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
