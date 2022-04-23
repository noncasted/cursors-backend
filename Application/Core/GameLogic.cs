using Application.Core.DataTransfer;

namespace Application.Core
{
    public class GameLogic
    {
        public static void Update()
        {
            ThreadManager.UpdateMain();
        }
    }
}