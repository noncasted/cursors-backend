using Domain.Connection;
using Domain.DataTransfer;

namespace Domain.Services
{
    public interface IRouter
    {
        void BindGlobal(string _route, RouteTarget _target);
        void BindLocal(int _roomId, RoomRoutes _routes);
        
        void Route(string _route, IClient _client, Packet _packet);
    }
}