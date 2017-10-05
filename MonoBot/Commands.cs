using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Links : ModuleBase {

    private string _configPath = "..\\..\\config.json";
    
    [Command("link")]
    [Summary("A quick and easy way to share commonly used links :)")]
    public async Task Link([Remainder] string linkName) {
        var link = GetLinks().SingleOrDefault(l => l.Name == linkName);
        if (link != null)
            await ReplyAsync(link.Url);

        await Context.Message.DeleteAsync();
    }

    private Link[] GetLinks() {
        var config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(_configPath));
        return config.Links.ToArray();
    }
}

public class Search : ModuleBase {

    private string _lmgtfyUrl = "http://lmgtfy.com/?q=";
    private string _googleUrl = "https://google.com/search?q=";

    [Command("google")]
    [Summary("Google")]
    public async Task Google([Remainder] string query) {
        await ReplyAsync(_googleUrl + query.Replace(' ', '+'));
        await Context.Message.DeleteAsync();
    }

    [Command("patronise")]
    [Summary("Let Me Google That For You")]
    public async Task LMGTFY([Remainder] string query) {
        await ReplyAsync(_lmgtfyUrl + query.Replace(' ', '+'));
        await Context.Message.DeleteAsync();
    }
}

public class SetGame : ModuleBase {
    [Command("setgame")]
    public async Task SetSelfGame([Remainder] string game) {
        var client = Context.Client as DiscordSocketClient;

        await client.SetGameAsync(game);
        Console.WriteLine("Setting game: " + game);
    }
}

public class Config {
    public List<Link> Links { get; set; }
}

public class Link {
    public string Name { get; set; }
    public string Url { get; set; }
}
