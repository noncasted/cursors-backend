using System;
using Server.Core.Routing.Routes;

namespace Server.Core.Connection.Connection.Handling
{
    public class Client
    {
        public Client(int _clientID, TargetConnection _defaultConnection, Action<Packet> _dataReceivedCallback)
        {
            id = _clientID;
            
            Tcp = new TcpConnection(_dataReceivedCallback);
            Udp = new UdpConnection(_dataReceivedCallback);

            defaultConnection = _defaultConnection;
        }
        
        public readonly TcpConnection Tcp;
        public readonly UdpConnection Udp;

        private readonly int id;
        private readonly TargetConnection defaultConnection;
        
        public int Id => id;

        public void SendData(ClientRoute _route, params byte[][] _data)
        {
            using (Packet _packet = new Packet(_route))
            {
                for (int i = 0; i < _data.Length; i++)
                    _packet.Write(_data[i]);
                
                switch (defaultConnection)
                {
                    case TargetConnection.TCP:
                        Tcp.SendData(_packet);
                        break;
                    case TargetConnection.UDP:
                        Udp.SendData(_packet);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}