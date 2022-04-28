using Server.Core.Connection.Connection.Processing;
using Server.Core.Routing;
using Server.Core.Users;
using Server.Features.Global.Common.Services;

namespace Server.Core
{
    public class ServerHandler
    {
        private TcpProcessor tcpProcessor;
        private UdpProcessor udpProcessor;

        private Router router;
        private UsersList usersList;

        private GlobalServicesHolder globalServices;

        public void Start(int _maxPlayers, int _port)
        {
            router = new Router();
            usersList = new UsersList(_maxPlayers, router);

            tcpProcessor = new TcpProcessor(_port, usersList);
            udpProcessor = new UdpProcessor(_port, usersList);

            globalServices = new GlobalServicesHolder(router);
        }
    }
}