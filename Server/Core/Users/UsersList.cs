using System.Collections.Generic;
using Server.Core.Config;
using Server.Core.Connection.Connection.Handling;
using Server.Core.Routing;

namespace Server.Core.Users
{
    public class UsersList
    {
        public UsersList(int _maxClients, Router _router)
        {
            maxClients = _maxClients;

            for (int i = 1; i <= maxClients; i++)
            {
                User _user = new User(i, ServerConfig.DefaultConnection, _router);
                users.Add(i, _user);
            }
        }

        private readonly int maxClients;
        private readonly Dictionary<int, User> users = new Dictionary<int, User>();

        public User GetUser(int _userId)
        {
            return users[_userId];
        }
        
        public bool GetFirstAvailableClient(out Client _client)
        {
            for (int i = 1; i <= maxClients; i++)
            {
                if (users[i].Client.Tcp.Connected == true || users[i].Client.Udp.Connected == true)
                    continue;
                
                _client = users[i].Client;
                return true;
            }

            _client = null;
            
            return false;
        }
    }
}