using System;
using System.Net;
using System.Net.Sockets;
using Server.Core.Connection.Connection.Handling;
using Server.Core.Users;

namespace Server.Core.Connection.Connection.Processing
{
    public class UdpProcessor
    {
        public UdpProcessor(int _port, UsersList _users)
        {
            listener = new UdpClient(_port);
            users = _users;
            
            listener.BeginReceive(ReceiveCallback, null);
        }
        
        private readonly UdpClient listener;
        private readonly UsersList users;
        
        private void ReceiveCallback(IAsyncResult _result)
        {
            IPEndPoint _clientEndPoint = new IPEndPoint(IPAddress.Any, 0);

            byte[] _data = listener.EndReceive(_result, ref _clientEndPoint);
            listener.BeginReceive(ReceiveCallback, null);
            
            if (_data.Length < 4)
                return;
            
            using (Packet _packet = new Packet(_data))
            {
                int _clientId = _packet.ReadInt();

                if (_clientId == 0)
                {
                    if (users.GetFirstAvailableClient(out Client _client) == false)
                    {
                        Console.WriteLine($"Failed to connect: Server full!");
                
                        return;
                    }
                    Console.WriteLine($"Connect new udp client on id: {_client.Id}");
                    _client.Udp.Connect(_clientEndPoint, listener, _client.Id);
                    return;
                }
                
                UdpConnection _connection = users.GetUser(_clientId).Client.Udp;
                
                if (_connection.CheckEndPointEquality(_clientEndPoint) == true)
                    _connection.HandleData(_packet);
            }
        }
    }
}