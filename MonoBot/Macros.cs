using Discord.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Macros : ModuleBase {

    private string _configPath = @"..\..\config.json";
    private Config _Config
    {
        get
        {
            return JsonConvert.DeserializeObject<Config>(File.ReadAllText(_configPath));
        }
        set
        {
            File.WriteAllText(_configPath, JsonConvert.SerializeObject(value));
        }
    }

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
            Console.WriteLine("No macro with name: {0}", linkName);
            await Task.CompletedTask;
            return;
        }

        await ReplyAsync(macro.Message);
    }
}
