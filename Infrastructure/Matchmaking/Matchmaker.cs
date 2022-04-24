using System;
using Domain.Connection;
using Domain.DataTransfer;
using Domain.Services;
using Infrastructure.Rooms;

namespace Infrastructure.Matchmaking
{
    public class Matchmaker : GlobalService
    {
        public Matchmaker(RoomsList _rooms, IRouter _router)
        {
            roomsList = _rooms;
            serviceResolver = new RoomServiceResolver();
            router = _router;
        }
        
        private readonly RoomsList roomsList;
        private readonly RoomServiceResolver serviceResolver;
        private readonly IRouter router;

        private int roomsCreated = 0;

        protected override void OnBinding(IRouter _router)
        {
            _router.BindGlobal("create-room", CreateRoom);
        }
        
        private void CreateRoom(IClient _client, Packet _packet)
        {
            string _roomType = _packet.ReadString();
            
            Console.WriteLine("Try create room " + _roomType);
            
            if (_client.InRoom == true)
            {
                Console.WriteLine("Trying to create room whilr being in one");
                return;
            }

            roomsCreated++;

            Room _room = new Room(roomsCreated, _roomType, serviceResolver, router);
            _room.AddPlayer(_client);
            
            roomsList.Add(_room);
        }
    }
}