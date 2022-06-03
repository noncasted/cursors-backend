using Server.Core.Connection;
using Server.Core.Connection.Connection.Handling;
using Server.Core.Connection.Packets;
using Server.Core.Rooms;
using Server.Core.Routing;
using Server.Core.Routing.Routes;
using Server.Core.Services.Global;
using Server.Core.Users;

namespace Server.Features.Global.Matchmaking
{
    public class MatchmakingService : GlobalService
    {
        public MatchmakingService(Router _router, RoomsList _rooms)
        {
            roomCreator = new RoomCreator(_router);

            roomsList = _rooms;
        }

        private readonly RoomCreator roomCreator;
        private readonly RoomsList roomsList;
        
        protected override void OnBinding(GlobalBinder _binder)
        {
            _binder.Bind(ServerRoute.Create_Room, OnCreateRoomRequested);
        }

        private void OnCreateRoomRequested(User _user, Packet _packet)
        {
            RoomType _type = (RoomType)_packet.ReadInt();

            Room _room = roomCreator.CreateRoom(roomsList.GetAvailableId(), _type, _user);
            
            roomsList.Add(_room);
            
            _user.SendReliable(ClientRoute.On_Room_Created, 0.AsBytes());
        }
    }
}