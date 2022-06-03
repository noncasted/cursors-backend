using Server.Core.Connection.Packets;

namespace Server.Core.Connection.Connection.Handling
{
    public interface IConnection
    {
        void SendData(Packet _packet, PacketType _type);

        void Disconnect();
    }
}