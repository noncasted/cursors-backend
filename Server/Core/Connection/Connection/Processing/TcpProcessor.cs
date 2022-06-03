using System;
using System.Net;
using System.Net.Sockets;
using Server.Core.Connection.Connection.Handling;
using Server.Core.Users;

namespace Server.Core.Connection.Connection.Processing
{
    public class TcpProcessor
    {
        public TcpProcessor(int _port, UsersList _users)
        {
            users = _users;
            
            listener = new TcpListener(IPAddress.Any, _port);
            listener.Start();
            listener.BeginAcceptTcpClient(ConnectionCallback, null);
        }
        
        private readonly TcpListener listener;
        private readonly UsersList users;
        
        private void ConnectionCallback(IAsyncResult _result)
        {
            TcpClient _tcpClient = listener.EndAcceptTcpClient(_result);
            listener.BeginAcceptTcpClient(ConnectionCallback, null);

            Console.WriteLine($"Incoming connection from {_tcpClient.Client.RemoteEndPoint}...");

            if (users.GetFirstAvailableClient(out Client _client) == false)
            {
                Console.WriteLine($"{_tcpClient.Client.RemoteEndPoint} failed to connect: Server full!");
                
                return;
            }

            TcpConnection _connection = new TcpConnection(_client.DataReceivedCallback, _client.Disconnect);
            _connection.Connect(_tcpClient, _client.Id);
            _client.InjectConnection(_connection);
        }
    }
}