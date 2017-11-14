using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SetGame : ModuleBase {
    [Command("setgame")]
    public async Task SetSelfGame([Remainder] string game) {
        var client = Context.Client as DiscordSocketClient;

        await client.SetGameAsync(game);
        Console.WriteLine("Setting game: " + game);
    }
}
