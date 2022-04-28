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
            _router.Bind(ServerRoute.Room_Players_Oberserver, OnPlayerMessage);
        }

        private void OnPlayerMessage(IClient _client, Packet _packet)
        {
            
        }
    }
}