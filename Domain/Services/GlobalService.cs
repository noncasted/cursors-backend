namespace Domain.Services
{
    public abstract class GlobalService
    {
        public void Bind(IRouter _router)
        {
            OnBinding(_router);
        }

        protected abstract void OnBinding(IRouter _router);
    }
}