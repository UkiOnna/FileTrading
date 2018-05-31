using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SkypeServerApp
{
    public class ClientWork
    {
        private TcpClient client;
        private NetworkStream stream;


        public ClientWork(TcpClient client)
        {
            this.client = client;
        }

        public void Work(object obj)//метод работает в отдельном потоке
        {
            stream = null;
            try
            {
                stream = client.GetStream();
                byte[] buffer = new byte[1024];

                while (true)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    int bytes;
                    do
                    {
                        bytes = stream.Read(buffer, 0, buffer.Length);
                        stringBuilder.Append(Encoding.Default.GetString(buffer, 0, bytes));
                    } while (stream.DataAvailable);

                    if (stringBuilder.ToString().Any())
                    {
                        string dataJson = stringBuilder.ToString();
                        var message = JsonConvert.DeserializeObject<FileInfo>(dataJson);

                        using (FileStream fileStream = new FileStream($"{Directory.GetCurrentDirectory()}\\{message.fileName}", FileMode.Create))
                        {
                            fileStream.Write(message.Bytes, 0, message.Bytes.Length);
                        }

                        Console.WriteLine("Load!");
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            finally
            {
                if (stream != null)
                {
                    stream.Dispose();
                }
                if (client != null)
                {
                    client.Close();
                }
            }
        }
    }
}
