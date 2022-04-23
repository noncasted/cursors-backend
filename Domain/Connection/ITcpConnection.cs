using System.Net;
using System.Net.Sockets;
using Domain.DataTransfer;

namespace Domain.Connection
{
    public interface ITcpConnection
    {
        void Connect(TcpClient _socket);
        void SendData(Packet _packet);
        
        bool Connected { get; }
        EndPoint RemoteEndPoint { get; }
    }
}