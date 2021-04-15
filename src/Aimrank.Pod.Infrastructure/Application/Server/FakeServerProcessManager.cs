using Aimrank.Pod.Application.Server;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Aimrank.Pod.Infrastructure.Application.Server
{
    internal class FakeServerProcessManager : IServerProcessManager
    {
        private readonly object _locker = new();
        
        private readonly ConcurrentQueue<int> _availablePorts = new();
        
        private readonly ConcurrentDictionary<Guid, ServerConfiguration> _processes = new();
        
        public FakeServerProcessManager()
        {
            _availablePorts.Enqueue(27016);
            _availablePorts.Enqueue(27017);
            _availablePorts.Enqueue(27018);
            _availablePorts.Enqueue(27019);
        }
        
        public string StartServer(Guid matchId, string steamToken, string map, IEnumerable<string> whitelist)
        {
            lock (_locker)
            {
                if (!_availablePorts.TryDequeue(out var port))
                {
                    throw new ServerException($"Failed to start CS:GO server for match with ID \"{matchId}\"");
                }
                
                var configuration = new ServerConfiguration(steamToken, port, whitelist.ToList(), map);

                _processes.TryAdd(matchId, configuration);

                return "localhost";
            }
        }

        public void StopServer(Guid matchId)
        {
            if (_processes.TryRemove(matchId, out var process))
            {
                lock (_locker)
                {
                    _availablePorts.Enqueue(process.Port);
                }
            }
        }
    }
}