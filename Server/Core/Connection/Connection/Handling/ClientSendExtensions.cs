using System.Collections.Generic;
using Server.Core.Connection.Packets;
using Server.Core.Routing.Routes;

namespace Server.Core.Connection.Connection.Handling
{
    public static class ClientSendExtensions
    {
        public static void Send(this Client _client, ClientRoute _route, PacketType _type, byte[] _bytes)
        {
            SendToClient(_client, _type, _route, _bytes);
        }
        
        public static void Send(this Client _client, ClientRoute _route, PacketType _type, byte[] _bytes0, byte[] _bytes1)
        {
            byte[] _bytes = Combine(_bytes0, _bytes1);
            
            SendToClient(_client, _type, _route, _bytes);
        }
        
        public static void Send(this Client _client, ClientRoute _route, PacketType _type, byte[] _bytes0, byte[] _bytes1, byte[] _bytes2)
        {
            byte[] _bytes = Combine(_bytes0, _bytes1, _bytes2);
            
            SendToClient(_client, _type, _route, _bytes);
        }
        
        public static void Send(this Client _client, ClientRoute _route, PacketType _type, byte[] _bytes0, byte[] _bytes1, byte[] _bytes2, byte[] _bytes3)
        {
            byte[] _bytes = Combine(_bytes0, _bytes1, _bytes2, _bytes3);
            
            SendToClient(_client, _type, _route, _bytes);
        }
        
        public static void Send(this Client _client, ClientRoute _route, PacketType _type, byte[] _bytes0, byte[] _bytes1, byte[] _bytes2, byte[] _bytes3, byte[] _bytes4)
        {
            byte[] _bytes = Combine(_bytes0, _bytes1, _bytes2, _bytes3, _bytes4);
            
            SendToClient(_client, _type, _route, _bytes);
        }
        
        public static void Send(this Client _client, ClientRoute _route, PacketType _type, byte[] _bytes0, byte[] _bytes1, byte[] _bytes2, byte[] _bytes3, byte[] _bytes4, byte[] _bytes5)
        {
            byte[] _bytes = Combine(_bytes0, _bytes1, _bytes2, _bytes3, _bytes4, _bytes5);
            
            SendToClient(_client, _type, _route, _bytes);
        }
        
        public static void SendReliable(this Client _client, ClientRoute _route, byte[] _bytes)
        {
            SendToClient(_client, PacketType.Reliable, _route, _bytes);
        }
        
        public static void SendReliable(this Client _client, ClientRoute _route, byte[] _bytes0, byte[] _bytes1)
        {
            byte[] _bytes = Combine(_bytes0, _bytes1);
            
            SendToClient(_client, PacketType.Reliable, _route, _bytes);
        }
        
        public static void SendReliable(this Client _client, ClientRoute _route, byte[] _bytes0, byte[] _bytes1, byte[] _bytes2)
        {
            byte[] _bytes = Combine(_bytes0, _bytes1, _bytes2);
            
            SendToClient(_client, PacketType.Reliable, _route, _bytes);
        }
        
        public static void SendReliable(this Client _client, ClientRoute _route, byte[] _bytes0, byte[] _bytes1, byte[] _bytes2, byte[] _bytes3)
        {
            byte[] _bytes = Combine(_bytes0, _bytes1, _bytes2, _bytes3);
            
            SendToClient(_client, PacketType.Reliable, _route, _bytes);
        }
        
        public static void SendReliable(this Client _client, ClientRoute _route, byte[] _bytes0, byte[] _bytes1, byte[] _bytes2, byte[] _bytes3, byte[] _bytes4)
        {
            byte[] _bytes = Combine(_bytes0, _bytes1, _bytes2, _bytes3, _bytes4);
            
            SendToClient(_client, PacketType.Reliable, _route, _bytes);
        }
        
        public static void SendReliable(this Client _client, ClientRoute _route, byte[] _bytes0, byte[] _bytes1, byte[] _bytes2, byte[] _bytes3, byte[] _bytes4, byte[] _bytes5)
        {
            byte[] _bytes = Combine(_bytes0, _bytes1, _bytes2, _bytes3, _bytes4, _bytes5);
            
            SendToClient(_client, PacketType.Reliable, _route, _bytes);
        }

        public static void SendUnreliable(this Client _client, ClientRoute _route, byte[] _bytes)
        {
            SendToClient(_client, PacketType.Unreliable, _route, _bytes);
        }
        
        public static void SendUnreliable(this Client _client, ClientRoute _route, byte[] _bytes0, byte[] _bytes1)
        {
            byte[] _bytes = Combine(_bytes0, _bytes1);
            
            SendToClient(_client, PacketType.Unreliable, _route, _bytes);
        }
        
        public static void SendUnreliable(this Client _client, ClientRoute _route, byte[] _bytes0, byte[] _bytes1, byte[] _bytes2)
        {
            byte[] _bytes = Combine(_bytes0, _bytes1, _bytes2);
            
            SendToClient(_client, PacketType.Unreliable, _route, _bytes);
        }
        
        public static void SendUnreliable(this Client _client, ClientRoute _route, byte[] _bytes0, byte[] _bytes1, byte[] _bytes2, byte[] _bytes3)
        {
            byte[] _bytes = Combine(_bytes0, _bytes1, _bytes2, _bytes3);
            
            SendToClient(_client, PacketType.Unreliable, _route, _bytes);
        }
        
        public static void SendUnreliable(this Client _client, ClientRoute _route, byte[] _bytes0, byte[] _bytes1, byte[] _bytes2, byte[] _bytes3, byte[] _bytes4)
        {
            byte[] _bytes = Combine(_bytes0, _bytes1, _bytes2, _bytes3, _bytes4);
            
            SendToClient(_client, PacketType.Unreliable, _route, _bytes);
        }
        
        public static void SendUnreliable(this Client _client, ClientRoute _route, byte[] _bytes0, byte[] _bytes1, byte[] _bytes2, byte[] _bytes3, byte[] _bytes4, byte[] _bytes5)
        {
            byte[] _bytes = Combine(_bytes0, _bytes1, _bytes2, _bytes3, _bytes4, _bytes5);
            
            SendToClient(_client, PacketType.Unreliable, _route, _bytes);
        }

        private static void SendToClient(Client _client, PacketType _type, ClientRoute _route, byte[] _bytes)
        {
            using (Packet _packet = new Packet(_route))
            {
                _packet.Write(_bytes);
                _client.SendData(_packet, _type);
            }
        }

        private static byte[] Combine(byte[] _bytes0, byte[] _bytes1)
        {
            byte[][] _rawBytes = new byte[2][];
            _rawBytes[0] = _bytes0;
            _rawBytes[1] = _bytes1;

            return Merge(_rawBytes);
        }
        
        private static byte[] Combine(byte[] _bytes0, byte[] _bytes1, byte[] _bytes2)
        {
            byte[][] _rawBytes = new byte[3][];
            _rawBytes[0] = _bytes0;
            _rawBytes[1] = _bytes1;
            _rawBytes[2] = _bytes2;

            return Merge(_rawBytes);
        }
        
        private static byte[] Combine(byte[] _bytes0, byte[] _bytes1, byte[] _bytes2, byte[] _bytes3)
        {
            byte[][] _rawBytes = new byte[4][];
            _rawBytes[0] = _bytes0;
            _rawBytes[1] = _bytes1;
            _rawBytes[2] = _bytes2;
            _rawBytes[3] = _bytes3;

            return Merge(_rawBytes);
        }
        
        private static byte[] Combine(byte[] _bytes0, byte[] _bytes1, byte[] _bytes2, byte[] _bytes3, byte[] _bytes4)
        {
            byte[][] _rawBytes = new byte[5][];
            _rawBytes[0] = _bytes0;
            _rawBytes[1] = _bytes1;
            _rawBytes[2] = _bytes2;
            _rawBytes[3] = _bytes3;
            _rawBytes[4] = _bytes4;

            return Merge(_rawBytes);
        }
        
        private static byte[] Combine(byte[] _bytes0, byte[] _bytes1, byte[] _bytes2, byte[] _bytes3, byte[] _bytes4, byte[] _bytes5)
        {
            byte[][] _rawBytes = new byte[6][];
            _rawBytes[0] = _bytes0;
            _rawBytes[1] = _bytes1;
            _rawBytes[2] = _bytes2;
            _rawBytes[3] = _bytes3;
            _rawBytes[4] = _bytes4;
            _rawBytes[6] = _bytes5;

            return Merge(_rawBytes);
        }

        private static byte[] Merge(IReadOnlyList<byte[]> _rawBytes)
        {
            int _length = 0;

            for (int i = 0; i < _rawBytes.Count; i++)
                _length += _rawBytes[i].Length;
            
            byte[] _bytes = new byte[_length];
            
            int _passedLength = 0;
            
            for (int i = 0; i < _rawBytes.Count; i++)
            {
                for (int j = 0; j < _rawBytes[i].Length; j++)
                    _bytes[_passedLength + j] = _rawBytes[i][j];

                _passedLength += _rawBytes[i].Length;
            }

            return _bytes;
        }
    }
}