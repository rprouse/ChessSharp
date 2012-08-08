using NUnit.Framework;

namespace Alteridem.Engine.Test
{
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
            Assert.AreEqual(index, Board.IndexFromSquare(square));
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
            Assert.AreEqual(square, Board.SquareFromIndex(index));
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
            Assert.AreEqual(expectedIndex, Board.Index(rank, file));
        }

        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")]
        [TestCase("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1")]
        [TestCase("rnbqkbnr/pp1ppppp/8/2p5/4P3/8/PPPP1PPP/RNBQKBNR w KQkq c6 0 2")]
        [TestCase("rnbqkbnr/pp1ppppp/8/2p5/4P3/5N2/PPPP1PPP/RNBQKB1R b KQkq - 1 2")]
        [TestCase("rnbqk2r/pp2ppbp/3p1np1/2pN4/2B1P3/5Q2/PPPP1PPP/R1B1K1NR w KQkq - 5 6")]
        [TestCase("8/2p4k/2p2K1p/2Pb2p1/p5p1/P1P5/8/8 w - - 4 37")]
        [TestCase("r1b1k2r/2p1b3/p1p4p/1p4p1/8/8/PPP2PPP/RNB1Q1K1 b kq - 2 16")]
        public void TestFen(string fen)
        {
            var board = new Board(fen);
            string newFen = board.FEN;
            Assert.AreEqual(fen, newFen);
        }

        // General Errors
        [TestCase("rnbqkbnr/pppp1ppp/4p3/8/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 2", "rnbqkbnr/pppp1ppp/4p3/8/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 2", false, 36, 28, "Wrong colour moving")]
        [TestCase("rnbqkbnr/pppp1ppp/4p3/8/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 2", "rnbqkbnr/pppp1ppp/4p3/8/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 2", false, 25, 33, "No piece to move")]
        [TestCase("rnbqkbnr/pppp1ppp/4p3/8/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 2", "rnbqkbnr/pppp1ppp/4p3/8/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 2", false, -1, 33, "from low")]
        [TestCase("rnbqkbnr/pppp1ppp/4p3/8/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 2", "rnbqkbnr/pppp1ppp/4p3/8/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 2", false, 64, 33, "from high")]
        [TestCase("rnbqkbnr/pppp1ppp/4p3/8/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 2", "rnbqkbnr/pppp1ppp/4p3/8/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 2", false, 33, -1, "to low")]
        [TestCase("rnbqkbnr/pppp1ppp/4p3/8/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 2", "rnbqkbnr/pppp1ppp/4p3/8/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 2", false, 33, 64, "to high")]
        [TestCase("rnbqkbnr/ppp2ppp/3p4/4p3/4P3/3P4/PPP2PPP/RNBQKBNR w KQkq - 0 3", "rnbqkbnr/ppp2ppp/3p4/4p3/4P3/3P4/PPP2PPP/RNBQKBNR w KQkq - 0 3", false, 19, 28, "Capture our own piece")]
        // Capture the king

        // White Pawn Moves
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "rnbqkbnr/pppppppp/8/8/8/4P3/PPPP1PPP/RNBQKBNR b KQkq - 0 1", true, 12, 20, "White pawn, one square")]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1", true, 12, 28, "White pawn, two squares")]
        [TestCase("rnbqkbnr/pppp1ppp/4p3/8/8/4P3/PPPP1PPP/RNBQKBNR w KQkq - 0 2", "rnbqkbnr/pppp1ppp/4p3/8/8/4P3/PPPP1PPP/RNBQKBNR w KQkq - 0 2", false, 20, 36, "White pawn, two squares, not first row")]
        [TestCase("rnbqkbnr/pppp1ppp/8/4p3/4P3/8/PPPP1PPP/RNBQKBNR w KQkq e6 0 2", "rnbqkbnr/pppp1ppp/8/4p3/4P3/8/PPPP1PPP/RNBQKBNR w KQkq e6 0 2", false, 28, 36, "White pawn, one square, blocked")]
        [TestCase("rnbqkbnr/pppp2pp/8/4p3/4Pp2/3P2P1/PPP2P1P/RNBQKBNR w KQkq - 0 4", "rnbqkbnr/pppp2pp/8/4p3/4Pp2/3P2P1/PPP2P1P/RNBQKBNR w KQkq - 0 4", false, 13, 29, "White pawn, two square, blocked 1")]
        [TestCase("rnbqkbnr/pppp2pp/8/4p3/4P3/3P1pPP/PPP2P2/RNBQKBNR w KQkq - 0 5", "rnbqkbnr/pppp2pp/8/4p3/4P3/3P1pPP/PPP2P2/RNBQKBNR w KQkq - 0 5", false, 13, 29, "White pawn, two square, blocked 2")]
        [TestCase("rnbqkbnr/ppp1pppp/8/3p4/4P3/8/PPPP1PPP/RNBQKBNR w KQkq d6 0 2", "rnbqkbnr/ppp1pppp/8/3P4/8/8/PPPP1PPP/RNBQKBNR b KQkq - 0 2", true, 28, 35, "White pawn, capture left")]
        [TestCase("rnbqkbnr/ppppp1pp/8/5p2/4P3/8/PPPP1PPP/RNBQKBNR w KQkq f6 0 2", "rnbqkbnr/ppppp1pp/8/5P2/8/8/PPPP1PPP/RNBQKBNR b KQkq - 0 2", true, 28, 37, "White pawn, capture right")]
        [TestCase("rnbqkbnr/pppp2pp/8/4pP2/8/8/PPPP1PPP/RNBQKBNR w KQkq e6 0 3", "rnbqkbnr/pppp2pp/4P3/8/8/8/PPPP1PPP/RNBQKBNR b KQkq - 0 3", true, 37, 44, "White pawn, en-passant capture")]
        [TestCase("rnbqkbnr/ppp3pp/3p4/4pP2/6P1/8/PPPP1P1P/RNBQKBNR w KQkq - 0 4", "rnbqkbnr/ppp3pp/3p4/4pP2/6P1/8/PPPP1P1P/RNBQKBNR w KQkq - 0 4", false, 37, 44, "White pawn, en-passant capture no longer valid")]

        // Black Pawn Moves
        [TestCase("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1", "rnbqkbnr/pppp1ppp/4p3/8/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 2", true, 52, 44, "Black pawn, one square")]
        [TestCase("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1", "rnbqkbnr/pppp1ppp/8/4p3/4P3/8/PPPP1PPP/RNBQKBNR w KQkq e6 0 2", true, 52, 36, "Black pawn, two squares")]
        [TestCase("rnbqkbnr/pppp1ppp/4p3/8/8/4PP2/PPPP2PP/RNBQKBNR b KQkq - 0 2", "rnbqkbnr/pppp1ppp/4p3/8/8/4PP2/PPPP2PP/RNBQKBNR b KQkq - 0 2", false, 44, 28, "Black pawn, two squares, not first row")]
        [TestCase("rnbqkbnr/pppp1ppp/8/4p3/4P3/3P4/PPP2PPP/RNBQKBNR b KQkq - 0 2", "rnbqkbnr/pppp1ppp/8/4p3/4P3/3P4/PPP2PPP/RNBQKBNR b KQkq - 0 2", false, 36, 28, "Black pawn, one square, blocked")]
        // 2 square blocked 1
        // 2 square blocked 2
        // Test capture left
        // Test capture right
        // Test en-passant capture
        // Test en-passant capture no longer valid

        public void TestMove(string startFen, string endFen, bool valid, int from, int to, string description)
        {
            var board = new Board(startFen);
            var move = board.MakeMove(from, to);
            Assert.AreEqual(valid, move.Valid, description);
            Assert.AreEqual(endFen, board.FEN, description);
        }

        public void TestBlackPawnSingleRow()
        {
        }

        public void TestBlackPawnDoubleRow()
        {
        }

        public void TestBlackPawnSingleRowBlocked()
        {
        }

        public void TestBlackPawnDoubleRowBlockedOne()
        {
        }

        public void TestBlackPawnDoubleRowBlockedTwo()
        {
        }
    }
}
