using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SkypeClientApp
{
    public class Client
    {
        private TcpClient client;
        private NetworkStream stream;

        public Client(TcpClient client)
        {
            this.client = client;
        }

        public async void Start()
        {
            await client.ConnectAsync(IPAddress.Parse("127.0.0.1"), 3535);
        }

        public void Working(string path)//метод работает асинхронно 
        {

            FileInfo fileInfo = new FileInfo(path);

            FileInfoo info = new FileInfoo();
            info.Bytes = File.ReadAllBytes(path);
            info.Extension = fileInfo.Extension;
            info.FileName = fileInfo.Name;


            var dataJson = JsonConvert.SerializeObject(info);
            var dataBytes = Encoding.Default.GetBytes(dataJson);

            if (stream == null)
            {
                stream = client.GetStream();
            }
            stream.Write(dataBytes, 0, dataBytes.Length);

        }
    }
}
