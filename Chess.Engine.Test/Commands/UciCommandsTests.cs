using Chess.Engine.Commands;
using Chess.Engine.IO;

namespace Chess.Engine.Test.Commands;

public class UciCommandsTests
{
    IConsole _console;
    UciCommands _commands;

    [SetUp]
    public void Setup()
    {
        _console = Substitute.For<IConsole>();
        _commands = new UciCommands(_console);
    }

    [Test]
    public void TestInitialization()
    {
        _console.Received().WriteLine(Arg.Is<string>(s => s.StartsWith("id name Alteridem")));
        _console.Received().WriteLine("id author Rob Prouse");
        _console.Received().WriteLine("uciok");

        _commands._board.ShouldNotBeNull(); // Ensure the board is initialized
        _commands._board.FEN.ShouldBe(TestFEN.Standard);
    }

    [Test]
    public void TestQuitCommand()
    {
        _console.ReadLine().Returns("quit");
        _commands.Run();
    }


    [Test]
    public void TestNullCommands()
    {
        _console.ReadLine().Returns(null, "quit");
        _commands.Run();
        _console.Received().WriteLine("Null command");
    }

    [Test]
    public void TestEmptyCommand()
    {
        _console.ReadLine().Returns(string.Empty, "quit");
        _commands.Run();
        _console.Received().WriteLine("Empty command");
    }

    [Test]
    public void TestInvalidCommand()
    {
        _console.ReadLine().Returns("invalid command", "quit");
        _commands.Run();
        _console.Received().WriteLine("Unknown command: invalid command");
    }

    [Test]
    public void TestDebugCommand()
    {
        _console.ReadLine().Returns("debug on", "quit");
        _commands.Run();
        // TODO: Verify debug behavior when implemented
    }

    [Test]
    public void TestReadyCommand()
    {
        _console.ReadLine().Returns("isready", "quit");
        _commands.Run();
        _console.Received().WriteLine("readyok");
    }

    [Test]
    public void TestSetOptionCommand()
    {
        _console.ReadLine().Returns("setoption name Style value Risky", "quit");
        _commands.Run();
        // TODO: Verify debug behavior when implemented
    }

    [Test]
    public void TestRegisterCommand()
    {
        _console.ReadLine().Returns("register name Stefan MK code 4359874324", "quit");
        _commands.Run();
        // TODO: Verify debug behavior when implemented
    }

    [Test]
    public void TestUciNewGameCommand()
    {
        _console.ReadLine().Returns("ucinewgame", "quit");
        _commands.Run();
        // TODO: Verify debug behavior when implemented
    }

    [Test]
    public void TestGoCommand()
    {
        _console.ReadLine().Returns("go ponder", "quit");
        _commands.Run();
        // TODO: Verify debug behavior when implemented
    }

    [Test]
    public void TestStopCommand()
    {
        _console.ReadLine().Returns("stop", "quit");
        _commands.Run();
        // TODO: Verify debug behavior when implemented
    }

    [Test]
    public void TestPonderHitCommand()
    {
        _console.ReadLine().Returns("ponderhit", "quit");
        _commands.Run();
        // TODO: Verify debug behavior when implemented
    }

    [TestCase("position")]
    [TestCase("position startpos")]
    [TestCase("position startpos moves")]
    [TestCase("position start")]
    [TestCase("position startpos e2e4")]
    [TestCase("position fen")]
    public void TestInvalidPositionCommand(string command)
    {
        _console.ReadLine().Returns("position", "quit");
        _commands.Run();
        _console.Received().WriteLine(Arg.Is<string>(s => s.StartsWith("Invalid position command.")));
    }

        [Test]
    public void TestPositionStartPosCommand()
    {
        _console.ReadLine().Returns("position startpos moves e2e4 e7e5", "quit");
        _commands.Run();
        // TODO: Implement halfmove clock
        _commands._board.FEN.ShouldBe("rnbqkbnr/pppp1ppp/8/4p3/4P3/8/PPPP1PPP/RNBQKBNR w KQkq e6 0 2");
    }

    [Test]
    public void TestPositionFenCommand()
    {
        _console.ReadLine().Returns("position fen rnbqkbnr/pppp1ppp/8/4p3/4P3/8/PPPP1PPP/RNBQKBNR w KQkq e6 0 2 d2d4 d7d5 ", "quit");
        _commands.Run();
        // TODO: Implement halfmove clock
        _commands._board.FEN.ShouldBe("rnbqkbnr/ppp2ppp/8/3pp3/3PP3/8/PPP2PPP/RNBQKBNR w KQkq d6 0 3");
    }
}
