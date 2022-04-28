using System;
using System.Threading;

namespace Server.Core.Processing
{
    public abstract class ProcessableThread
    {
        public ProcessableThread(float _ticksPerSecond)
        {
            ticksPerSecond = _ticksPerSecond;
            thread = new Thread(Process);
        }

        private readonly float ticksPerSecond;

        private readonly Thread thread;

        private bool isRunning = false;

        public void Start()
        {
            thread.Start();
            isRunning = true;
        }

        public void Stop()
        {
            isRunning = false;
        }

        private void Process()
        {
            while (isRunning == true)
            {
                DateTime _nextLoop = DateTime.Now;

                while (_nextLoop < DateTime.Now)
                {
                    OnTick();
                    _nextLoop = _nextLoop.AddMilliseconds(ticksPerSecond);
                }

                if (_nextLoop > DateTime.Now)
                    Thread.Sleep(_nextLoop - DateTime.Now);
            }
        }

        protected abstract void OnTick();
    }
}