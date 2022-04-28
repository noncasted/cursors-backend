using System;

namespace Server.Core.Processing
{
    public readonly struct ExecutablePacket
    {
        public ExecutablePacket(Action<byte[]> _action, byte[] _data)
        {
            action = _action;
            data = _data;
        }
        
        private readonly Action<byte[]> action;
        private readonly byte[] data;

        public void Invoke()
        {
            action.Invoke(data);
        }
    }
}