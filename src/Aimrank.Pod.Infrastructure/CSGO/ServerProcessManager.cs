using Aimrank.Pod.Core.Services;
using Aimrank.Pod.Infrastructure.Cluster;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Aimrank.Pod.Infrastructure.CSGO
{
    internal class ServerProcessManager : IServerProcessManager, IDisposable
    {
        private readonly object _locker = new();
        
        private readonly ConcurrentQueue<int> _availablePorts = new();
        
        private readonly ConcurrentDictionary<Guid, ServerProcess> _processes = new();

        private readonly PodAddressFactory _podAddressFactory;

        private readonly ILogger<ServerProcessManager> _logger;

        public ServerProcessManager(PodAddressFactory podAddressFactory, ILogger<ServerProcessManager> logger)
        {
            _podAddressFactory = podAddressFactory;
            _logger = logger;
            _availablePorts.Enqueue(30001);
            _availablePorts.Enqueue(30002);
            _availablePorts.Enqueue(30003);
            _availablePorts.Enqueue(30004);
        }

        public string StartServer(Guid matchId, string steamToken, string map, IEnumerable<string> whitelist)
        {
            lock (_locker)
            {
                if (!_availablePorts.TryDequeue(out var port))
                {
                    throw new ServerException($"Failed to start CS:GO server for match with ID \"{matchId}\"");
                }
                
                var process = new ServerProcess(matchId, new ServerConfiguration(
                    steamToken, port, whitelist.ToList(), map));


                if (!_processes.TryAdd(matchId, process))
                {
                    throw new ServerException($"Failed to start CS:GO server for match with ID \"{matchId}\"");
                }
                
                process.Start();

                var address = _podAddressFactory.CreateAddress(port);
                
                _logger.LogInformation($"Started CS:GO server at {address}");

                return address;
            }
        }

        public void StopServer(Guid matchId)
        {
            if (!_processes.TryRemove(matchId, out var process))
            {
                throw new ServerException($"Failed to stop CS:GO server for match with ID \"{matchId}\"");
            }
            
            Task.Run(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(20));
                await process.StopAsync();
                process.Dispose();
                
                lock (_locker)
                {
                    _availablePorts.Enqueue(process.Configuration.Port);
                }
            });
        }

        public void Dispose()
        {
            foreach (var process in _processes.Values)
            {
                process.Dispose();
            }
        }
    }
}