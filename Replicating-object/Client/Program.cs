using SharedData;
using System;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "CLIENT";

            string ip = "127.0.0.1";
            int port = 26750;

            Client client = new Client(ip, port);
            client.SendMessage(Packet.PLAYER_SPAWN);
            client.SendMessage(Packet.PLAYER_MOVE);
            client.SendMessage(Packet.PLAYER_ATTACK);

            Console.ReadKey();
        }
    }
}
