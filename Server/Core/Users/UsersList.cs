using System.Collections.Generic;
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
                User _user = new User(i, _router);
                users.Add(i, _user);
            }
        }

        private readonly int maxClients;
        private readonly Dictionary<int, User> users = new Dictionary<int, User>();

        public bool GetFirstAvailableClient(out Client _client)
        {
            for (int i = 1; i <= maxClients; i++)
            {
                if (users[i].Client.Connected == true)
                    continue;
                
                _client = users[i].Client;
                return true;
            }

            _client = null;
            
            return false;
        }
    }
}