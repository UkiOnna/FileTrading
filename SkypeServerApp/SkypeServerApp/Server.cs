using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SkypeServerApp
{
    public class Server
    {
        private string adress;
        private int port;
        private TcpListener server;

        public Server(string adr, int pt)
        {
            adress = adr;
            port = pt;
        }

        public async void Working()
        {
            server = new TcpListener(IPAddress.Parse(adress), port);

            try
            {
                server.Start();
                while (true)
                {
                    var client = await server.AcceptTcpClientAsync();

                    ClientWork clientObj = new ClientWork(client);
                    ThreadPool.QueueUserWorkItem(new WaitCallback(clientObj.Work), null); //запускает каждого клиента в отдельный поток

                    server.Start();
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                if (server != null)
                {
                    server.Stop();
                }
            }
        }
    }
}
