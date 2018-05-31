using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SkypeServerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server("127.0.0.1",3535);
            server.Working();
            Console.ReadLine();
        }
    }
}
