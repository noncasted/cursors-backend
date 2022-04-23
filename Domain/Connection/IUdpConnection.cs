using System.Net;
using System.Net.Sockets;
using Domain.DataTransfer;

namespace Domain.Connection
{
    public interface IUdpConnection
    {
        void Connect(IPEndPoint _endPoint, UdpClient _udpListener);
        void SendData(Packet _packet);
        void HandleData(Packet _packet);
        bool CheckEndPointEquality(IPEndPoint _endPoint);
        
        bool Connected { get; }
        EndPoint RemoteEndPoint { get; }
    }
}