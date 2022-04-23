using System;
using System.Net;
using System.Net.Sockets;
using Application.Core.Clients;
using Domain.Connection;
using Domain.DataTransfer;

namespace Application.Core.ListenerProcessors
{
    public class UdpProcessor
    {
        public UdpProcessor(int _port, ServerClients _clients)
        {
            listener = new UdpClient(_port);
            clients = _clients;
            
            listener.BeginReceive(ReceiveCallback, null);
        }
        
        private readonly UdpClient listener;
        private readonly ServerClients clients;
        
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

                IUdpConnection _connection = clients.GetUdp(_clientId);
                
                if (_connection.Connected == false)
                {
                    Console.WriteLine("Not connected");
                    _connection.Connect(_clientEndPoint, listener);
                    return;
                }

                if (_connection.CheckEndPointEquality(_clientEndPoint) == true)
                    _connection.HandleData(_packet);
            }
        }
    }
}