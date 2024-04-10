using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace C_chapter
{
    public class Program
    {
        static void Main(string[] args)
        {

            TcpListener listener = new TcpListener(IPAddress.Any, 8080);
            listener.Start();

            Console.WriteLine("Waiting for connection...");
            TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine("Connection accepted.");

            using (NetworkStream stream = client.GetStream())
            {
                // Отправка HTTP-ответа
                string response = "HTTP/1.1 200 OK\n" +
                                  "Date: Wed, 11 Feb 2009 11:20:59 GMT\n" +
                                  "Server: Apache\n" +
                                  "Last-Modified: Wed, 11 Feb 2021 11:20:59 GMT\n" +
                                  "Content-Type: text/html; charset=utf-8\n" +
                                  "Content-Length: 1234\n" +
                                  "\n\n" +
                                  "<!DOCTYPE html>\n" +
                                  "<html>\n" +
                                  "<body>\n" +
                                  "<h1>My First Heading</h1>\n" +
                                  "<p>My first paragraph.</p>\n" +
                                  "</body>\n" +
                                  "</html>";

                byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                stream.Write(responseBytes, 0, responseBytes.Length);
            }

            Console.ReadKey();

            client.Close();
            listener.Stop(); 
        }
    }
}
