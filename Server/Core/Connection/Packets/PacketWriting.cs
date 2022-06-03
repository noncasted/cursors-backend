using System;
using System.Text;

namespace Server.Core.Connection.Packets
{
    public partial class Packet
    {
        public void Write(byte _value)
        {
            buffer.Add(_value);
        }

        public void Write(byte[] _value)
        {
            buffer.AddRange(_value);
        }

        public void Write(short _value)
        {
            buffer.AddRange(BitConverter.GetBytes(_value));
        }

        public void Write(int _value)
        {
            buffer.AddRange(BitConverter.GetBytes(_value));
        }

        public void Write(long _value)
        {
            buffer.AddRange(BitConverter.GetBytes(_value));
        }

        public void Write(float _value)
        {
            buffer.AddRange(BitConverter.GetBytes(_value));
        }

        public void Write(bool _value)
        {
            buffer.AddRange(BitConverter.GetBytes(_value));
        }

        public void Write(string _value)
        {
            Write(_value.Length); 
            buffer.AddRange(Encoding.ASCII.GetBytes(_value)); 
        }
    }
}