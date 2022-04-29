using System.Collections.Generic;
using Server.Core.Connection;
using Server.Core.Routing.Routes;
using Server.Core.Users;

namespace Server.Core.Routing
{
    public class Router
    {
        public Router()
        {
            globalRoutes = new Dictionary<ServerRoute, RouteTarget>();
            roomRoutes = new Dictionary<int, RoomRoutes>();
        }
        
        private readonly Dictionary<ServerRoute, RouteTarget> globalRoutes;
        private readonly Dictionary<int, RoomRoutes> roomRoutes;
        
        public void BindGlobal(ServerRoute serverRoute, RouteTarget _target)
        {
            globalRoutes.Add(serverRoute, _target);
        }
        
        public void BindLocal(int _roomId, RoomRoutes _roomRoutes)
        {
            roomRoutes.Add(_roomId, _roomRoutes);
        }
        
        public void Route(ServerRoute serverRoute, User _user, Packet _packet)
        {
            if (IsGlobal(serverRoute) == true)
                globalRoutes[serverRoute](_user, _packet);
            else
                roomRoutes[_user.RoomId].routes[serverRoute](_user, _packet);
        }
        
        private bool IsGlobal(ServerRoute serverRoute)
        {
            if (globalRoutes.ContainsKey(serverRoute) == true)
                return true;

            return false;
        }
    }
}