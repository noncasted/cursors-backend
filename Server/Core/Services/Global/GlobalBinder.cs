using Server.Core.Routing;
using Server.Core.Routing.Routes;

namespace Server.Core.Services.Global
{
    public class GlobalBinder
    {
        public GlobalBinder(Router _router)
        {
            router = _router;
        }
        
        private readonly Router router;
        
        public void Bind(ServerRoute _route, RouteTarget _target)
        {
            router.BindGlobal(_route, _target);    
        }
    }
}