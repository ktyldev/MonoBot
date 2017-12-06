using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Anagram : ModuleBase
{
    private StringBuilder _stringBuilder;
    private List<string> _permutations;

    [Command("anagram")]
    public async Task GetPermutation(string anagram)
    {
        _permutations = new List<string>();
        _stringBuilder = new StringBuilder();
        GetPermutation(anagram.ToCharArray());

        _permutations
            .ToList()
            .ForEach(p => { _stringBuilder.Append(p + ", "); Console.WriteLine(p); });
        
        var results = _stringBuilder.ToString();

        var message = String.Format("```{0}```", results);

        await (ReplyAsync(message));
    }

    private void Swap(ref char a, ref char b)
    {
        if (a == b) return;

        a ^= b;
        b ^= a;
        a ^= b;
    }

    private void GetPermutation(char[] list)
    {
        var x = list.Length;
        GetPermutation(list, 0, x);
    }

    private void GetPermutation(char[] list, int recursionDepth, int maxDepth)
    {
        if (recursionDepth == maxDepth)
        {
            var str = "";
            list.ToList().ForEach(c => str += c);
            if (_permutations.Contains(str))
                return;
            
            _permutations.Add(str);
            return;
        }

        for (int i = recursionDepth; i < maxDepth; i++)
        {
            Swap(ref list[recursionDepth], ref list[i]);
            GetPermutation(list, recursionDepth + 1, maxDepth);
            Swap(ref list[recursionDepth], ref list[i]);
        }
    }
}
