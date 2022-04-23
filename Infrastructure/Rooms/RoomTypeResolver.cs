namespace Infrastructure.Rooms
{
    public static class RoomTypeResolver
    {
        public static RoomType TryGetRoomType(string _value)
        {
            switch (_value)
            {
                case "hub":
                    return RoomType.Hub;
                case "game":
                    return RoomType.Game;
                default:
                    return RoomType.None;
            }
        }
    }
}