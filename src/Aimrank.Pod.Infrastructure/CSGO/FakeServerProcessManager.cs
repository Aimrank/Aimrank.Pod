using Aimrank.Pod.Core.Services;
using Aimrank.Pod.Infrastructure.Cluster;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Aimrank.Pod.Infrastructure.CSGO
{
    internal class FakeServerProcessManager : IServerProcessManager
    {
        private readonly object _locker = new();
        
        private readonly ConcurrentQueue<int> _availablePorts = new();
        
        private readonly ConcurrentDictionary<Guid, ServerConfiguration> _processes = new();

        private readonly PodAddressFactory _podAddressFactory;

        private readonly ILogger<FakeServerProcessManager> _logger;
        
        public FakeServerProcessManager(
            PodAddressFactory podAddressFactory,
            ILogger<FakeServerProcessManager> logger)
        {
            _podAddressFactory = podAddressFactory;
            _logger = logger;
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

                var address = _podAddressFactory.CreateAddress(port);
                
                _logger.LogInformation($"Started CS:GO server at {address}");

                return address;
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