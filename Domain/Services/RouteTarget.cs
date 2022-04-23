using Domain.Connection;
using Domain.DataTransfer;

namespace Domain.Services
{
    public delegate void RouteTarget(IClient _client, Packet _packet);
}