using System;
using System.Collections.Generic;
using Application.Core.Clients;
using Domain.Connection;
using Domain.DataTransfer;
using Domain.Services;

namespace Application.Core.DataTransfer
{
    public class PacketSender : IPacketSender
    {
        public void SendConnectionData(Client _client, string _message)
        {
            using (Packet _packet = new Packet(ClientRoute.On_Connected))
            {
                _packet.Write(_message);
                _packet.Write(_client.Id);

                SendToClient(_client, TargetConnection.TCP, _packet);
            }
        }

        public void ConnectUdp(Client _client)
        {
            Console.WriteLine("Send test udp");
            
            using (Packet _packet = new Packet(ClientRoute.On_Connected_Udp))
            {
                _packet.Write("Test packet for UDP");
                
                SendToClient(_client, TargetConnection.UDP, _packet);
            }
        }

        public void SendToClient(IClient _client, TargetConnection _targetConnection, Packet _packet)
        {
            _packet.WriteLength();

            switch (_targetConnection)
            {
                case TargetConnection.TCP:
                    _client.Tcp.SendData(_packet);
                    break;
                case TargetConnection.UDP:
                    _client.Udp.SendData(_packet);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_targetConnection), _targetConnection, null);
            }
        }

        public void SendToAll(IReadOnlyList<IClient> _clients, TargetConnection _targetConnection, Packet _packet)
        {
            _packet.WriteLength();
            
            int _count = _clients.Count;

            for (int i = 0; i < _count; i++)
            {
                switch (_targetConnection)
                {
                    case TargetConnection.TCP:
                        _clients[i].Tcp.SendData(_packet);
                        break;
                    case TargetConnection.UDP:
                        _clients[i].Udp.SendData(_packet);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(_targetConnection), _targetConnection, null);
                }
            }
        }

        public void SendToOthers(IReadOnlyList<IClient> _clients, IClient _sender, TargetConnection _targetConnection, Packet _packet)
        {
            _packet.WriteLength();
            
            int _count = _clients.Count;

            for (int i = 0; i < _count; i++)
            {
                if (_clients[i].Id == _sender.Id)
                    continue;
                
                switch (_targetConnection)
                {
                    case TargetConnection.TCP:
                        _clients[i].Tcp.SendData(_packet);
                        break;
                    case TargetConnection.UDP:
                        _clients[i].Udp.SendData(_packet);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(_targetConnection), _targetConnection, null);
                }
            }
        }
    }
}
