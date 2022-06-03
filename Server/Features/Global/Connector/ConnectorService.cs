using System;
using Server.Core.Connection;
using Server.Core.Connection.Packets;
using Server.Core.Routing.Routes;
using Server.Core.Services.Global;
using Server.Core.Users;

namespace Server.Features.Global.Connector
{
    public class ConnectorService : GlobalService
    {
        protected override void OnBinding(GlobalBinder _router)
        {
            _router.Bind(ServerRoute.On_Connected_Tcp, OnConnectedTcp);
            _router.Bind(ServerRoute.On_Connected_Udp, OnConnectedUdp);
        }

        private void OnConnectedTcp(User _user, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string _userName = _packet.ReadString();
            
            Console.WriteLine($"{_userName} connected successfully and is now player {_user.Client.Id}");

            if (_user.Client.Id != _clientIdCheck)
            {
                Console.WriteLine($"Player \"{_userName}\" (Id: {_user.Client.Id} has assumed the wrong cliend id {_clientIdCheck}");
            }
        }
        
        private void OnConnectedUdp(User _user, Packet _packet)
        {
            string _message = _packet.ReadString();
            
            Console.WriteLine($"Received packet via UDP. Contains message: {_message}");
        }
    }
}