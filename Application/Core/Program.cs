using System;
using System.Threading;
using Application.Core.Configuration;
using Domain.Connection;

namespace Application.Core
{
    internal static class Program
    {
        private static bool isRunning = false;

        private static Server server;
        
        public static void Main(string[] _args)
        {
            ServerConfig.Configurate(50, 26950, TargetConnection.TCP);
            
            isRunning = true;

            Thread _mainThread = new Thread(new ThreadStart(MainThread));
            _mainThread.Start();

            server = new Server();
            
            server.Start(ServerConfig.MaxPlayers, ServerConfig.Port);
            Console.ReadKey();
        }

        private static void MainThread()
        {
            Console.WriteLine($"Main thread started. Running at {Constants.TickPerSecond} ticks per second");
            
            DateTime _nextLoop = DateTime.Now;

            while (isRunning == true)
            {
                while (_nextLoop < DateTime.Now)
                {
                    GameLogic.Update();

                    _nextLoop = _nextLoop.AddMilliseconds(Constants.MsPerTick);
                }

                if (_nextLoop > DateTime.Now)
                {
                    Thread.Sleep(_nextLoop - DateTime.Now);
                }
            }
        }
    }
}