namespace Server.Core.Config
{
    public static class ServerConfig
    {
        private static int maxPlayers;
        private static int port;
        
        public static int MaxPlayers => maxPlayers;
        public static int Port => port;

        public static void Configurate(int _maxPlayers, int _port)
        {
            maxPlayers = _maxPlayers;
            port = _port;
        }
    }
}