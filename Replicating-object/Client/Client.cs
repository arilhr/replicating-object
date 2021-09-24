using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using SharedData;

namespace Client
{
    class Client
    {
        TcpClient socket;
        NetworkStream stream;
        string ip;
        int port;

        public Client(string ip, int port)
        {
            try
            {
                // try connect to server
                socket = new TcpClient(ip, port);
                stream = socket.GetStream();

                Console.WriteLine("Connected to server...");

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }
        }

        public void SendMessage(Packet _packet)
        {
            // Translate the Message into ASCII.
            byte[] data = BitConverter.GetBytes((Int32) _packet);

            // Send the message to the connected server. 
            stream.Write(data, 0, data.Length);
        }
    }
}
