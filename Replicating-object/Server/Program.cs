using System;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "SERVER";
            string ip = "127.0.0.1";
            int port = 26750;

            Server server = new Server(ip, port);
            server.Start();

            Console.ReadKey();
        }
    }
}
