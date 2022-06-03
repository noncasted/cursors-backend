using System;
using System.Net;
using System.Net.Sockets;
using Server.Core.Connection.Packets;
using Server.Core.Processing;
using Server.Core.Routing.Routes;

namespace Server.Core.Connection.Connection.Handling
{
    public class UdpConnection : IConnection
    {
        public UdpConnection(Action<Packet> _receivingCallback, int _clientId)
        {
            receivingCallback = _receivingCallback;
            ClientId = _clientId;
        }

        public readonly int ClientId;
        
        private readonly Action<Packet> receivingCallback;

        private IPEndPoint endPoint;
        private UdpClient udpListener;

        public event Action<UdpConnection> Disconnected;

        public bool CheckEndPointEquality(IPEndPoint _endPoint)
        {
            if (endPoint.ToString().Equals(_endPoint.ToString()) == true)
                return true;

            return false;
        }

        public void Connect(IPEndPoint _endPoint, UdpClient _udpListener, int _id)
        {
            if (_endPoint == null)
            {
                Console.WriteLine($"Trying to connect client with empty endPoint");
                return;
            }
            
            endPoint = _endPoint;
            udpListener = _udpListener;
            
            using (Packet _packet = new Packet(ClientRoute.On_Connected_Udp))
            {
                _packet.Write("Connected to server via UDP.");
                _packet.Write(_id);

                SendData(_packet, PacketType.Unreliable);
            }
        }

        public void SendData(Packet _packet, PacketType _type)
        {
            _packet.WriteLength();
            
            udpListener.BeginSend(_packet.ToArray(), _packet.GetLength(), endPoint, null, null);
        }
        
        public void HandleData(Packet _packetData)
        {
            int _packetLength = _packetData.ReadInt();
            byte[] _packetBytes = _packetData.ReadBytes(_packetLength);
            
            ThreadManager.AddConnectionTask(OnDataReceived, _packetBytes);
        }
        
        private void OnDataReceived(byte[] _bytes)
        {
            using (Packet _packet = new Packet(_bytes))
            {
                receivingCallback?.Invoke(_packet);
            }
        }

        public void Disconnect()
        {
            endPoint = null;
            
            Disconnected?.Invoke(this);
        }
    }
}