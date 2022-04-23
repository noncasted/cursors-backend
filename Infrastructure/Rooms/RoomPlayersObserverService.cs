using Domain.Connection;
using Domain.DataTransfer;
using Domain.Services;

namespace Infrastructure.Rooms
{
    public class RoomPlayersObserverService : RoomService
    {
        public RoomPlayersObserverService(Room _room) : base(_room)
        {
        }

        protected override void OnBinding(RoomBinder _router)
        {
            _router.Bind("players-observer", OnPlayerMessage);
        }

        private void OnPlayerMessage(IClient _client, Packet _packet)
        {
            
        }
    }
}