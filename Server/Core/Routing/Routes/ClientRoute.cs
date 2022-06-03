namespace Server.Core.Routing.Routes
{
    public enum ClientRoute
    {
        On_Connected_Tcp = 1,
        On_Connected_Udp = 2,
        
        On_Room_Created = 3,
        On_Room_Joined = 4,
        On_Room_Left = 5,
        
        On_Player_Joined = 6,
        On_Player_Left = 7,
    }
}