using System;
using System.Collections.Generic;
using Server.Core.Routing.Routes;

namespace Server.Core.Connection.Packets
{
    public partial class Packet : IDisposable
    {
        private List<byte> buffer;
        private byte[] readableBuffer;
        private int readPos;
        
        private bool disposed = false;

        public void ResetBuffers()
        {
            buffer.Clear();
            readableBuffer = Array.Empty<byte>();
            readPos = 0;
            disposed = false;
        }

        public Packet()
        {
            buffer = new List<byte>(); 
            readPos = 0; 
        }

        public Packet(int _id)
        {
            buffer = new List<byte>(); 
            readPos = 0; 

            Write(_id); 
        }

        public Packet(ClientRoute _route)
        {
            buffer = new List<byte>(); 
            readPos = 0; 

            Write((int)_route); 
        }

        public Packet(byte[] _data)
        {
            buffer = new List<byte>(); 
            readPos = 0; 

            SetBytes(_data);
        }

        public void SetBytes(byte[] _data)
        {
            Write(_data);
            readableBuffer = buffer.ToArray();
        }

        public void WriteLength()
        {
            buffer.InsertRange(0, BitConverter.GetBytes(buffer.Count)); 
        }

        public void InsertInt(int _value)
        {
            buffer.InsertRange(0, BitConverter.GetBytes(_value)); 
        }

        public byte[] ToArray()
        {
            readableBuffer = buffer.ToArray();
            return readableBuffer;
        }

        public int GetLength()
        {
            return buffer.Count; 
        }
        
        public int UnreadLength()
        {
            return GetLength() - readPos; 
        }
        
        public void Reset(bool _shouldReset = true)
        {
            if (_shouldReset == true)
            {
                buffer.Clear(); 
                readableBuffer = null;
                readPos = 0; 
            }
            else
            {
                readPos -= 4; 
            }
        }

        private void TryDispose(bool _disposing)
        {
            if (disposed == true)
                return;

            if (_disposing == false)
                return;
            
            buffer = null;
            readableBuffer = null;
            readPos = 0;

            disposed = true;
        }

        public void Dispose()
        {
            TryDispose(true);
            GC.SuppressFinalize(this);
        }
    }
}