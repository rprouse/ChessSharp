using System;

namespace Chess.Engine.IO;

public class SystemConsole : IConsole
{
    public void WriteLine(string message) => 
        Console.WriteLine(message);

    public string ReadLine() => 
        Console.ReadLine();
}
