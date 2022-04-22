namespace Application.Core
{
    internal static class Program
    {
        public static void Main(string[] _args)
        {
            Server.Start(50, 26950);
        }
    }
}