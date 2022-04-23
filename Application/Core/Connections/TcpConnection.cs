using System;
using System.Net;
using System.Net.Sockets;
using Application.Core.Clients;
using Application.Core.Configuration;
using Application.Core.DataTransfer;
using Application.Core.Routing;

namespace Application.Core.Connections
{
    public class TcpConnection
    {
        public TcpConnection(Client _owner, PacketSender _packetSender, Router _router)
        {
            id = _owner.Id;
            dataBufferSize = Constants.TcpBufferSize;
            receiveBuffer = new byte[dataBufferSize];
            packetSender = _packetSender;
            owner = _owner;
            router = _router;
        }

        private readonly int dataBufferSize;
        private readonly int id;

        private readonly Packet receivedData = new Packet();
        private readonly byte[] receiveBuffer;
        private readonly PacketSender packetSender;
        private readonly Router router;
        private readonly Client owner;

        private bool connected = false;

        private TcpClient socket;
        private NetworkStream stream;

        public bool Connected => connected;
        public EndPoint RemoteEndPoint => socket.Client.RemoteEndPoint;

        public void Connect(TcpClient _socket)
        {
            if (_socket == null)
            {
                Console.WriteLine($"Trying to connect client(id: {id}) with empty socket");
                return;
            }

            connected = true;

            socket = _socket;
            socket.ReceiveBufferSize = dataBufferSize;
            socket.SendBufferSize = dataBufferSize;

            stream = socket.GetStream();
            stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);

            packetSender.Welcome(owner, "Welcome to the server");
            //PacketSender.Welcome(id, "Welcome to the server");
            Console.WriteLine("Welcome sent");
        }

        public void SendData(Packet _packet)
        {
            if (connected == false)
                return;

            byte[] _buffer = _packet.ToArray();

            if (_buffer.Length >= socket.SendBufferSize)
            {
                Console.WriteLine(
                    $"Buffer size is bigger than send buffer size: data size: {_buffer.Length} buffer size: {socket.SendBufferSize}");
                return;
            }

            if (stream.CanWrite == false)
            {
                Console.WriteLine($"Stream writing is unavailable");
                return;
            }

            stream.BeginWrite(_packet.ToArray(), 0, _packet.Length(), null, null);
        }

        private void ReceiveCallback(IAsyncResult _result)
        {
            int _byteLength = stream.EndRead(_result);

            if (_byteLength <= 0)
            {
                // TODO: Disconnect

                return;
            }

            byte[] _data = new byte[_byteLength];

            Array.Copy(receiveBuffer, _data, _byteLength);

            receivedData.Reset(HandleData(_data));

            if (stream.CanRead == false)
            {
                Console.WriteLine($"Stream writing is unavailable");
                return;
            }

            Console.WriteLine("Packet received");


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

                ThreadManager.ExecuteOnMainThread(() =>
                {
                    using (Packet _packet = new Packet(_packetBytes))
                    {
                        int _packetId = _packet.ReadInt();
                        router.Route(_packetId, id, _packet);
                    }
                });

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
    }
}