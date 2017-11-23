using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Selfbot {
    private DiscordSocketClient _client;
    private ConsoleLogger _log;

    public async Task MainAsync(string[] args) {
        Console.Title = "MonoBot";

        _log = new ConsoleLogger();
        _client = new DiscordSocketClient();
        var commands = new Commands(_client, _log);

        _client.Log += m => {
            _log.Log(m);
            return Task.CompletedTask;
        };

        var token = args.First();

        await _client.LoginAsync(TokenType.User, token);
        await _client.StartAsync();

        await Task.Delay(-1);
    }
}
