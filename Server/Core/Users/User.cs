using Server.Core.Connection;
using Server.Core.Connection.Connection.Handling;
using Server.Core.Routing;
using Server.Core.Routing.Routes;

namespace Server.Core.Users
{
    public class User
    {
        public User(int _clientID, TargetConnection _defaultConnection, Router _router)
        {
            Client = new Client(_clientID, _defaultConnection, OnDataReceived);
            router = _router;
        }
        
        public readonly Client Client;
        
        private readonly Router router;

        private bool inRoom;
        private int roomId;

        public bool InRoom => inRoom;
        public int RoomId => roomId;
        
        private void OnDataReceived(Packet _packet)
        {
            ServerRoute _route = (ServerRoute)_packet.ReadInt();
            
            router.Route(_route, this, _packet);
        }

        public void OnRoomJoined(int _roomId)
        {
            inRoom = true;
            roomId = _roomId;
        }

        public void OnRoomLeft()
        {
            inRoom = false;
            roomId = -1;
        }
    }
}