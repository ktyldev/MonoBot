using Discord.Commands;
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

public class Google : ModuleBase {

    private string _baseUrl = "http://lmgtfy.com/?q=";

    [Command("google")]
    [Summary("Let Me Google That For You")]
    public async Task LMGTFY([Remainder] string query) {
        await ReplyAsync(_baseUrl + query.Replace(' ', '+'));
        await Context.Message.DeleteAsync();
    }
}

public class Config {
    public List<Link> Links { get; set; }
}

public class Link {
    public string Name { get; set; }
    public string Url { get; set; }
}
