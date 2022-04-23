using System;
using System.Net;
using System.Net.Sockets;
using Application.Core.Clients;
using Application.Core.DataTransfer;
using Application.Core.Routing;

namespace Application.Core.Connections
{
    public class UdpConnection
    {
        public UdpConnection(Client _owner, PacketSender _packetSender, Router _router)
        {
            id = _owner.Id;
            owner = _owner;

            packetSender = _packetSender;
            router = _router;
        }
        
        private readonly int id;
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

            packetSender.UDPTest(owner);
        }

        public void SendData(Packet _packet)
        {
            udpListener.BeginSend(_packet.ToArray(), _packet.Length(), endPoint, null, null);
        }

        public void HandleData(Packet _packetData)
        {
            int _packetLength = _packetData.ReadInt();
            byte[] _packetBytes = _packetData.ReadBytes(_packetLength);
                
            ThreadManager.ExecuteOnMainThread(() =>
            {
                using (Packet _packet = new Packet(_packetBytes))
                {
                    int _packetId = _packet.ReadInt();
                    router.Route(_packetId, id, _packet);
                }
            });
        }
    }
}