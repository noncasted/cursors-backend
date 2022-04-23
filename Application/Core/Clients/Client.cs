using Application.Core.Connections;
using Application.Core.DataTransfer;
using Application.Core.Routing;

namespace Application.Core.Clients
{
    public class Client
    {
        public readonly TcpConnection Tcp;
        public readonly UdpConnection Udp;

        public readonly int Id;
        
        public Client(int _clientID, PacketSender _packetSender, Router _router)
        {
            Id = _clientID;
            
            Tcp = new TcpConnection(this, _packetSender, _router);
            Udp = new UdpConnection(this, _packetSender, _router);
        }
    }
}