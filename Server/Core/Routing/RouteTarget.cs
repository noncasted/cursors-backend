using Server.Core.Connection;
using Server.Core.Users;

namespace Server.Core.Routing
{
    public delegate void RouteTarget(User _client, Packet _packet);
}