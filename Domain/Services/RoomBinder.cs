using System.Collections.Generic;
using Domain.DataTransfer;

namespace Domain.Services
{
    public class RoomBinder
    {
        public RoomBinder()
        {
            routes = new Dictionary<ServerRoute, RouteTarget>();
        }
        
        private readonly Dictionary<ServerRoute, RouteTarget> routes;

        public void Bind(ServerRoute serverRoute, RouteTarget _target)
        {
            routes.Add(serverRoute, _target);
        }

        public RoomRoutes GetRoutes()
        {
            RoomRoutes _routes = new RoomRoutes(routes);
            
            return _routes;
        }
    }
}