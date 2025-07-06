namespace Chess.Engine.Test;

public class CheckTests
{
    // Rook on same rank black
    [TestCase("8/Rk6/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("8/1kR5/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("8/1k1R4/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("8/1k2R3/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("8/1k3R2/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("8/1k4R1/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("8/1k5R/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, true)]

    // Queen on same rank black
    [TestCase("8/Qk6/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("8/1kQ5/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("8/1k1Q4/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("8/1k2Q3/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("8/1k3Q2/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("8/1k4Q1/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("8/1k5Q/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, true)]

    // Rook on same rank white
    [TestCase("8/rK6/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, true)]
    [TestCase("8/1Kr5/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, true)]
    [TestCase("8/1K1r4/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, true)]
    [TestCase("8/1K2r3/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, true)]
    [TestCase("8/1K3r2/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, true)]
    [TestCase("8/1K4r1/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, true)]
    [TestCase("8/1K5r/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, true)]

    // Queen on same rank white
    [TestCase("8/qK6/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, true)]
    [TestCase("8/1Kq5/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, true)]
    [TestCase("8/1K1q4/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, true)]
    [TestCase("8/1K2q3/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, true)]
    [TestCase("8/1K3q2/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, true)]
    [TestCase("8/1K4q1/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, true)]
    [TestCase("8/1K5q/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, true)]

    // Blocked Rook on same rank black
    [TestCase("8/Rpk5/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, false)]
    [TestCase("8/1kpR4/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, false)]
    [TestCase("8/1kp1R3/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, false)]
    [TestCase("8/1kp2R2/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, false)]
    [TestCase("8/1kp3R1/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, false)]
    [TestCase("8/1kp4R/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, false)]

    // Blocked Queen on same rank black
    [TestCase("8/Qpk5/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, false)]
    [TestCase("8/1kpQ4/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, false)]
    [TestCase("8/1kp1Q3/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, false)]
    [TestCase("8/1kp2Q2/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, false)]
    [TestCase("8/1kp3Q1/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, false)]
    [TestCase("8/1kp4Q/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, false)]

    // Blocked Rook on same rank white
    [TestCase("8/rpK5/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, false)]
    [TestCase("8/1Kpr4/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, false)]
    [TestCase("8/1Kp1r3/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, false)]
    [TestCase("8/1Kp2r2/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, false)]
    [TestCase("8/1Kp3r1/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, false)]
    [TestCase("8/1Kp4r/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, false)]

    // Blocked Queen on same rank white
    [TestCase("8/qpK5/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, false)]
    [TestCase("8/1Kpq4/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, false)]
    [TestCase("8/1Kp1q3/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, false)]
    [TestCase("8/1Kp2q2/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, false)]
    [TestCase("8/1Kp3q1/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, false)]
    [TestCase("8/1Kp4q/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, false)]

    // Rook on same file
    [TestCase("1R6/1k6/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("8/1k6/8/8/8/8/6K1/1R6 b - - 0 1", PieceColour.Black, true)]
    [TestCase("1r6/1K6/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, true)]
    [TestCase("8/1K6/8/8/8/8/6k1/1r6 b - - 0 1", PieceColour.White, true)]

    // Queen on same file
    [TestCase("1Q6/1k6/8/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("8/1k6/8/8/8/8/6K1/1Q6 b - - 0 1", PieceColour.Black, true)]
    [TestCase("1q6/1K6/8/8/8/8/6k1/8 b - - 0 1", PieceColour.White, true)]
    [TestCase("8/1K6/8/8/8/8/6k1/1q6 b - - 0 1", PieceColour.White, true)]

    // Blocked Rook on same file
    [TestCase("1R6/1p6/1k6/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, false)]
    [TestCase("8/8/1k6/1p6/8/8/6K1/1R6 b - - 0 1", PieceColour.Black, false)]
    [TestCase("1r6/1P6/1K6/8/8/8/6k1/8 b - - 0 1", PieceColour.White, false)]
    [TestCase("8/8/1K6/1P6/8/8/6k1/1r6 b - - 0 1", PieceColour.White, false)]

    // Blocked Queen on same file
    [TestCase("1Q6/1p6/1k6/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, false)]
    [TestCase("8/8/1k6/1p6/8/8/6K1/1Q6 b - - 0 1", PieceColour.Black, false)]
    [TestCase("1q6/1P6/1K6/8/8/8/6k1/8 b - - 0 1", PieceColour.White, false)]
    [TestCase("8/8/1K6/1P6/8/8/6k1/1q6 b - - 0 1", PieceColour.White, false)]

    // Bishop on same diagonal
    [TestCase("8/B7/1k6/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("8/8/1k6/8/8/8/6K1/6B1 b - - 0 1", PieceColour.Black, true)]
    [TestCase("8/8/1k6/B7/8/8/6K1/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("3B4/8/1k6/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("8/b7/1K6/8/8/8/6k1/8 b - - 0 1", PieceColour.White, true)]
    [TestCase("8/8/1K6/8/8/8/6k1/6b1 b - - 0 1", PieceColour.White, true)]
    [TestCase("8/8/1K6/b7/8/8/6k1/8 b - - 0 1", PieceColour.White, true)]
    [TestCase("3b4/8/1K6/8/8/8/6k1/8 b - - 0 1", PieceColour.White, true)]

    // Queen on same diagonal
    [TestCase("8/Q7/1k6/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("8/8/1k6/8/8/8/6K1/6Q1 b - - 0 1", PieceColour.Black, true)]
    [TestCase("8/8/1k6/Q7/8/8/6K1/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("3Q4/8/1k6/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("8/q7/1K6/8/8/8/6k1/8 b - - 0 1", PieceColour.White, true)]
    [TestCase("8/8/1K6/8/8/8/6k1/6q1 b - - 0 1", PieceColour.White, true)]
    [TestCase("8/8/1K6/q7/8/8/6k1/8 b - - 0 1", PieceColour.White, true)]
    [TestCase("3q4/8/1K6/8/8/8/6k1/8 b - - 0 1", PieceColour.White, true)]

    // Blocked Bishop on same diagonal
    [TestCase("4B3/3p4/2k5/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, false)]
    [TestCase("8/8/2k5/1p6/B7/8/6K1/8 b - - 0 1", PieceColour.Black, false)]
    [TestCase("B7/1p6/2k5/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, false)]
    [TestCase("8/8/2k5/3p4/8/8/5K2/7B b - - 0 1", PieceColour.Black, false)]
    [TestCase("4b3/3P4/2K5/8/8/8/6k1/8 b - - 0 1", PieceColour.White, false)]
    [TestCase("8/8/2K5/1P6/b7/8/6k1/8 b - - 0 1", PieceColour.White, false)]
    [TestCase("b7/1P6/2K5/8/8/8/6k1/8 b - - 0 1", PieceColour.White, false)]
    [TestCase("8/8/2K5/3P4/8/8/5k2/7b b - - 0 1", PieceColour.White, false)]

    // Blocked Queen on same diagonal
    [TestCase("4Q3/3p4/2k5/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, false)]
    [TestCase("8/8/2k5/1p6/Q7/8/6K1/8 b - - 0 1", PieceColour.Black, false)]
    [TestCase("Q7/1p6/2k5/8/8/8/6K1/8 b - - 0 1", PieceColour.Black, false)]
    [TestCase("8/8/2k5/3p4/8/8/5K2/7Q b - - 0 1", PieceColour.Black, false)]
    [TestCase("4q3/3P4/2K5/8/8/8/6k1/8 b - - 0 1", PieceColour.White, false)]
    [TestCase("8/8/2K5/1P6/q7/8/6k1/8 b - - 0 1", PieceColour.White, false)]
    [TestCase("q7/1P6/2K5/8/8/8/6k1/8 b - - 0 1", PieceColour.White, false)]
    [TestCase("8/8/2K5/3P4/8/8/5k2/7q b - - 0 1", PieceColour.White, false)]

    // Knight checks
    [TestCase("1N6/8/2k5/8/8/8/5K2/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("3N4/8/2k5/8/8/8/5K2/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("8/N7/2k5/8/8/8/5K2/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("8/8/2k5/N7/8/8/5K2/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("1k6/8/N7/8/8/8/5K2/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("1k6/8/2N5/8/8/8/5K2/8 b - - 0 1", PieceColour.Black, true)]
    [TestCase("8/8/8/8/8/8/k4K2/2N5 b - - 0 1", PieceColour.Black, true)]
    [TestCase("8/8/8/8/8/8/2N2K2/k7 b - - 0 1", PieceColour.Black, true)]
    [TestCase("8/8/2K5/8/8/8/5N2/7k b - - 0 1", PieceColour.Black, true)]
    [TestCase("1n6/8/2K5/8/8/8/5k2/8 b - - 0 1", PieceColour.White, true)]
    [TestCase("3n4/8/2K5/8/8/8/5k2/8 b - - 0 1", PieceColour.White, true)]
    [TestCase("8/n7/2K5/8/8/8/5k2/8 b - - 0 1", PieceColour.White, true)]
    [TestCase("8/8/2K5/n7/8/8/5k2/8 b - - 0 1", PieceColour.White, true)]
    [TestCase("1K6/8/n7/8/8/8/5k2/8 b - - 0 1", PieceColour.White, true)]
    [TestCase("1K6/8/2n5/8/8/8/5k2/8 b - - 0 1", PieceColour.White, true)]
    [TestCase("8/8/8/8/8/8/K4k2/2n5 b - - 0 1", PieceColour.White, true)]
    [TestCase("8/8/8/8/8/8/2n2k2/K7 b - - 0 1", PieceColour.White, true)]
    [TestCase("8/8/2k5/8/8/8/5n2/7K b - - 0 1", PieceColour.White, true)]

    public void CanDetermineCheck(string fen, PieceColour kingColour, bool expected)
    {
        var board = BoardFactory.Create(fen);
        int kingIndex = board.FindKing(kingColour);
        var result = board.IsInCheck(kingIndex);
        result.ShouldBe(expected);
    }
}
