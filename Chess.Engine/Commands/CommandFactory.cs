using System;

namespace Chess.Engine.Commands;

public static class CommandFactory
{
    public static ICommandParser GetParser()
    {
        while (true)
        {
            string command = Console.ReadLine();
            if (command != null)
            {
                command = command.Trim().ToLowerInvariant();

                switch (command)
                {
                    case "uci":
                        return new UciCommands();
                    case "quit":
                        return null;

                }
            }
        }
    }
}
