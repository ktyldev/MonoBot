using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ConsoleLogger {
    public void Log(string source, string message) {
        Console.WriteLine("{0} {1}     {2}", DateTime.Now.ToLongTimeString(), source, message);
    }
}
