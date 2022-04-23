using System;
using System.Net;
using System.Net.Sockets;
using Application.Core.Clients;

namespace Application.Core.ListenerProcessors
{
    public class TcpProcessor
    {
        public TcpProcessor(int _port, ServerClients _clients)
        {
            clients = _clients;
            
            listener = new TcpListener(IPAddress.Any, _port);
            listener.Start();
            listener.BeginAcceptTcpClient(ConnectionCallback, null);
        }
        
        private readonly TcpListener listener;
        private readonly ServerClients clients;
        
        private void ConnectionCallback(IAsyncResult _result)
        {
            TcpClient _tcpClient = listener.EndAcceptTcpClient(_result);
            listener.BeginAcceptTcpClient(ConnectionCallback, null);

            Console.WriteLine($"Incoming connection from {_tcpClient.Client.RemoteEndPoint}...");

            if (clients.GetFirstAvailable(out Client _client) == false)
            {
                Console.WriteLine($"{_tcpClient.Client.RemoteEndPoint} failed to connect: Server full!");
                
                return;
            }
            
            _client.Tcp.Connect(_tcpClient);
        }
    }
}