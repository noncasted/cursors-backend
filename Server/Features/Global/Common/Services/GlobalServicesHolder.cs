using System.Collections.Generic;
using Server.Core.Routing;
using Server.Core.Services.Global;
using Server.Features.Global.Connector;
using Server.Features.Global.Matchmaking;

namespace Server.Features.Global.Common.Services
{
    public class GlobalServicesHolder
    {
        public GlobalServicesHolder(Router _router)
        {
            services = new List<GlobalService>();

            RoomsList _roomsList = new RoomsList();
            
            services.Add(new ConnectorService());
            services.Add(new MatchmakingService(_router, _roomsList));
            
            GlobalBinder _binder = new GlobalBinder(_router);
            
            for (int i = 0; i < services.Count; i++)
                services[i].Bind(_binder);
        }

        private readonly List<GlobalService> services;
    }
}