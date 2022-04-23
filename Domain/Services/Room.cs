using System.Collections.Generic;
using Domain.Connection;

namespace Domain.Services
{
    public class Room
    {
        public Room(int _roomId, string _roomType, IRoomServiceResolver _resolver, IRouter _router)
        {
            RoomId = _roomId;
            
            players = new Dictionary<int, Player>();
            services = _resolver.Resolve(_roomType, this);

            RoomBinder _binder = new RoomBinder();
            
            for (int i = 0; i < services.Length; i++) 
                services[i].Bind(_binder);
            
            _router.BindLocal(_roomId, _binder.GetRoutes());
        }
        
        private readonly Dictionary<int, Player> players;
        private readonly RoomService[] services;

        public readonly int RoomId;

        public void AddPlayer(IClient _client)
        {
            int _inRoomId = players.Count + 1;
            Player _player = new Player(_client, this, _inRoomId);
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