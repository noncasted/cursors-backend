using System.Collections.Generic;

namespace Domain.Services
{
    public class RoomRoutes
    {
        public RoomRoutes(Dictionary<string, RouteTarget> _routes)
        {
            routes = _routes;
        }
        
        public readonly Dictionary<string, RouteTarget> routes;
    }
}