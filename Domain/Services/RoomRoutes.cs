using System.Collections.Generic;
using Domain.DataTransfer;

namespace Domain.Services
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