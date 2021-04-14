using Aimrank.CSGO.Application.Contracts;
using Aimrank.CSGO.Application.Server.CancelMatch;
using Aimrank.CSGO.Application.Server.FinishMatch;
using Aimrank.CSGO.Application.Server.PlayerDisconnected;
using Aimrank.CSGO.Application.Server.StartMatch;
using Aimrank.CSGO.Application.Server;
using System.Collections.Generic;
using System.Text.Json;
using System;

namespace Aimrank.CSGO.Infrastructure.Application.Server
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