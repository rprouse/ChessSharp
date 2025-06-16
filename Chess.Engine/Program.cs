using Chess.Engine.Commands;

ICommandParser parser = CommandFactory.GetParser();
if (parser != null )
{
    parser.Run();
}
