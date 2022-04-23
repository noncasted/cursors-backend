using System;
using System.Collections.Generic;
using Domain.Services;

namespace Infrastructure.Rooms
{
    public class RoomServiceResolver : IRoomServiceResolver
    {
        public RoomService[] Resolve(string _rawType, Room _room)
        {
            RoomType _roomType = RoomTypeResolver.TryGetRoomType(_rawType);

            List<RoomService> _roomServices = new List<RoomService>();

            switch (_roomType)
            {
                case RoomType.None:
                    break;
                case RoomType.Hub:
                    _roomServices.Add(new RoomPlayersObserverService(_room));
                    break;
                case RoomType.Game:
                    _roomServices.Add(new RoomPlayersObserverService(_room));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return _roomServices.ToArray();
        }
    }
}