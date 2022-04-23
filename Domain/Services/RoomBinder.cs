using System.Collections.Generic;

namespace Domain.Services
{
    public class RoomBinder
    {
        public RoomBinder()
        {
            routes = new Dictionary<string, RouteTarget>();
        }
        
        private readonly Dictionary<string, RouteTarget> routes;

        public void Bind(string _route, RouteTarget _target)
        {
            routes.Add(_route, _target);
        }

        public RoomRoutes GetRoutes()
        {
            RoomRoutes _routes = new RoomRoutes(routes);
            
            return _routes;
        }
    }
}