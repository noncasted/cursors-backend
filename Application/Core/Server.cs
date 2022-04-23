using System;
using Application.Core.Clients;
using Application.Core.DataTransfer;
using Application.Core.ListenerProcessors;
using Application.Core.Routing;

namespace Application.Core
{
    public class Server
    {
        private TcpProcessor tcpProcessor;
        private UdpProcessor udpProcessor;
        
        private Router router;
        private PacketSender packetSender;
        private ServerHandle handle;
        
        private ServerClients serverClients;

        public void Start(int _maxPlayers, int _port)
        {
            Console.WriteLine("Starting server...");
            
            packetSender = new PacketSender();
            
            handle = new ServerHandle();

            router = new Router();
            router.BindRoute((int)ClientPackets.welcomeReceived, handle.WelcomeReceived);
            router.BindRoute((int)ClientPackets.udpTestReceived, handle.UDPTestReceived);

            serverClients = new ServerClients(_maxPlayers, packetSender, router);

            udpProcessor = new UdpProcessor(_port, serverClients);
            tcpProcessor = new TcpProcessor(_port, serverClients);

            Console.WriteLine($"Server started on {_port}");
        }
    }
}