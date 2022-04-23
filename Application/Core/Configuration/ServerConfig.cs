using Domain.Connection;

namespace Application.Core.Configuration
{
    public static class ServerConfig
    {
        private static int maxPlayers;
        private static int port;
        private static TargetConnection defaultConnection;

        public static int MaxPlayers => maxPlayers;
        public static int Port => port;
        public static TargetConnection DefaultConnection => defaultConnection;

        public static void Configurate(int _maxPlayers, int _port, TargetConnection _defaultConnection)
        {
            maxPlayers = _maxPlayers;
            port = _port;
            defaultConnection = _defaultConnection;
        }
    }
}