using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ConsoleLogger {
    public void Log(LogMessage message) {
        if (message.Exception != null) {
            Log("Error", message.Exception.Message);
            return;
        }

        Log(message.Source, message.Message);
    }

    public void Log(string source, string message) {
        Console.WriteLine("{0} {1}     {2}", DateTime.Now.ToLongTimeString(), source, message);
    }
}
