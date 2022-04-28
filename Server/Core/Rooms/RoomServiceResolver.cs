using System;
using System.Collections.Generic;
using Server.Core.Routing;
using Server.Core.Services.Room;

namespace Server.Core.Rooms
{
    public class RoomServiceResolver
    {
        public RoomServiceResolver(Router _router)
        {
            router = _router;
        }

        private readonly Router router;
        
        public RoomService[] Resolve(RoomType _type, Room _room)
        {
            List<RoomService> _services = new List<RoomService>();
            RoomBinder _binder = new RoomBinder();
            
            switch (_type)
            {
                case RoomType.Hub:
                    break;
                case RoomType.Game:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_type), _type, null);
            }
            
            for (int i = 0; i < _services.Count; i++)
                _services[i].Bind(_binder);
            
            router.BindLocal(_room.Id, _binder.GetRoutes());

            return null;
        }
    }
}