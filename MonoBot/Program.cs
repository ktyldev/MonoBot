using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace MonoBot {
    class Program {
        static void Main(string[] args) {
            if (!args.Any()) {
                Console.WriteLine("Token required!");
                Console.WriteLine("Monobot.exe [token]");
                return;
            }

            new Program().MainAsync(args).GetAwaiter().GetResult();
        }

        private DiscordSocketClient _client;

        public async Task MainAsync(string[] args) {
            Console.Title = "MonoBot";

            _client = new DiscordSocketClient();
            _client.Log += m => {
                Console.WriteLine(m.ToString());
                return Task.CompletedTask;
            };
            
            _client.MessageReceived += m => {
                Console.WriteLine(m.ToString());
                return Task.CompletedTask;
            };

            var token = args.First();
            await _client.LoginAsync(TokenType.User, token);
            await _client.StartAsync();
            
            await Task.Delay(-1);
        }
    }
}
