using System;
using System.Collections.Generic;

namespace Server.Core.Processing
{
    public static class ThreadManager
    {
        private static readonly List<Action> executeOnMainThread = new List<Action>();
        private static readonly List<Action> executeCopiedOnMainThread = new List<Action>();
        
        private static readonly List<ExecutablePacket> executePackets = new List<ExecutablePacket>();
        private static readonly List<ExecutablePacket> copiedPackets = new List<ExecutablePacket>();
        
        private static bool actionToExecuteOnMainThread = false;
        private static bool packetToExecuteOnMainThread = false;

        public static void AddLogicTask(Action _action)
        {
            if (_action == null)
            {
                Console.WriteLine("No action to execute on main thread!");
                return;
            }

            lock (executeOnMainThread)
            {
                executeOnMainThread.Add(_action);
                actionToExecuteOnMainThread = true;
            }
        }
        
        public static void AddConnectionTask(Action<byte[]> _action, byte[] _data)
        {
            if (_action == null)
            {
                Console.WriteLine("No action to execute on main thread!");
                return;
            }

            lock (executePackets)
            {
                executePackets.Add(new ExecutablePacket(_action, _data));
                packetToExecuteOnMainThread = true;
            }
        }

        /// <summary>Executes all code meant to run on the main thread. NOTE: Call this ONLY from the main thread.</summary>
        public static void ExecuteLogic()
        {
            if (actionToExecuteOnMainThread == true)
            {
                executeCopiedOnMainThread.Clear();
                
                lock (executeOnMainThread)
                {
                    executeCopiedOnMainThread.AddRange(executeOnMainThread);
                    executeOnMainThread.Clear();
                    actionToExecuteOnMainThread = false;
                }

                for (int i = 0; i < executeCopiedOnMainThread.Count; i++)
                    executeCopiedOnMainThread[i]();
            }
        }

        public static void ExecuteConnections()
        {
            if (packetToExecuteOnMainThread == true)
            {
                copiedPackets.Clear();
                
                lock (executePackets)
                {
                    copiedPackets.AddRange(executePackets);
                    executePackets.Clear();
                    packetToExecuteOnMainThread = false;
                }

                for (int i = 0; i < copiedPackets.Count; i++)
                    copiedPackets[i].Invoke();
            }
        }
    }
}