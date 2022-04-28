using System;
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
        
        public void Route(ServerRoute serverRoute, IClient _client, Packet _packet)
        {
            Console.WriteLine($"New route: {serverRoute}");
            
            if (IsGlobal(serverRoute) == true)
                globalRoutes[serverRoute](_client, _packet);
            else
                roomRoutes[_client.RoomId].routes[serverRoute](_client, _packet);
        }

        private bool IsGlobal(ServerRoute serverRoute)
        {
            if (globalRoutes.ContainsKey(serverRoute) == true)
                return true;

            return false;
        }
    }
}