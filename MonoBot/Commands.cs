using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Links : ModuleBase {

    private Link[] _links = new Link[] {
            new Link {
                Name = "git gud",
                Url = "https://docs.google.com/document/d/14c_HM8c2znmiFB00whNEYekx6LKyvF99XnE6BuTzwXI/edit?usp=sharing"
            }
        };

    [Command("link")]
    [Summary("A quick and easy way to share commonly used links :)")]
    public async Task Link(string linkName) {
        var link = _links.SingleOrDefault(l => l.Name == linkName);
        if (link != null)
            await ReplyAsync(link.Url);

        await Context.Message.DeleteAsync();
    }
}

public class Link {
    public string Name { get; set; }
    public string Url { get; set; }
}
