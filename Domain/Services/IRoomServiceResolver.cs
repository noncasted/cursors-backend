namespace Domain.Services
{
    public interface IRoomServiceResolver
    {
        RoomService[] Resolve(string _rawType, Room _room);
    }       
}