using System;
using System.Text;

namespace Server.Core.Connection.Packets
{
    public partial class Packet
    {
        public byte ReadByte(bool _moveReadPos = true)
        {
            if (buffer.Count > readPos)
            {
                byte _value = readableBuffer[readPos]; 
                
                if (_moveReadPos)
                    readPos += 1;

                return _value;
            }
            else
            {
                throw new Exception("Could not read value of type 'byte'!");
            }
        }

        public byte[] ReadBytes(int _length, bool _moveReadPos = true)
        {
            if (buffer.Count > readPos)
            {
                byte[] _value = buffer.GetRange(readPos, _length).ToArray();
                
                if (_moveReadPos)
                    readPos += _length;

                return _value; 
            }
            else
            {
                throw new Exception("Could not read value of type 'byte[]'!");
            }
        }

        public short ReadShort(bool _moveReadPos = true)
        {
            if (buffer.Count > readPos)
            {
                short _value = BitConverter.ToInt16(readableBuffer, readPos); 
                
                if (_moveReadPos)
                    readPos += 2; 

                return _value; 
            }
            else
            {
                throw new Exception("Could not read value of type 'short'!");
            }
        }

        public int ReadInt(bool _moveReadPos = true)
        {
            if (buffer.Count > readPos)
            {
                int _value = BitConverter.ToInt32(readableBuffer, readPos); 
                
                if (_moveReadPos)
                    readPos += 4; 

                return _value; 
            }
            else
            {
                throw new Exception("Could not read value of type 'int'!");
            }
        }
        
        public long ReadLong(bool _moveReadPos = true)
        {
            if (buffer.Count > readPos)
            {
                long _value = BitConverter.ToInt64(readableBuffer, readPos); 
                
                if (_moveReadPos)
                    readPos += 8; 

                return _value; 
            }
            else
            {
                throw new Exception("Could not read value of type 'long'!");
            }
        }
        
        public float ReadFloat(bool _moveReadPos = true)
        {
            if (buffer.Count > readPos)
            {
                float _value = BitConverter.ToSingle(readableBuffer, readPos); 
                
                if (_moveReadPos)
                    readPos += 4; 

                return _value; 
            }
            else
            {
                throw new Exception("Could not read value of type 'float'!");
            }
        }

        public bool ReadBool(bool _moveReadPos = true)
        {
            if (buffer.Count > readPos)
            {
                bool _value = BitConverter.ToBoolean(readableBuffer, readPos); 
                
                if (_moveReadPos)
                    readPos += 1; 

                return _value; 
            }
            else
            {
                throw new Exception("Could not read value of type 'bool'!");
            }
        }
        
        public string ReadString(bool _moveReadPos = true)
        {
            try
            {
                int _length = ReadInt(); 
                string _value = Encoding.ASCII.GetString(readableBuffer, readPos, _length); 
                
                if (_moveReadPos && _value.Length > 0)
                    readPos += _length; 

                return _value; 
            }
            catch
            {
                throw new Exception("Could not read value of type 'string'!");
            }
        }
    }
}