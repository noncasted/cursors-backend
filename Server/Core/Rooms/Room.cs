using System.Collections.Generic;
using Server.Core.Services.Room;
using Server.Core.Users;

namespace Server.Core.Rooms
{
    public class Room
    {
        public Room(int id, RoomType _roomType, RoomServiceResolver _resolver)
        {
            Id = id;
            
            players = new Dictionary<int, Player>();
            services = _resolver.Resolve(_roomType, this);
        }
        
        private readonly Dictionary<int, Player> players;
        private readonly RoomService[] services;

        public readonly int Id;

        public void AddPlayer(User _user)
        {
            int _inRoomId = players.Count + 1;
            Player _player = new Player(_user, this);
            players.Add(_inRoomId, _player);
        }

        public void RemovePlayer(Player _player)
        {
            
        }

        public void OnTick()
        {
            for (int i = 0; i < services.Length; i++)
                services[i].InvokeOnTick();
        }
    }
}