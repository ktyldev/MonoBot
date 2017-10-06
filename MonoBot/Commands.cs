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

    [Command("addlink")]
    [Summary("Add a link linkName:linkUrl")]
    public async Task AddLink([Remainder] string linkDef) {
        var splitChar = ';';

        var args = linkDef.Split(splitChar);
        if (args.Length != 2) {
            Console.WriteLine("Incorrect number of arguments");
            return;
        }
        
        var config = GetConfig();
        config.Links.Add(new Link {
            Name = args[0],
            Url = args[1]
        });
        SaveConfig(config);

        await Context.Message.DeleteAsync();
    }

    [Command("link")]
    [Summary("A quick and easy way to share commonly used links :)")]
    public async Task Link([Remainder] string linkName) {
        var link = GetConfig()
            .Links
            .SingleOrDefault(l => l.Name == linkName);

        if (link != null) {
            await ReplyAsync(link.Url);
        } else {
            Console.WriteLine("No link with name: " + linkName);
        }

        await Context.Message.DeleteAsync();
    }

    private Config GetConfig() {
        return JsonConvert.DeserializeObject<Config>(File.ReadAllText(_configPath));
    }

    private void SaveConfig(Config config) {
        File.WriteAllText(_configPath, JsonConvert.SerializeObject(config));
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
;