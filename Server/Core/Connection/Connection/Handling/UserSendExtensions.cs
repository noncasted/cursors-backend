using Server.Core.Routing.Routes;
using Server.Core.Users;

namespace Server.Core.Connection.Connection.Handling
{
    public static class UserSendExtensions
    {
        public static void Send(this User _user, ClientRoute _route, PacketType _type, byte[] _bytes)
        {
            _user.Client.Send(_route, _type, _bytes);
        }
        
        public static void Send(this User _user, ClientRoute _route, PacketType _type, byte[] _bytes0, byte[] _bytes1)
        {
            _user.Client.Send(_route, _type, _bytes0, _bytes1);
        }
        
        public static void Send(this User _user, ClientRoute _route, PacketType _type, byte[] _bytes0, byte[] _bytes1, byte[] _bytes2)
        {
            _user.Client.Send(_route, _type, _bytes0, _bytes1, _bytes2);
        }
        
        public static void Send(this User _user, ClientRoute _route, PacketType _type, byte[] _bytes0, byte[] _bytes1, byte[] _bytes2, byte[] _bytes3)
        {
            _user.Client.Send(_route, _type, _bytes0, _bytes1, _bytes2, _bytes3);
        }
        
        public static void Send(this User _user, ClientRoute _route, PacketType _type, byte[] _bytes0, byte[] _bytes1, byte[] _bytes2, byte[] _bytes3, byte[] _bytes4)
        {
            _user.Client.Send(_route, _type, _bytes0, _bytes1, _bytes2, _bytes3, _bytes4);
        }
        
        public static void Send(this User _user, ClientRoute _route, PacketType _type, byte[] _bytes0, byte[] _bytes1, byte[] _bytes2, byte[] _bytes3, byte[] _bytes4, byte[] _bytes5)
        {
            _user.Client.Send(_route, _type, _bytes0, _bytes1, _bytes2, _bytes3, _bytes4, _bytes5);
        }
        
        
        public static void SendReliable(this User _user, ClientRoute _route, byte[] _bytes)
        {
            _user.Client.SendReliable(_route, _bytes);
        }
        
        public static void SendReliable(this User _user, ClientRoute _route, byte[] _bytes0, byte[] _bytes1)
        {
            _user.Client.SendReliable(_route, _bytes0, _bytes1);
        }
        
        public static void SendReliable(this User _user, ClientRoute _route, byte[] _bytes0, byte[] _bytes1, byte[] _bytes2)
        {
            _user.Client.SendReliable(_route, _bytes0, _bytes1, _bytes2);
        }
        
        public static void SendReliable(this User _user, ClientRoute _route, byte[] _bytes0, byte[] _bytes1, byte[] _bytes2, byte[] _bytes3)
        {
            _user.Client.SendReliable(_route, _bytes0, _bytes1, _bytes2, _bytes3);
        }
        
        public static void SendReliable(this User _user, ClientRoute _route, byte[] _bytes0, byte[] _bytes1, byte[] _bytes2, byte[] _bytes3, byte[] _bytes4)
        {
            _user.Client.SendReliable(_route, _bytes0, _bytes1, _bytes2, _bytes3, _bytes4);
        }
        
        public static void SendReliable(this User _user, ClientRoute _route, byte[] _bytes0, byte[] _bytes1, byte[] _bytes2, byte[] _bytes3, byte[] _bytes4, byte[] _bytes5)
        {
            _user.Client.SendReliable(_route, _bytes0, _bytes1, _bytes2, _bytes3, _bytes4, _bytes5);
        }

        public static void SendUnreliable(this User _user, ClientRoute _route, byte[] _bytes)
        {
            _user.Client.SendReliable(_route, _bytes);
        }
        
        public static void SendUnreliable(this User _user, ClientRoute _route, byte[] _bytes0, byte[] _bytes1)
        {
            _user.Client.SendReliable(_route, _bytes0, _bytes1);
        }
        
        public static void SendUnreliable(this User _user, ClientRoute _route, byte[] _bytes0, byte[] _bytes1, byte[] _bytes2)
        {
            _user.Client.SendReliable(_route, _bytes0, _bytes1, _bytes2);
        }
        
        public static void SendUnreliable(this User _user, ClientRoute _route, byte[] _bytes0, byte[] _bytes1, byte[] _bytes2, byte[] _bytes3)
        {
            _user.Client.SendReliable(_route, _bytes0, _bytes1, _bytes2, _bytes3);
        }
        
        public static void SendUnreliable(this User _user, ClientRoute _route, byte[] _bytes0, byte[] _bytes1, byte[] _bytes2, byte[] _bytes3, byte[] _bytes4)
        {
            _user.Client.SendReliable(_route, _bytes0, _bytes1, _bytes2, _bytes3, _bytes4);
        }
        
        public static void SendUnreliable(this User _user, ClientRoute _route, byte[] _bytes0, byte[] _bytes1, byte[] _bytes2, byte[] _bytes3, byte[] _bytes4, byte[] _bytes5)
        {
            _user.Client.SendReliable(_route, _bytes0, _bytes1, _bytes2, _bytes3, _bytes4, _bytes5);
        }
    }
}