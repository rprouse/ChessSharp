using Chess.Engine.IO;

namespace Chess.Engine.Commands;

public class CommandFactory(IConsole console)
{
    private readonly IConsole _console = console;

    public ICommandParser GetParser()
    {
        string command = _console.ReadLine();
        if (command != null)
        {
            command = command.Trim();

            switch (command)
            {
                case "uci":
                    return new UciCommands(_console);
                case "quit":
                    return null;

            }
        }
        return null; // Invalid command or no command provided
    }
}
