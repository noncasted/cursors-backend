using System.Collections.Generic;
using Server.Core.Routing.Routes;

namespace Server.Core.Routing
{
    public class RoomRoutes
    {
        public RoomRoutes(Dictionary<ServerRoute, RouteTarget> _routes)
        {
            routes = _routes;
        }
        
        public readonly Dictionary<ServerRoute, RouteTarget> routes;
    }
}