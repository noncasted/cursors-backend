using Server.Core.Processing;

namespace Server.Features.Execution.Threads
{
    public class ConnectionsThread : ProcessableThread
    {
        public ConnectionsThread(float _ticksPerSecond) : base(_ticksPerSecond)
        {
        }

        protected override void OnTick()
        {
            ThreadManager.ExecuteConnections();
        }
    }
}