namespace Domain.Connection
{
    public interface IClient
    {
        ITcpConnection Tcp { get; }
        IUdpConnection Udp { get; }
        
        int Id { get; }
        bool InRoom { get; }
        int RoomId { get; }

        void SendData(string _route, params byte[][] _data);
        void OnRoomJoined(int _roomId);
        void OnRoomLeft();
    }
}