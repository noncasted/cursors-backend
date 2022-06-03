using System.Collections.Generic;
using Server.Core.Rooms;

namespace Server.Features.Global.Matchmaking
{
    public class RoomsList
    {
        public RoomsList()
        {
            rooms = new Dictionary<int, Room>();
        }
        
        private readonly Dictionary<int, Room> rooms;
        private int roomsCreated = 0;

        public void Add(Room _room)
        {
            rooms.Add(_room.Id, _room);
        }

        public void Remove(Room _room)
        {
            rooms.Remove(_room.Id);
        }

        public int GetAvailableId()
        {
            roomsCreated++;
            return roomsCreated;
        }
    }
}