namespace Chess.Engine.Test;

[TestFixture]
public class TestBoard
{
    [TestCase("-", -1)]
    [TestCase("a1", 00)]
    [TestCase("a8", 56)]
    [TestCase("a5", 32)]
    [TestCase("b1", 01)]
    [TestCase("b7", 49)]
    [TestCase("c6", 42)]
    [TestCase("d5", 35)]
    [TestCase("e4", 28)]
    [TestCase("h1", 07)]
    [TestCase("h8", 63)]
    public void TestIndexFromSquare(string square, int index)
    {
        Board.IndexFromSquare(square).ShouldBe(index, $"Square {square} should map to index {index}");
    }

    [TestCase("-", -1)]
    [TestCase("a1", 00)]
    [TestCase("a8", 56)]
    [TestCase("a5", 32)]
    [TestCase("b1", 01)]
    [TestCase("b7", 49)]
    [TestCase("c6", 42)]
    [TestCase("d5", 35)]
    [TestCase("e4", 28)]
    [TestCase("h1", 07)]
    [TestCase("h8", 63)]
    public void TestSquareFromIndex(string square, int index)
    {
        Board.SquareFromIndex(index).ShouldBe(square, $"Index {index} should map to square {square}");
    }


    [TestCase(-1, -1, -1)]
    [TestCase(0, 0, 00)]
    [TestCase(0, 7, 56)]
    [TestCase(0, 4, 32)]
    [TestCase(1, 0, 01)]
    [TestCase(1, 6, 49)]
    [TestCase(2, 5, 42)]
    [TestCase(3, 4, 35)]
    [TestCase(4, 3, 28)]
    [TestCase(7, 0, 07)]
    [TestCase(7, 7, 63)]
    public void TestIndex(int file, int rank, int expectedIndex)
    {
        Board.Index(rank, file).ShouldBe(expectedIndex, $"Index for file {file}, rank {rank} should be {expectedIndex}");
    }

    [TestCase(3, 28)]
    [TestCase(7, 63)]
    [TestCase(0, 0)]
    public void TestRank(int rank, int index)
    {
        Board.Rank(index).ShouldBe(rank, $"Rank for index {index} should be {rank}");
    }

    [TestCase(3, 25)]
    [TestCase(7, 63)]
    [TestCase(0, 0)]
    public void TestFile(int file, int index)
    {
        Board.Rank(index).ShouldBe(file, $"File for index {index} should be {file}");
    }

    // General Errors
    [TestCase("rnbqkbnr/pppp1ppp/4p3/8/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 2", "rnbqkbnr/pppp1ppp/4p3/8/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 2", false, "e5", "e4", "Wrong colour moving")]
    [TestCase("rnbqkbnr/pppp1ppp/4p3/8/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 2", "rnbqkbnr/pppp1ppp/4p3/8/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 2", false, "b4", "b5", "No piece to move")]
    [TestCase("rnbqkbnr/pppp1ppp/4p3/8/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 2", "rnbqkbnr/pppp1ppp/4p3/8/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 2", false, "a0", "b5", "from low")]
    [TestCase("rnbqkbnr/pppp1ppp/4p3/8/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 2", "rnbqkbnr/pppp1ppp/4p3/8/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 2", false, "i9", "b5", "from high")]
    [TestCase("rnbqkbnr/pppp1ppp/4p3/8/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 2", "rnbqkbnr/pppp1ppp/4p3/8/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 2", false, "b5", "a0", "to low")]
    [TestCase("rnbqkbnr/pppp1ppp/4p3/8/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 2", "rnbqkbnr/pppp1ppp/4p3/8/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 2", false, "b5", "i9", "to high")]
    [TestCase("rnbqkbnr/ppp2ppp/3p4/4p3/4P3/3P4/PPP2PPP/RNBQKBNR w KQkq - 0 3", "rnbqkbnr/ppp2ppp/3p4/4p3/4P3/3P4/PPP2PPP/RNBQKBNR w KQkq - 0 3", false, "d3", "e4", "Capture our own piece")]
    // Capture the king, this is an invalid FEN since the king cannot be in check at the end of the move
    [TestCase("8/5k2/8/2p5/P7/2P5/8/1r1K2R1 w - - 3 48", "8/5k2/8/2p5/P7/2P5/8/1r1K2R1 w - - 3 48", false, "a1", "d1", "Invalid king capture")]

    // White Pawn Moves
    [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "rnbqkbnr/pppppppp/8/8/8/4P3/PPPP1PPP/RNBQKBNR b KQkq - 0 1", true, "e2", "e3", "White pawn, one square")]
    [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1", true, "e2", "e4", "White pawn, two squares")]
    [TestCase("rnbqkbnr/pppp1ppp/4p3/8/8/4P3/PPPP1PPP/RNBQKBNR w KQkq - 0 2", "rnbqkbnr/pppp1ppp/4p3/8/8/4P3/PPPP1PPP/RNBQKBNR w KQkq - 0 2", false, "e3", "e5", "White pawn, two squares, not first row")]
    [TestCase("rnbqkbnr/pppp1ppp/8/4p3/4P3/8/PPPP1PPP/RNBQKBNR w KQkq e6 0 2", "rnbqkbnr/pppp1ppp/8/4p3/4P3/8/PPPP1PPP/RNBQKBNR w KQkq e6 0 2", false, "e4", "e5", "White pawn, one square, blocked")]
    [TestCase("rnbqkbnr/pppp2pp/8/4p3/4Pp2/3P2P1/PPP2P1P/RNBQKBNR w KQkq - 0 4", "rnbqkbnr/pppp2pp/8/4p3/4Pp2/3P2P1/PPP2P1P/RNBQKBNR w KQkq - 0 4", false, "f2", "f4", "White pawn, two square, blocked 1")]
    [TestCase("rnbqkbnr/pppp2pp/8/4p3/4P3/3P1pPP/PPP2P2/RNBQKBNR w KQkq - 0 5", "rnbqkbnr/pppp2pp/8/4p3/4P3/3P1pPP/PPP2P2/RNBQKBNR w KQkq - 0 5", false, "f2", "f4", "White pawn, two square, blocked 2")]
    [TestCase("rnbqkbnr/ppp1pppp/8/3p4/4P3/8/PPPP1PPP/RNBQKBNR w KQkq d6 0 2", "rnbqkbnr/ppp1pppp/8/3P4/8/8/PPPP1PPP/RNBQKBNR b KQkq - 0 2", true, "e4", "d5", "White pawn, capture left")]
    [TestCase("rnbqkbnr/ppppp1pp/8/5p2/4P3/8/PPPP1PPP/RNBQKBNR w KQkq f6 0 2", "rnbqkbnr/ppppp1pp/8/5P2/8/8/PPPP1PPP/RNBQKBNR b KQkq - 0 2", true, "e4", "f5", "White pawn, capture right")]
    [TestCase("rnbqkbnr/pppp2pp/8/4pP2/8/8/PPPP1PPP/RNBQKBNR w KQkq e6 0 3", "rnbqkbnr/pppp2pp/4P3/8/8/8/PPPP1PPP/RNBQKBNR b KQkq - 0 3", true, "f5", "e6", "White pawn, en-passant capture")]
    [TestCase("rnbqkbnr/ppp3pp/3p4/4pP2/6P1/8/PPPP1P1P/RNBQKBNR w KQkq - 0 4", "rnbqkbnr/ppp3pp/3p4/4pP2/6P1/8/PPPP1P1P/RNBQKBNR w KQkq - 0 4", false, "f5", "e6", "White pawn, en-passant capture no longer valid")]

    // Black Pawn Moves
    [TestCase("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1", "rnbqkbnr/pppp1ppp/4p3/8/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 2", true, "e7", "e6", "Black pawn, one square")]
    [TestCase("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1", "rnbqkbnr/pppp1ppp/8/4p3/4P3/8/PPPP1PPP/RNBQKBNR w KQkq e6 0 2", true, "e7", "e5", "Black pawn, two squares")]
    [TestCase("rnbqkbnr/pppp1ppp/4p3/8/8/4PP2/PPPP2PP/RNBQKBNR b KQkq - 0 2", "rnbqkbnr/pppp1ppp/4p3/8/8/4PP2/PPPP2PP/RNBQKBNR b KQkq - 0 2", false, "e6", "e4", "Black pawn, two squares, not first row")]
    [TestCase("rnbqkbnr/pppp1ppp/8/4p3/4P3/3P4/PPP2PPP/RNBQKBNR b KQkq - 0 2", "rnbqkbnr/pppp1ppp/8/4p3/4P3/3P4/PPP2PPP/RNBQKBNR b KQkq - 0 2", false, "e5", "e4", "Black pawn, one square, blocked")]
    [TestCase("rnbqkbnr/ppp1pppp/4P3/3p4/8/8/PPPP1PPP/RNBQKBNR b KQkq - 0 3", "rnbqkbnr/ppp1pppp/4P3/3p4/8/8/PPPP1PPP/RNBQKBNR b KQkq - 0 3", false, "e7", "e5", "Black pawn, two square, blocked 1")]
    [TestCase("rnbqkbnr/ppp1pppp/3p4/4P3/8/8/PPPP1PPP/RNBQKBNR b KQkq - 0 2", "rnbqkbnr/ppp1pppp/3p4/4P3/8/8/PPPP1PPP/RNBQKBNR b KQkq - 0 2", false, "e7", "e5", "Black pawn, two square, blocked 2")]
    [TestCase("rnbqkbnr/ppp1p1pp/3p1p2/4P3/8/3P4/PPP2PPP/RNBQKBNR b KQkq - 0 3", "rnbqkbnr/ppp1p1pp/5p2/4p3/8/3P4/PPP2PPP/RNBQKBNR w KQkq - 0 4", true, "d6", "e5", "Black pawn, capture left")]
    [TestCase("rnbqkbnr/ppp1p1pp/3p1p2/4P3/8/3P4/PPP2PPP/RNBQKBNR b KQkq - 0 3", "rnbqkbnr/ppp1p1pp/3p4/4p3/8/3P4/PPP2PPP/RNBQKBNR w KQkq - 0 4", true, "f6", "e5", "Black pawn, capture right")]
    [TestCase("rnbqkbnr/pppp1ppp/8/8/P2Pp3/8/1PP1PPPP/RNBQKBNR b KQkq d3 0 3", "rnbqkbnr/pppp1ppp/8/8/P7/3p4/1PP1PPPP/RNBQKBNR w KQkq - 0 4", true, "e4", "d3", "Black pawn, en-passant capture")]
    [TestCase("rnbqkbnr/pppp2pp/8/5p2/P2Pp3/1P6/2P1PPPP/RNBQKBNR b KQkq - 0 4", "rnbqkbnr/pppp2pp/8/5p2/P2Pp3/1P6/2P1PPPP/RNBQKBNR b KQkq - 0 4", false, "e4", "d3", "Black pawn, en-passant capture no longer valid")]

    // Knight Moves
    [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "rnbqkbnr/pppppppp/8/8/8/N7/PPPPPPPP/R1BQKBNR b KQkq - 0 1", true, "b1", "a3", "White knight opening")]
    [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "rnbqkbnr/pppppppp/8/8/8/2N5/PPPPPPPP/R1BQKBNR b KQkq - 0 1", true, "b1", "c3", "White knight opening")]
    [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "rnbqkbnr/pppppppp/8/8/8/5N2/PPPPPPPP/RNBQKB1R b KQkq - 0 1", true, "g1", "f3", "White knight opening")]
    [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", false, "g1", "g3", "White knight opening, bad move")]
    [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", false, "g1", "e2", "White knight opening, blocked move")]
    [TestCase("r1bqkbnr/pppppp2/n5p1/7p/3N4/2P1P3/PP1P1PPP/RNBQKB1R w KQkq - 0 5", "r1bqkbnr/pppppp2/n3N1p1/7p/8/2P1P3/PP1P1PPP/RNBQKB1R b KQkq - 0 5", true, "d4", "e6", "White knight all moves")]
    [TestCase("r1bqkbnr/pppppp2/n5p1/7p/3N4/2P1P3/PP1P1PPP/RNBQKB1R w KQkq - 0 5", "r1bqkbnr/pppppp2/n5p1/5N1p/8/2P1P3/PP1P1PPP/RNBQKB1R b KQkq - 0 5", true, "d4", "f5", "White knight all moves")]
    [TestCase("r1bqkbnr/pppppp2/n5p1/7p/3N4/2P1P3/PP1P1PPP/RNBQKB1R w KQkq - 0 5", "r1bqkbnr/pppppp2/n5p1/7p/8/2P1PN2/PP1P1PPP/RNBQKB1R b KQkq - 0 5", true, "d4", "f3", "White knight all moves")]
    [TestCase("r1bqkbnr/pppppp2/n5p1/7p/3N4/2P1P3/PP1P1PPP/RNBQKB1R w KQkq - 0 5", "r1bqkbnr/pppppp2/n5p1/7p/8/2P1P3/PP1PNPPP/RNBQKB1R b KQkq - 0 5", true, "d4", "e2", "White knight all moves")]
    [TestCase("r1bqkbnr/pppppp2/n5p1/7p/3N4/2P1P3/PP1P1PPP/RNBQKB1R w KQkq - 0 5", "r1bqkbnr/pppppp2/n5p1/7p/8/2P1P3/PPNP1PPP/RNBQKB1R b KQkq - 0 5", true, "d4", "c2", "White knight all moves")]
    [TestCase("r1bqkbnr/pppppp2/n5p1/7p/3N4/2P1P3/PP1P1PPP/RNBQKB1R w KQkq - 0 5", "r1bqkbnr/pppppp2/n5p1/7p/8/1NP1P3/PP1P1PPP/RNBQKB1R b KQkq - 0 5", true, "d4", "b3", "White knight all moves")]
    [TestCase("r1bqkbnr/pppppp2/n5p1/7p/3N4/2P1P3/PP1P1PPP/RNBQKB1R w KQkq - 0 5", "r1bqkbnr/pppppp2/n5p1/1N5p/8/2P1P3/PP1P1PPP/RNBQKB1R b KQkq - 0 5", true, "d4", "b5", "White knight all moves")]
    [TestCase("r1bqkbnr/pppppp2/n5p1/7p/3N4/2P1P3/PP1P1PPP/RNBQKB1R w KQkq - 0 5", "r1bqkbnr/pppppp2/n1N3p1/7p/8/2P1P3/PP1P1PPP/RNBQKB1R b KQkq - 0 5", true, "d4", "c6", "White knight all moves")]

    // Bishop Moves
    [TestCase("r1bqkbnr/pppppp2/n5p1/7p/3N4/2P1P3/PP1P1PPP/RNBQKB1R w KQkq - 0 5", "r1bqkbnr/pppppp2/n5p1/1B5p/3N4/2P1P3/PP1P1PPP/RNBQK2R b KQkq - 0 5", true, "f1", "b5", "Bishop moves")]
    [TestCase("r1bqkbnr/pppppp2/n5p1/7p/3N4/2P1P3/PP1P1PPP/RNBQKB1R w KQkq - 0 5", "r1bqkbnr/pppppp2/B5p1/7p/3N4/2P1P3/PP1P1PPP/RNBQK2R b KQkq - 0 5", true, "f1", "a6", "Bishop capture")]
    [TestCase("r1bqkbnr/pppppp2/n5p1/7p/3N4/2P1P3/PP1P1PPP/RNBQKB1R w KQkq - 0 5", "r1bqkbnr/pppppp2/n5p1/7p/3N4/2P1P3/PP1P1PPP/RNBQKB1R w KQkq - 0 5", false, "f1", "g2", "Bishop blocked 1")]
    [TestCase("r1bqkbnr/pppppp2/n5p1/7p/3N4/2P1P3/PP1P1PPP/RNBQKB1R w KQkq - 0 5", "r1bqkbnr/pppppp2/n5p1/7p/3N4/2P1P3/PP1P1PPP/RNBQKB1R w KQkq - 0 5", false, "f1", "h3", "Bishop blocked 1")]

    // Rook Moves
    [TestCase("r1bqkbnr/pppppp2/n5p1/7Q/3N4/2P1P3/PP1P1PPP/RNB1KB1R b KQkq - 0 5", "r1bqkbn1/pppppp2/n5pr/7Q/3N4/2P1P3/PP1P1PPP/RNB1KB1R w KQq - 0 6", true, "h8", "h6", "Rook moves")]
    [TestCase("r1bqkbnr/pppppp2/n5p1/7Q/3N4/2P1P3/PP1P1PPP/RNB1KB1R b KQkq - 0 5", "r1bqkbn1/pppppp2/n5p1/7r/3N4/2P1P3/PP1P1PPP/RNB1KB1R w KQq - 0 6", true, "h8", "h5", "Rook capture")]
    [TestCase("r1bqkbnr/pppppp2/n5p1/7p/3N4/2P1P3/PP1P1PPP/RNBQKB1R w KQkq - 0 5", "r1bqkbnr/pppppp2/n5p1/7p/3N4/2P1P3/PP1P1PPP/RNBQKB1R w KQkq - 0 5", false, "h1", "h2", "Rook blocked 1")]
    [TestCase("r1bqkbnr/pppppp2/n5p1/7Q/3N4/2P1P3/PP1P1PPP/RNB1KB1R b KQkq - 0 5", "r1bqkbnr/pppppp2/n5p1/7Q/3N4/2P1P3/PP1P1PPP/RNB1KB1R b KQkq - 0 5", false, "h8", "h4", "Rook blocked 2")]

    // Queen Moves
    [TestCase("r1bqkbnr/pppppp2/n5p1/7p/3N4/2P1P3/PP1P1PPP/RNBQKB1R w KQkq - 0 5", "r1bqkbnr/pppppp2/n5p1/7p/Q2N4/2P1P3/PP1P1PPP/RNB1KB1R b KQkq - 0 5", true, "d1", "a4", "Queen moves")]
    [TestCase("r1bqkbnr/pppppp2/n5p1/7p/3N4/2P1P3/PP1P1PPP/RNBQKB1R w KQkq - 0 5", "r1bqkbnr/pppppp2/n5p1/7Q/3N4/2P1P3/PP1P1PPP/RNB1KB1R b KQkq - 0 5", true, "d1", "h5", "Queen capture")]
    [TestCase("r1bqkbnr/pppppp2/n5p1/7p/3N4/2P1P3/PP1P1PPP/RNBQKB1R w KQkq - 0 5", "r1bqkbnr/pppppp2/n5p1/7p/3N4/2P1P3/PP1P1PPP/RNBQKB1R w KQkq - 0 5", false, "d1", "d2", "Queen blocked 1")]

    // King Moves
    [TestCase("8/5k2/8/2p5/P7/2P5/1r6/3K3R b - - 0 1", "8/8/5k2/2p5/P7/2P5/1r6/3K3R w - - 0 2", true, "f7", "f6", "King Move")]
    [TestCase("8/5k2/8/2p5/P7/2P5/1r6/3K3R b - - 0 1", "8/8/4k3/2p5/P7/2P5/1r6/3K3R w - - 0 2", true, "f7", "e6", "King Move")]
    //[TestCase("8/5k2/8/2p5/P7/2P5/1r4R1/3K4 b - - 0 1", "8/5k2/8/2p5/P7/2P5/1r4R1/3K4 b - - 0 1", false, "f7", "g7", "King Move into check")]

    public void TestMove(string startFen, string endFen, bool valid, string from, string to, string description)
    {
        var board = BoardFactory.Create(startFen);
        var move = board.MakeMove(from, to);
        move.Valid.ShouldBe(valid, description);
        endFen.ShouldBe(board.FEN, description);
    }
}
