using Domain.Connection;
using Domain.DataTransfer;

namespace Domain.Services
{
    public interface IRouter
    {
        void BindGlobal(ServerRoute serverRoute, RouteTarget _target);
        void BindLocal(int _roomId, RoomRoutes _routes);
        
        void Route(ServerRoute serverRoute, IClient _client, Packet _packet);
    }
}