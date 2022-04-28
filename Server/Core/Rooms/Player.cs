using Server.Core.Users;

namespace Server.Core.Rooms
{
    public class Player
    {
        public Player(User _user, Room _room)
        {
            user = _user;
            _user.OnRoomJoined(_room.Id);

            room = _room;
            roomId = _room.Id;
        }

        private readonly User user;
        private readonly Room room;
        private readonly int roomId;

        public void OnLeftRoom()
        {
            user.OnRoomLeft();
        }
    }
}