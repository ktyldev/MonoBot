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
        
        var config = GetConfig();
        config.Macros.Add(new Macro {
            Name = args[0],
            Message = args[1]
        });
        SaveConfig(config);

        await Context.Message.DeleteAsync();
    }
    
    [Command("m")]
    [Summary("Save a message under a macro")]
    public async Task Macro([Remainder] string linkName) {
        var macro = GetConfig()
            .Macros
            .SingleOrDefault(l => l.Name == linkName);

        if (macro != null) {
            await ReplyAsync(macro.Message);
        } else {
            Console.WriteLine("No link with name: {0}", linkName);
        }

        await Context.Message.DeleteAsync();
    }

    [Command("payrespects")]
    [Summary(":(")]
    public async Task PayRespects() {
        await Context.Message.DeleteAsync();
        await ReplyAsync("F");
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
    public List<Macro> Macros { get; set; }
}

public class Macro {
    public string Name { get; set; }
    public string Message { get; set; }
}
;