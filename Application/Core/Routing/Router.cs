using System.Collections.Generic;
using Application.Core.DataTransfer;

namespace Application.Core.Routing
{
    public class Router
    {
        public Router()
        {
            routes = new Dictionary<int, RouteTarget>();
        }
        
        public delegate void RouteTarget(int _senderClient, Packet _packet);
        
        private static Dictionary<int, RouteTarget> routes;

        public void Route(int _route, int _clientId, Packet _packet)
        {
            routes[_route](_clientId, _packet);
        }

        public void BindRoute(int _route, RouteTarget _target)
        {
            routes.Add(_route, _target);
        }
    }
}