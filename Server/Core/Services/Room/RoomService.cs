namespace Server.Core.Services.Room
{
    public abstract class RoomService
    {
        public RoomService(Rooms.Room _room)
        {
            room = _room;
        }
        
        private readonly Rooms.Room room;

        internal void Bind(RoomBinder _router)
        {
            OnBinding(_router);
        }

        internal void InvokeOnTick()
        {
            OnTick();
        }

        protected abstract void OnBinding(RoomBinder _router);
        
        protected virtual void OnTick() {}
    }
}