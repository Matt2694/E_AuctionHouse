using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class Debug
    {
        public static void Write(string msg)
        {
            Console.WriteLine("[DEBUG] " + msg);
        }
    }
}
