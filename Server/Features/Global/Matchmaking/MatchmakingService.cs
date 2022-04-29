using Server.Core.Connection;
using Server.Core.Routing.Routes;
using Server.Core.Services.Global;
using Server.Core.Users;

namespace Server.Features.Global.Matchmaking
{
    public class MatchmakingService : GlobalService
    {
        protected override void OnBinding(GlobalBinder _binder)
        {
            _binder.Bind(ServerRoute.Create_Room, OnCreateRoomRequested);
        }

        private void OnCreateRoomRequested(User _user, Packet _packet)
        {
            
        }
    }
}