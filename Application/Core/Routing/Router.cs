using System.Collections.Generic;
using Domain.Connection;
using Domain.DataTransfer;
using Domain.Services;

namespace Application.Core.Routing
{
    public class Router : IRouter
    {
        public Router()
        {
            globalRoutes = new Dictionary<string, RouteTarget>();
            roomRoutes = new Dictionary<int, RoomRoutes>();
        }
        
        private readonly Dictionary<string, RouteTarget> globalRoutes;
        private readonly Dictionary<int, RoomRoutes> roomRoutes;
        
        public void BindGlobal(string _route, RouteTarget _target)
        {
            globalRoutes.Add(_route, _target);
        }
        
        public void BindLocal(int _roomId, RoomRoutes _roomRoutes)
        {
            roomRoutes.Add(_roomId, _roomRoutes);
        }
        
        public void Route(string _route, IClient _client, Packet _packet)
        {
            if (IsGlobal(_route) == true)
                globalRoutes[_route](_client, _packet);
            else
                roomRoutes[_client.RoomId].routes[_route](_client, _packet);
        }

        private bool IsGlobal(string _route)
        {
            if (globalRoutes.ContainsKey(_route) == true)
                return true;

            return false;
        }
    }
}