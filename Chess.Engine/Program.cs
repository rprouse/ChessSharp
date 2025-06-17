using Chess.Engine.Commands;
using Chess.Engine.IO;

var commandFactory = new CommandFactory(new SystemConsole());
ICommandParser parser = commandFactory.GetParser();
if (parser != null)
{
    parser.Run();
}
