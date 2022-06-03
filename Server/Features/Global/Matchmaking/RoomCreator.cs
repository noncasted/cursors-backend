using Server.Core.Rooms;
using Server.Core.Routing;
using Server.Core.Users;

namespace Server.Features.Global.Matchmaking
{
    public class RoomCreator
    {
        public RoomCreator(Router _router)
        {
            serviceResolver = new RoomServiceResolver(_router);
        }

        private readonly RoomServiceResolver serviceResolver;
        
        public Room CreateRoom(int _id, RoomType _type, User _user)
        {
            Room _room = new Room(_id, _type, serviceResolver);

            _room.AddPlayer(_user);

            return _room;
        }
    }
}