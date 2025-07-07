using Chess.Engine.Commands;
using Chess.Engine.IO;

namespace Chess.Engine.Test.Commands;

public class CommandFactoryTests
{
    IConsole _console;
    CommandFactory _factory;

    [SetUp]
    public void Setup()
    {
        _console = Substitute.For<IConsole>();
        _factory = new CommandFactory(_console);
    }

    [Test]
    public void TestCommandFactoryUciParser()
    {
        _console.ReadLine().Returns("uci");
        _factory.GetParser().ShouldBeOfType<UciCommands>();
    }

    [Test]
    public void TestCommandFactoryQuit()
    {
        _console.ReadLine().Returns("quit");
        _factory.GetParser().ShouldBeNull();
    }

    [TestCase("invalid")]
    [TestCase(" ")]
    [TestCase(null)]
    public void TestCommandFactoryInvalid(string? command)
    {
        _console.ReadLine().Returns(command);
        _factory.GetParser().ShouldBeNull();
    }
}
