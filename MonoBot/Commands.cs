using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public class Commands {

    private const char COMMAND_CHAR = '~';

    private DiscordSocketClient _client;
    private CommandService _commands;
    private IServiceProvider _services;

    public Commands(DiscordSocketClient client) {
        _commands = new CommandService();
        _commands.AddModulesAsync(Assembly.GetEntryAssembly());
        
        _client = client;
        _client.MessageReceived += HandleCommand;

        _services = new ServiceCollection().BuildServiceProvider();
    }
    
    private async Task HandleCommand(SocketMessage socketMessage) {
        var message = (SocketUserMessage)socketMessage;

        if (message == null || message.Author.Id != _client.CurrentUser.Id)
            return;

        var argPos = 0;
        if (!(message.HasCharPrefix(COMMAND_CHAR, ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos)))
            return;

        Console.WriteLine("Running command: " + message);
        var context = new CommandContext(_client, message);
        var result = await _commands.ExecuteAsync(context, argPos, _services);

        if (!result.IsSuccess) {
            Console.WriteLine(result.ErrorReason);
        }

        await context.Message.DeleteAsync();
    }
}

