using Application.Core.Converting;
using Domain.Connection;

namespace Domain.Services
{
    public class Player
    {
        public Player(IClient _owner, Room _current, int _inRoomId)
        {
            owner = _owner;
            current = _current;
            inRoomId = _inRoomId;
            
            owner.OnRoomJoined(_current.RoomId);
            owner.SendData("on-room-joined", _current.RoomId.AsBytes());
        }
        
        private readonly IClient owner;
        private readonly Room current;
        private readonly int inRoomId;
        
        public void OnRoomLeaved()
        {
            owner.OnRoomLeft();
        }
    }
}