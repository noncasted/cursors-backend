using System;
using System.Numerics;

namespace Server.Core.Connection
{
    public static class Converter
    {
        public static byte[] AsBytes(this int _value)
        {
            return BitConverter.GetBytes(_value);
        }
        
        public static byte[] AsBytes(this long _value)
        {
            return BitConverter.GetBytes(_value);
        }
        
        public static byte[] AsBytes(this float _value)
        {
            return BitConverter.GetBytes(_value);
        }
        
        public static byte[] AsBytes(this Vector2 _value)
        {
            byte[] _x = BitConverter.GetBytes(_value.X);
            byte[] _y = BitConverter.GetBytes(_value.Y);

            byte[] _result = new byte[8];

            _result[0] = _x[0];
            _result[1] = _x[1];
            _result[2] = _x[2];
            _result[3] = _x[3];
            
            _result[4] = _y[4];
            _result[5] = _y[5];
            _result[6] = _y[6];
            _result[7] = _y[7];

            return _result;
        }
        
        public static byte[] AsBytes(this Vector3 _value)
        {
            byte[] _x = BitConverter.GetBytes(_value.X);
            byte[] _y = BitConverter.GetBytes(_value.Y);
            byte[] _z = BitConverter.GetBytes(_value.Z);

            byte[] _result = new byte[12];

            _result[0] = _x[0];
            _result[1] = _x[1];
            _result[2] = _x[2];
            _result[3] = _x[3];
            
            _result[4] = _y[4];
            _result[5] = _y[5];
            _result[6] = _y[6];
            _result[7] = _y[7];
            
            _result[8] = _y[8];
            _result[9] = _y[9];
            _result[10] = _y[10];
            _result[11] = _y[11];

            return _result;
        }
    }
}