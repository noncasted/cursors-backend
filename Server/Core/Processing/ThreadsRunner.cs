using Server.Core.Config;
using Server.Features.Execution.Threads;

namespace Server.Core.Processing
{
    public class ThreadsRunner
    {
        public ThreadsRunner()
        {
            logicThread = new LogicThread(Constants.MsPerTick);
            connectionsThread = new ConnectionsThread(Constants.MsPerTick);
            
            logicThread.Start();
            connectionsThread.Start();
        }

        private readonly LogicThread logicThread;
        private readonly ConnectionsThread connectionsThread;
    }
}