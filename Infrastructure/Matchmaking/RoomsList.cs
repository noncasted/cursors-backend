using System.Collections.Generic;
using Domain.Services;

namespace Infrastructure.Matchmaking
{
    public class RoomsList
    {
        public RoomsList()
        {
            rooms = new List<Room>();
        }
        
        private readonly List<Room> rooms;

        public void Add(Room _room)
        {
            
        }

        public void Remove(Room _room)
        {
            
        }
    }
}