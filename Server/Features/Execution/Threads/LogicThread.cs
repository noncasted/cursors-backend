using Server.Core.Processing;

namespace Server.Features.Execution.Threads
{
    public class LogicThread : ProcessableThread
    {
        public LogicThread(float _ticksPerSecond) : base(_ticksPerSecond)
        {
        }

        protected override void OnTick()
        {
            ThreadManager.ExecuteLogic();
        }
    }
}