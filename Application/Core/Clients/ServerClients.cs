using System;
using System.Collections.Generic;
using System.Net;
using Application.Core.Connections;
using Application.Core.DataTransfer;
using Application.Core.Routing;

namespace Application.Core.Clients
{
    public class ServerClients
    {
        public ServerClients(int _maxClients, PacketSender _packetSender, Router _router)
        {
            maxClients = _maxClients;
            
            for (int i = 1; i <= maxClients; i++)
                clients.Add(i, new Client(i, _packetSender, _router));
        }

        private readonly int maxClients;
        private readonly Dictionary<int, Client> clients = new Dictionary<int, Client>();

        public void SendData(int _clientId, Packet _packet, TargetConnection _targetConnection)
        {
            switch (_targetConnection)
            {
                case TargetConnection.TCP:
                    clients[_clientId].Tcp.SendData(_packet);
                    break;
                case TargetConnection.UDP:
                    clients[_clientId].Udp.SendData(_packet);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_targetConnection), _targetConnection, null);
            }
        }

        public EndPoint GetRemoteEndPoint(int _clientId, TargetConnection _targetConnection)
        {
            switch (_targetConnection)
            {
                case TargetConnection.TCP:
                    return clients[_clientId].Tcp.RemoteEndPoint;
                case TargetConnection.UDP:
                    return clients[_clientId].Udp.RemoteEndPoint;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_targetConnection), _targetConnection, null);
            }
        }

        public UdpConnection GetUdp(int _clientId)
        {
            return clients[_clientId].Udp;
        }
        
        public bool GetFirstAvailable(out Client _client)
        {
            for (int i = 1; i <= maxClients; i++)
            {
                if (clients[i].Tcp.Connected == false)
                {
                    _client = clients[i];
                    return true;
                }
            }

            _client = null;
            
            return false;
        }
    }
}