using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Server.Core.Connection.Connection.Handling;
using Server.Core.Connection.Packets;
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
            connections = new Dictionary<int, UdpConnection>();
        }
        
        private readonly UdpClient listener;
        private readonly UsersList users;

        private readonly Dictionary<int, UdpConnection> connections;

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
                    
                    UdpConnection _clientConnection = new UdpConnection(_client.DataReceivedCallback, _clientId);
                    _clientConnection.Connect(_clientEndPoint, listener, _client.Id);
                    _client.InjectConnection(_clientConnection);
                    _clientConnection.Disconnected += OnDisconnected;
                    connections.Add(_client.Id, _clientConnection);
                    
                    return;
                }

                if (connections.TryGetValue(_clientId, out UdpConnection _connection) == false)
                {
                    Console.WriteLine($"No udp client for id {_clientId}");
                    
                    return;
                }
                
                if (_connection.CheckEndPointEquality(_clientEndPoint) == true)
                    _connection.HandleData(_packet);
            }
        }

        private void OnDisconnected(UdpConnection _connection)
        {
            _connection.Disconnected -= OnDisconnected;
            connections.Remove(_connection.ClientId);
        }
    }
}