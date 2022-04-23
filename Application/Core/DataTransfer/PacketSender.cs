using System.Collections.Generic;
using Application.Core.Clients;
using Application.Core.Configuration;

namespace Application.Core.DataTransfer
{
    public class PacketSender
    {
        private void SendTCPData(Client _client, Packet _packet)
        {
            _packet.WriteLength();
            
            _client.Tcp.SendData(_packet);
        }

        private void SendUDPData(Client _client, Packet _packet)
        {
            _packet.WriteLength();
            _client.Udp.SendData(_packet);
        }

        private void SendTCPDataToAll(IReadOnlyDictionary<int, Client> _clients, Packet _packet)
        {
            _packet.WriteLength();

            for (int i = 1; i <= ServerConfig.MaxPlayers; i++)
                _clients[i].Tcp.SendData(_packet);
        }
        
        private void SendTCPDataToAll(int _exceptId, IReadOnlyDictionary<int, Client> _clients, Packet _packet)
        {
            _packet.WriteLength();

            for (int i = 1; i <= ServerConfig.MaxPlayers; i++)
            {
                if (i == _exceptId)
                    continue;
                
                _clients[i].Tcp.SendData(_packet);
            }        
        }
        
        private void SendUDPDataToAll(IReadOnlyDictionary<int, Client> _clients, Packet _packet)
        {
            _packet.WriteLength();

            for (int i = 1; i <= ServerConfig.MaxPlayers; i++)
                _clients[i].Udp.SendData(_packet);    
        }
        
        private void SendUDPDataToAll(int _exceptId, IReadOnlyDictionary<int, Client> _clients, Packet _packet)
        {
            _packet.WriteLength();

            for (int i = 1; i <= ServerConfig.MaxPlayers; i++)
            {
                if (i == _exceptId)
                    continue;
                
                _clients[i].Udp.SendData(_packet);    
            }        
        }

        public void Welcome(Client _client, string _message)
        {
            using (Packet _packet = new Packet((int)ServerPackets.welcome))
            {
                _packet.Write(_message);
                _packet.Write(_client.Id);

                SendTCPData(_client, _packet);
            }
        }

        public void UDPTest(Client _client)
        {
            using (Packet _packet = new Packet((int)ServerPackets.udpTest))
            {
                _packet.Write("Test packet for UDP");
                
                SendUDPData(_client, _packet);
            }
        }
    }
}