using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace MonoBot {
    class Program {

        private const char COMMAND_CHAR = '~';

        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        static void Main(string[] args) {
            if (!args.Any()) {
                Console.WriteLine("Token required!");
                Console.WriteLine("Monobot.exe [token]");
                return;
            }

            new Program().MainAsync(args).GetAwaiter().GetResult();
        }
        
        public async Task MainAsync(string[] args) {
            Console.Title = "MonoBot";

            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _services = new ServiceCollection()
                .BuildServiceProvider();

            _client.Log += m => {
                Console.WriteLine(m.ToString());
                return Task.CompletedTask;
            };

            var token = args.First();

            await InstallCommands();

            await _client.LoginAsync(TokenType.User, token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task InstallCommands() {
            _client.MessageReceived += HandleCommand;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task HandleCommand(SocketMessage socketMessage) {
            var message = (SocketUserMessage)socketMessage;

            if (message == null || message.Author.Id != _client.CurrentUser.Id)
                return;

            var argPos = 0;
            if (!(message.HasCharPrefix(COMMAND_CHAR, ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos)))
                return;

            Console.WriteLine("Running command!");
            var context = new CommandContext(_client, message);
            var result = await _commands.ExecuteAsync(context, argPos, _services);

            if (!result.IsSuccess) {
                await context.Channel.SendMessageAsync(result.ErrorReason);
            }
        }
    }
}
