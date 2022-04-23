using System.Collections.Generic;
using Domain.Connection;

namespace Domain.DataTransfer
{
    public interface IPacketSender
    {
        void SendToClient(IClient _client, TargetConnection _targetConnection, Packet _packet);
        
        void SendToAll(IReadOnlyList<IClient> _clients, TargetConnection _targetConnection, Packet _packet);
        
        void SendToOthers(IReadOnlyList<IClient> _clients, IClient _sender, TargetConnection _targetConnection, Packet _packet);
    }
}