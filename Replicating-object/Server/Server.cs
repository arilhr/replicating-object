using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using SharedData;

namespace Server
{
    public class Constant
    {
        public static byte[] dataBuffer = new byte[4096];
    }


    class Server
    {
        TcpListener listener;
        private string ip;
        private int port;
        
        private List<Client> clientOnServer = new List<Client>();
        public delegate void PacketHandler(int _fromClient);
        public static Dictionary<int, PacketHandler> packetHandlers;

        public Server(string _ip, int _port)
        {
            ip = _ip;
            port = _port;
        }

        public void Start()
        {
            listener = new TcpListener(IPAddress.Parse(ip), port);
            listener.Start();
            Console.WriteLine("Server Started");
            listener.BeginAcceptTcpClient(ConnectionCallback, null);
        }

        private void ConnectionCallback(IAsyncResult _result)
        {
            TcpClient client = listener.EndAcceptTcpClient(_result);
            listener.BeginAcceptTcpClient(ConnectionCallback, null);
            
            // add player on server
            Client newPlayer = new Client(client);
            clientOnServer.Add(newPlayer);
            Console.WriteLine($"Incoming connection from {client.Client.RemoteEndPoint}...");
        }

        private void InitializePacketHandler()
        {
            packetHandlers = new Dictionary<int, PacketHandler>()
            {
                { (int) Packet.PLAYER_SPAWN, SpawnPlayer}
            };
        }

        public void SpawnPlayer(int _fromClient)
        {

        }

        public void MovePlayer(int _fromClient)
        {

        }
    }


    public class Client
    {
        private TcpClient socket;
        private NetworkStream stream;
        private Player player;

        public Client(TcpClient _client)
        {
            socket = _client;
            socket.ReceiveBufferSize = Constant.dataBuffer.Length;
            socket.SendBufferSize = Constant.dataBuffer.Length;

            stream = socket.GetStream();

            stream.BeginRead(Constant.dataBuffer, 0, Constant.dataBuffer.Length, ReceiveData, null);
        }

        private void ReceiveData(IAsyncResult _result)
        {
            try
            {
                int _byteLength = stream.EndRead(_result);
                if (_byteLength <= 0)
                {
                    // disconnected
                    return;
                }

                byte[] data = new byte[_byteLength];
                Array.Copy(Constant.dataBuffer, data, _byteLength);

                HandleData(data);
                stream.BeginRead(Constant.dataBuffer, 0, Constant.dataBuffer.Length, ReceiveData, null);
            }
            catch (Exception _ex)
            {
                Console.WriteLine($"Error receiving TCP data: {_ex}");
                // disconnected
            }
        }

        private void HandleData(byte[] data)
        {
            byte[] buffer = data;
            int readPos = 0;

            while (buffer.Length - readPos >= 4)
            {
                int packetType = BitConverter.ToInt32(buffer, readPos);
                readPos += 4;

                switch (packetType)
                {
                    case (int)Packet.PLAYER_SPAWN:
                        Console.WriteLine("Spawned");
                        break;
                    case (int)Packet.PLAYER_MOVE:
                        Console.WriteLine("Player Move");
                        break;
                    case (int)Packet.PLAYER_ATTACK:
                        Console.WriteLine("Player Attack");
                        break;
                    default:
                        break;
                }
            }
            

        }
    }
}
