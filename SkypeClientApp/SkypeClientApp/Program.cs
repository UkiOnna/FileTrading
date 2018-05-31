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
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient();
            Client clientFile = new Client(client);
            clientFile.Start();
            try
            {
                while (true)
                {
                    Console.Write("Введите путь -- для выхода 4");
                    string path = Console.ReadLine();
                    if (path == "4")
                    {
                        break;
                    }
                    else
                    {
                        if (File.Exists(path))
                        {
                            Task.Run(() =>
                             {
                                 clientFile.Working(path);//запускаем метод в отдельном потоке
                             });
                        }
                        else
                        {
                            Console.WriteLine("Файл не наййден");
                        }
                    }
                }

            }

            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                if (client != null)
                {
                    client.Close();
                }
            }
        }
    }
}
