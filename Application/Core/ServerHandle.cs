using System;
using Domain.Connection;
using Domain.DataTransfer;

namespace Application.Core
{
    public class ServerHandle
    {
        public void WelcomeReceived(IClient _client, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string _userName = _packet.ReadString();
            
            Console.WriteLine($"{_userName} connected successfully and is now player {_client.Id}");

            if (_client.Id != _clientIdCheck)
            {
                Console.WriteLine($"Player \"{_userName}\" (Id: {_client.Id} has assumed the wrong cliend id {_clientIdCheck}");
            }
        }

        public void UDPTestReceived(IClient _client, Packet _packet)
        {
            string _message = _packet.ReadString();
            
            Console.WriteLine($"Received packet via UDP. Contains message: {_message}");
        }
    }
}