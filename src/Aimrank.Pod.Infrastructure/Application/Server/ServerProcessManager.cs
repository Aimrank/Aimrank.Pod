using Aimrank.Pod.Application.Server;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;
using System;

namespace Aimrank.Pod.Infrastructure.Application.Server
{
    internal class ServerProcessManager : IServerProcessManager, IDisposable
    {
        private readonly object _locker = new();
        
        private readonly ConcurrentQueue<int> _availablePorts = new();
        
        private readonly ConcurrentDictionary<Guid, ServerProcess> _processes = new();

        public ServerProcessManager()
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
                
                var process = new ServerProcess(matchId, new ServerConfiguration(
                    steamToken, port, whitelist.ToList(), map));


                if (!_processes.TryAdd(matchId, process))
                {
                    throw new ServerException($"Failed to start CS:GO server for match with ID \"{matchId}\"");
                }
                
                process.Start();

                return $"{GetLocalIpAddress()}:{port}";
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
        
        private static string GetLocalIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            return "localhost";
        }
    }
}