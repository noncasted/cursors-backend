using System;
using Application.Core.Clients;
using Application.Core.Connections;
using Application.Core.DataTransfer;

namespace Application.Core
{
    public class ServerHandle
    {
        public void WelcomeReceived(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string _userName = _packet.ReadString();
            
            Console.WriteLine($"connected successfully and is now player {_fromClient}");

            if (_fromClient != _clientIdCheck)
            {
                Console.WriteLine($"Player \"{_userName}\" (Id: {_fromClient} has assumed the wrong cliend id {_clientIdCheck}");
            }
        }

        public void UDPTestReceived(int _fromClient, Packet _packet)
        {
            string _message = _packet.ReadString();
            
            Console.WriteLine($"Received packet via UDP. Contains message: {_message}");
        }
    }
}