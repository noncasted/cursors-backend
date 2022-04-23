using System;
using Application.Core.Connections;
using Application.Core.DataTransfer;
using Application.Core.Routing;
using Domain.Connection;
using Domain.DataTransfer;

namespace Application.Core.Clients
{
    public class Client : IClient
    {
        public Client(int _clientID, PacketSender _packetSender, Router _router, TargetConnection _defaultConnection)
        {
            id = _clientID;
            
            tcp = new TcpConnection(this, _packetSender, _router);
            udp = new UdpConnection(this, _packetSender, _router);

            defaultConnection = _defaultConnection;
        }
        
        private readonly ITcpConnection tcp;
        private readonly IUdpConnection udp;

        private readonly int id;
        private readonly TargetConnection defaultConnection;

        private bool inRoom = false;
        private int roomId = -1;

        public ITcpConnection Tcp => tcp;
        public IUdpConnection Udp => udp;
        public int Id => id;
        public bool InRoom => inRoom;
        public int RoomId => roomId;

        public void SendData(string _route, params byte[][] _data)
        {
            using (Packet _packet = new Packet(_route))
            {
                for (int i = 0; i < _data.Length; i++)
                    _packet.Write(_data[i]);
                
                _packet.WriteLength();
                
                switch (defaultConnection)
                {
                    case TargetConnection.TCP:
                        tcp.SendData(_packet);
                        break;
                    case TargetConnection.UDP:
                        udp.SendData(_packet);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public void OnRoomJoined(int _room)
        {
            inRoom = true;
            roomId = _room;
        }

        public void OnRoomLeft()
        {
            inRoom = false;
        }
    }
}