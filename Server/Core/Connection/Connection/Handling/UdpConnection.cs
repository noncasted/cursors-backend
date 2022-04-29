using System;
using System.Net;
using System.Net.Sockets;
using Server.Core.Processing;
using Server.Core.Routing.Routes;

namespace Server.Core.Connection.Connection.Handling
{
    public class UdpConnection
    {
        public UdpConnection(Action<Packet> _receivingCallback)
        {
            receivingCallback = _receivingCallback;
        }

        private readonly Action<Packet> receivingCallback;

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
            
            using (Packet _packet = new Packet(ClientRoute.On_Connected_Udp))
            {
                SendData(_packet);
            }
        }

        public void SendData(Packet _packet)
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
    }
}