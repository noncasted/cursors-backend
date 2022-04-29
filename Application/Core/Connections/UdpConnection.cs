using System;
using System.Net;
using System.Net.Sockets;
using Application.Core.Clients;
using Application.Core.DataTransfer;
using Application.Core.Routing;
using Domain.Connection;
using Domain.DataTransfer;

namespace Application.Core.Connections
{
    public class UdpConnection : IUdpConnection
    {
        public UdpConnection(Client _owner, PacketSender _packetSender, Router _router)
        {
            owner = _owner;

            packetSender = _packetSender;
            router = _router;
        }
        
        private readonly Client owner;

        private readonly PacketSender packetSender;
        private readonly Router router;
        
        private bool connected = false;
        
        private IPEndPoint endPoint;
        private UdpClient udpListener;

        public bool Connected => connected;
        public EndPoint RemoteEndPoint => udpListener.Client.RemoteEndPoint;

        public bool CheckEndPointEquality(IPEndPoint _endPoint)
        {
            if (endPoint.ToString().Equals(_endPoint.ToString()) == true)
                return true;

            return false;
        }

        public void Connect(IPEndPoint _endPoint, UdpClient _udpListener)
        {
            if (_endPoint == null)
            {
                Console.WriteLine($"Trying to connect client with empty endPoint");
                return;
            }
            
            endPoint = _endPoint;
            udpListener = _udpListener;
            connected = true;
            
            packetSender.ConnectUdp(owner);
        }

        public void SendData(Packet _packet)
        {
            udpListener.BeginSend(_packet.ToArray(), _packet.Length(), endPoint, null, null);
        }

        public void HandleData(Packet _packetData)
        {
            Console.WriteLine("Handle udp");
            
            int _packetLength = _packetData.ReadInt();
            byte[] _packetBytes = _packetData.ReadBytes(_packetLength);
                
            ThreadManager.ExecuteOnMainThread(() =>
            {
                using (Packet _packet = new Packet(_packetBytes))
                {
                    int _route = _packet.ReadInt();
                    router.Route((ServerRoute)_route, owner, _packet);
                }
            });
        }
    }
}