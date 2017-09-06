using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.CLI
{
	public static class Console2
	{
		private static object thisLock = new object();
		private static string result = "";
		public static void WriteLine(string message)
		{
			lock (thisLock)
			{
				foreach(char c in result)
				{
					System.Console.Write("\b");
				}
				System.Console.WriteLine(message);
				System.Console.Write(result);
			}
		}

		public static string ReadLine()
		{
			//string msg;
			//System.ConsoleKeyInfo key = System.Console.ReadKey();
			//lock (thisLock)
			//{
			//	msg = System.Console.ReadLine();
			//}
			//return key.KeyChar.ToString() + msg;
			string lresult;
			ConsoleKeyInfo keypressed = ReadKey();
			while(keypressed.Key != ConsoleKey.Enter)
			{
				result += keypressed.KeyChar.ToString();
				keypressed = ReadKey();
			}
			lresult = result.ToString();
			result = "";
			if (lresult.Length > 0)
			{
				return lresult + "\n";
			}
			else return lresult;
		}

		public static System.ConsoleKeyInfo ReadKey()
		{
			lock (thisLock)
			{
				return System.Console.ReadKey();
			}
		}

	}
}
