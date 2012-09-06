using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alteridem.Engine.Commands
{
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
}
