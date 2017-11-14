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
        static void Main(string[] args) {
            if (!args.Any()) {
                Console.WriteLine("Token required!");
                Console.WriteLine("Monobot.exe [token]");
                return;
            }

            new Selfbot().MainAsync(args).GetAwaiter().GetResult();
        }
    }
}
