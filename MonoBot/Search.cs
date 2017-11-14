using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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