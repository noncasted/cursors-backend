namespace Server.Core.Services.Global
{
    public abstract class GlobalService
    {
        public void Bind(GlobalBinder _router)
        {
            OnBinding(_router);
        }

        protected abstract void OnBinding(GlobalBinder _binder);

        protected virtual void OnTick() {}
    }
}