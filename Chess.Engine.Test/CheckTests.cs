namespace Chess.Engine.Test;

public class CheckTests
{
    // Rook on same rank
    [TestCase("8/Rk6/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("8/1kR5/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("8/1k1R4/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("8/1k2R3/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("8/1k3R2/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("8/1k4R1/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("8/1k5R/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, true)]

    // Queen on same rank
    [TestCase("8/Qk6/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("8/1kQ5/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("8/1k1Q4/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("8/1k2Q3/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("8/1k3Q2/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("8/1k4Q1/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("8/1k5Q/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, true)]

    // Rook on same rank
    [TestCase("8/rK6/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, true)]
    [TestCase("8/1Kr5/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, true)]
    [TestCase("8/1K1r4/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, true)]
    [TestCase("8/1K2r3/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, true)]
    [TestCase("8/1K3r2/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, true)]
    [TestCase("8/1K4r1/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, true)]
    [TestCase("8/1K5r/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, true)]

    // Queen on same rank
    [TestCase("8/qK6/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, true)]
    [TestCase("8/1Kq5/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, true)]
    [TestCase("8/1K1q4/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, true)]
    [TestCase("8/1K2q3/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, true)]
    [TestCase("8/1K3q2/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, true)]
    [TestCase("8/1K4q1/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, true)]
    [TestCase("8/1K5q/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, true)]

    // Blocked Rook on same rank
    [TestCase("8/Rpk5/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, false)]
    [TestCase("8/1kpR4/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, false)]
    [TestCase("8/1kp1R3/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, false)]
    [TestCase("8/1kp2R2/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, false)]
    [TestCase("8/1kp3R1/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, false)]
    [TestCase("8/1kp4R/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, false)]

    // Blocked Queen on same rank
    [TestCase("8/Qpk5/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, false)]
    [TestCase("8/1kpQ4/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, false)]
    [TestCase("8/1kp1Q3/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, false)]
    [TestCase("8/1kp2Q2/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, false)]
    [TestCase("8/1kp3Q1/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, false)]
    [TestCase("8/1kp4Q/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, false)]

    // Blocked Rook on same rank
    [TestCase("8/rpK5/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, false)]
    [TestCase("8/1Kpr4/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, false)]
    [TestCase("8/1Kp1r3/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, false)]
    [TestCase("8/1Kp2r2/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, false)]
    [TestCase("8/1Kp3r1/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, false)]
    [TestCase("8/1Kp4r/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, false)]

    // Blocked Queen on same rank
    [TestCase("8/qpK5/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, false)]
    [TestCase("8/1Kpq4/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, false)]
    [TestCase("8/1Kp1q3/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, false)]
    [TestCase("8/1Kp2q2/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, false)]
    [TestCase("8/1Kp3q1/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, false)]
    [TestCase("8/1Kp4q/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, false)]
    public void CanDetermineCheck(string fen, PieceColour colour, bool expected)
    {
        var board = BoardFactory.Create(fen);
        int kingIndex = board.FindKing(colour);
        var result = board.IsInCheck(kingIndex);
        result.ShouldBe(expected);
    }
}
