using System;
using Server.Core.Connection.Packets;

namespace Server.Core.Connection.Connection.Handling
{
    public class Client
    {
        public Client(int _clientID, Action<Packet> _dataReceivedCallback)
        {
            id = _clientID;

            DataReceivedCallback = _dataReceivedCallback;
        }

        public readonly Action<Packet> DataReceivedCallback;

        private IConnection connection;
        private bool connected = false; 

        private readonly int id;
        
        public int Id => id;
        public bool Connected => connected;

        public void InjectConnection(IConnection _connection)
        {
            connection = _connection;
            connected = false;
        }

        public void SendData(Packet _packet, PacketType _type)
        {
            connection.SendData(_packet, _type);
        }
        
        public void Disconnect()
        {
            connected = false;
            
            connection.Disconnect();
            
            Console.WriteLine("Client disconnected");
        }
    }
}