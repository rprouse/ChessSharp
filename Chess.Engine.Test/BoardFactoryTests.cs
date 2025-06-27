namespace Chess.Engine.Test;

[TestFixture]
public class BoardFactoryTests
{
    [TestCase(BoardInitialization.Blank, TestFEN.Blank)]
    [TestCase(BoardInitialization.Standard, TestFEN.Standard)]
    public void TestCreateBoard(BoardInitialization bi, string fen)
    {
        var board = BoardFactory.Create(bi);
        board.FEN.ShouldBe(fen);
    }

    [Test]
    public void TestCreate960BoardThrowsNotImplementedException()
    {
        Should.Throw<NotImplementedException>(() => BoardFactory.Create(BoardInitialization.Chess960));
    }

    [TestCase(TestFEN.Blank)]
    [TestCase(TestFEN.Standard)]
    [TestCase("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1")]
    [TestCase("rnbqkbnr/pp1ppppp/8/2p5/4P3/8/PPPP1PPP/RNBQKBNR w KQkq c6 0 2")]
    [TestCase("rnbqkbnr/pp1ppppp/8/2p5/4P3/5N2/PPPP1PPP/RNBQKB1R b KQkq - 1 2")]
    [TestCase("rnbqk2r/pp2ppbp/3p1np1/2pN4/2B1P3/5Q2/PPPP1PPP/R1B1K1NR w KQkq - 5 6")]
    [TestCase("8/2p4k/2p2K1p/2Pb2p1/p5p1/P1P5/8/8 w - - 4 57")]
    [TestCase("r1b1k2r/2p1b3/p1p4p/1p4p1/8/8/PPP2PPP/RNB1Q1K1 b kq - 2 16")]
    public void TestNewBoardWithFen(string fen)
    {
        var board = BoardFactory.Create(fen);
        board.FEN.ShouldBe(fen, $"FEN should remain unchanged: {fen} != {board.FEN}");
    }

    [TestCase("rnbqkbnr/pppp1ppp/4p3/8/4P3/8/PPPP1PPP/RNBQKBNR w - 0 2")]
    [TestCase("rnbqkbnr/pppp1ppp/4p3/8/4P3/PPPP1PPP/RNBQKBNR w KQkq - 0 2")]
    [TestCase("rnbqkbnr/pppp1ppp/4p3/8/4P3/7/PPPP1PPP/RNBQKBNR w KQkq - 0 2")]
    [TestCase("rnbqkbnr/pppp1ppp/4p3/8/3P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 2")]
    [TestCase("rnbqkbnr/pppp1ppp/4p3/p8/3P4/8/PPPP1PPP/RNBQKBNR w KQkq - 0 2")]
    [TestCase("rnbqkbnr/pppppppp/8/8/9/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")]
    [TestCase("rnbqkbnr/pppppppp/8/8/8/8p/PPPPPPPP/RNBQKBNR w KQkq - 0 1")]
    [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - A 1")]
    [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 A")]
    [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 0")]
    public void InvalidFenThrowsArgumentException(string fen)
    {
        Should.Throw<ArgumentException>(() => BoardFactory.Create(fen), $"Invalid FEN should throw an exception: {fen}");
    }
}
