using System;
using Server.Core.Config;
using Server.Core.Connection.Connection.Handling;
using Server.Core.Processing;

namespace Server.Core
{
    internal static class Program
    {
        private static ServerHandler server;
        private static ThreadsRunner threadsRunner;
        
        public static void Main(string[] _args)
        {
            Console.WriteLine("Start server");
            
            ServerConfig.Configurate(50, 26950);

            threadsRunner = new ThreadsRunner();
            server = new ServerHandler();
            
            server.Start(ServerConfig.MaxPlayers, ServerConfig.Port);
        }
    }
}