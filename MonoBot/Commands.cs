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

    [Command("addmacro")]
    [Summary("Add a macro name;message")]
    public async Task AddMacro([Remainder] string input) {
        var splitChar = ';';

        var args = input.Split(splitChar);
        if (args.Length != 2) {
            Console.WriteLine("Incorrect number of arguments");
            return;
        }
        
        var config = _Config;
        config.Macros.Add(new Macro {
            Name = args[0],
            Message = args[1]
        });
        _Config = config;
        await Task.CompletedTask;
    }
    
    [Command("m")]
    [Summary("Save a message under a macro")]
    public async Task Macro([Remainder] string linkName) {
        var macro = _Config
            .Macros
            .SingleOrDefault(l => l.Name == linkName);

        if (macro == null) {
            Console.WriteLine("No link with name: {0}", linkName);
            await Task.CompletedTask;
            return;
        }

        await ReplyAsync(macro.Message);
    }

    [Command("payrespects")]
    [Summary(":(")]
    public async Task PayRespects() {
        await Context.Message.DeleteAsync();
        await ReplyAsync("F");
    }

    private Config _Config { get {
            return JsonConvert.DeserializeObject<Config>(File.ReadAllText(_configPath));
        } set {
            File.WriteAllText(_configPath, JsonConvert.SerializeObject(value));
        }
    }
}

public class Search : ModuleBase {

    private string _lmgtfyUrl = "http://lmgtfy.com/";
    private string _googleUrl = "https://google.com/search";
    
    [Command("google")]
    public async Task Google([Remainder] string query) {
        await QueryTask(_googleUrl, query);
    }

    [Command("patronise")]
    public async Task LMGTFY([Remainder] string query) {
        await QueryTask(_lmgtfyUrl, query);
    }
    private async Task QueryTask(string url, string query) {
        await ReplyAsync(url + String.Format("?q={0}", query.Replace(' ', '+')));
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
    public List<Macro> Macros { get; set; }
}

public class Macro {
    public string Name { get; set; }
    public string Message { get; set; }
}
;