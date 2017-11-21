using System;
using System.Linq;

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
