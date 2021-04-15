using Aimrank.Pod.Application.Contracts;
using Aimrank.Pod.Application.Server.CancelMatch;
using Aimrank.Pod.Application.Server.FinishMatch;
using Aimrank.Pod.Application.Server.PlayerDisconnected;
using Aimrank.Pod.Application.Server.StartMatch;
using Aimrank.Pod.Application.Server;
using System.Collections.Generic;
using System.Text.Json;
using System;

namespace Aimrank.Pod.Infrastructure.Application.Server
{
    internal class ServerEventMapper : IServerEventMapper
    {
        private readonly Dictionary<string, Type> _commands = new()
        {
            ["map_start"] = typeof(StartMatchCommand),
            ["match_end"] = typeof(FinishMatchCommand),
            ["match_cancel"] = typeof(CancelMatchCommand),
            ["player_disconnect"] = typeof(PlayerDisconnectCommand)
        };

        public ICommand Map(Guid matchId, string name, dynamic data)
        {
            var settings = new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

            var commandType = _commands.GetValueOrDefault(name);
            if (commandType is null)
            {
                return null;
            }

            var content = JsonSerializer.Serialize<dynamic>(data, settings) as string;
            
            content = data is null
                ? $"{{\"matchId\": \"{matchId}\"}}"
                : $"{{\"matchId\": \"{matchId}\", {content.Substring(1)}";
            
            var command = (ICommand) JsonSerializer.Deserialize(content, commandType, settings);

            return command;
        }
    }
}