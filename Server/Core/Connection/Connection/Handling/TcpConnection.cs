using System;
using System.Net.Sockets;
using Server.Core.Config;
using Server.Core.Connection.Packets;
using Server.Core.Processing;
using Server.Core.Routing.Routes;

namespace Server.Core.Connection.Connection.Handling
{
    public class TcpConnection : IConnection
    {
        public TcpConnection(Action<Packet> _receivingCallback, Action _disconectCallback)
        {
            dataBufferSize = Constants.TcpBufferSize;
            receiveBuffer = new byte[dataBufferSize];

            receivingCallback = _receivingCallback;
            disconnectCallback = _disconectCallback;
        }

        private readonly int dataBufferSize;
        private readonly byte[] receiveBuffer;
        
        private readonly Action<Packet> receivingCallback;
        private readonly Action disconnectCallback;

        private readonly Packet receivedData = new Packet();

        private bool connected = false;

        private TcpClient socket;
        private NetworkStream stream;

        public void Connect(TcpClient _socket, int _id)
        {
            if (_socket == null)
            {
                Console.WriteLine($"Trying to connect client with empty socket");
                return;
            }
            
            connected = true;

            socket = _socket;
            socket.ReceiveBufferSize = dataBufferSize;
            socket.SendBufferSize = dataBufferSize;

            stream = socket.GetStream();
            stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);

            using (Packet _packet = new Packet(ClientRoute.On_Connected_Tcp))
            {
                _packet.Write("Connected to the server");
                _packet.Write(_id);
                
                SendData(_packet, PacketType.Reliable);
            }
        }
        
        public void SendData(Packet _packet, PacketType _type)
        {
            _packet.WriteLength();
            
            if (connected == false)
                return;

            byte[] _bytes = _packet.ToArray();

            if (_bytes.Length >= socket.SendBufferSize)
            {
                Console.WriteLine($"Buffer size is bigger than send buffer size: data size: {_bytes.Length} buffer size: {socket.SendBufferSize}");
                return;
            }

            if (stream.CanWrite == false)
            {
                Console.WriteLine($"Stream writing is unavailable");
                return;
            }

            stream.BeginWrite(_packet.ToArray(), 0, _packet.GetLength(), null, null);
        }

        private void ReceiveCallback(IAsyncResult _result)
        {
            int _byteLength = stream.EndRead(_result);
            
            if (_byteLength <= 0)
            {
                disconnectCallback?.Invoke();

                return;
            }

            byte[] _data = new byte[_byteLength];
            
            Array.Copy(receiveBuffer, _data, _byteLength);

            receivedData.Reset(HandleData(_data));

            if (stream.CanRead == false)
            {
                Console.WriteLine($"Stream reading is unavailable");
                disconnectCallback?.Invoke();
                return;
            }

            stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
        }

        private bool HandleData(byte[] _data)
        {
            int _packetLength = 0;

            receivedData.SetBytes(_data);
            
            if (receivedData.UnreadLength() >= 4)
            {
                _packetLength = receivedData.ReadInt();

                if (_packetLength <= 0)
                    return true;
            }
            
            while (_packetLength > 0 && _packetLength <= receivedData.UnreadLength())
            {
                byte[] _packetBytes = receivedData.ReadBytes(_packetLength);

                ThreadManager.AddConnectionTask(OnDataReceived, _packetBytes);

                _packetLength = 0;

                if (receivedData.UnreadLength() >= 4)
                {
                    _packetLength = receivedData.ReadInt();

                    if (_packetLength <= 0)
                        return true;
                }
            }

            if (_packetLength <= 1)
            {
                return true;
            }

            return false;
        }

        private void OnDataReceived(byte[] _bytes)
        {
            using (Packet _packet = new Packet(_bytes))
            {
                receivingCallback?.Invoke(_packet);
            }
        }

        public void Disconnect()
        {
            socket.Close();
            stream = null;
            receivedData.ResetBuffers();
            connected = false;

            for (int i = 0; i < receiveBuffer.Length; i++)
                receiveBuffer[i] = 0;

            socket = null;
        }
    }
}