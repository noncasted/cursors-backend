using System;
using System.Net;
using System.Net.Sockets;
using Server.Core.Connection.Connection.Handling;
using Server.Core.Users;

namespace Server.Core.Connection.Connection.Processing
{
    public class UdpProcessor
    {
        public UdpProcessor(int _port, UsersList users)
        {
            listener = new UdpClient(_port);
            users = users;
            
            listener.BeginReceive(ReceiveCallback, null);
        }
        
        private readonly UdpClient listener;
        private readonly UsersList users;
        
        private void ReceiveCallback(IAsyncResult _result)
        {
            Console.WriteLine("Udp received 00");
            
            IPEndPoint _clientEndPoint = new IPEndPoint(IPAddress.Any, 0);

            byte[] _data = listener.EndReceive(_result, ref _clientEndPoint);
            listener.BeginReceive(ReceiveCallback, null);

            if (_data.Length < 4)
                return;

            using (Packet _packet = new Packet(_data))
            {
                int _clientId = _packet.ReadInt();

                if (_clientId == 0)
                    return;

                UdpConnection _connection = users.GetUser(_clientId).Client.Udp;

                if (_connection.Connected == false)
                {
                    _connection.Connect(_clientEndPoint, listener);
                    
                    return;
                }

                if (_connection.CheckEndPointEquality(_clientEndPoint) == true)
                    _connection.HandleData(_packet);
            }
        }
    }
}