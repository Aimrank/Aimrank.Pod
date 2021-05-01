using Aimrank.Pod.Core.Commands.CancelMatch;
using Aimrank.Pod.Core.Commands.FinishMatch;
using Aimrank.Pod.Core.Commands.PlayerDisconnected;
using Aimrank.Pod.Core.Commands.StartMatch;
using Aimrank.Pod.Core.Services;
using MediatR;
using System.Collections.Generic;
using System.Text.Json;
using System;

namespace Aimrank.Pod.Infrastructure.CSGO
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

        public IRequest Map(Guid matchId, string name, dynamic data)
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
            
            var command = (IRequest) JsonSerializer.Deserialize(content, commandType, settings);

            return command;
        }
    }
}