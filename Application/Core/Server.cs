using System;
using Application.Core.Clients;
using Application.Core.DataTransfer;
using Application.Core.ListenerProcessors;
using Application.Core.Routing;
using Domain.DataTransfer;
using Domain.Services;
using Infrastructure.Matchmaking;

namespace Application.Core
{
    public class Server
    {
        private TcpProcessor tcpProcessor;
        private UdpProcessor udpProcessor;
        
        private Router router;
        private PacketSender packetSender;
        private ServerHandle handle;
        private Matchmaker matchmaker;
        private RoomsList roomsList;
        
        private ServerClients serverClients;

        public void Start(int _maxPlayers, int _port)
        {
            Console.WriteLine("Starting server...");
            
            packetSender = new PacketSender();
            
            handle = new ServerHandle();
            router = new Router();
            roomsList = new RoomsList();
            matchmaker = new Matchmaker(roomsList, router);
            
            matchmaker.Bind(router);

            router.BindGlobal(ServerRoute.Connection, handle.OnClientConnected);
            router.BindGlobal(ServerRoute.Connection_Udp, handle.OnUdpConnection);

            serverClients = new ServerClients(_maxPlayers, packetSender, router);

            tcpProcessor = new TcpProcessor(_port, serverClients);
            udpProcessor = new UdpProcessor(_port, serverClients);

            Console.WriteLine($"Server started on {_port}");
        }
    }
}