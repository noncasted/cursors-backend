using System;
using System.Numerics;

namespace Domain.Services
{
    public static class ConvertExtensions
    {
        public static byte[] AsBytes(this int _value)
        {
            Span<byte> _result = stackalloc byte[4];

            Span<byte> _buffer = BitConverter.GetBytes(_value);
            
            _result[0] = _buffer[0];
            _result[1] = _buffer[1];
            _result[2] = _buffer[2];
            _result[3] = _buffer[3];
            
            return _result.ToArray();        
        }
        
        public static byte[] AsBytes(this Vector2 _value)
        {
            byte[] a = 10.AsBytes();
            
            Span<byte> _result = stackalloc byte[8];

            Span<byte> _buffer = BitConverter.GetBytes(_value.X);
            Console.WriteLine($"Float size: {_buffer.Length}");
            _result[0] = _buffer[0];
            _result[1] = _buffer[1];
            _result[2] = _buffer[2];
            _result[3] = _buffer[3];
            
            _buffer = BitConverter.GetBytes(_value.Y);
            _result[4] = _buffer[4];
            _result[5] = _buffer[5];
            _result[6] = _buffer[6];
            _result[7] = _buffer[7];

            return _result.ToArray();        
        }

        public static byte[] AsBytes(this string _value)
        {
            int _length = _value.Length;
            Span<byte> _result = stackalloc byte[_length * 2 + 4];

            Span<byte> _buffer = BitConverter.GetBytes(_length);

            _result[0] = _buffer[0];
            _result[1] = _buffer[1];
            _result[2] = _buffer[2];
            _result[3] = _buffer[3];
            
            for (int i = 4; i < _length; i++)
            {
                _buffer = BitConverter.GetBytes(_value[i]);
                _result[i] = _buffer[0];
                _result[i + 1] = _buffer[1];
            }

            return _result.ToArray();
        }
    }
}