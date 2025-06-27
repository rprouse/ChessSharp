namespace Chess.Engine.Test;

[TestFixture]
public class MoveTests
{
    [TestCase(PieceType.None, MoveFlags.QuietMove)]
    [TestCase(PieceType.None, MoveFlags.QueenCastle)]
    [TestCase(PieceType.Knight, MoveFlags.KnightPromotion)]
    [TestCase(PieceType.Bishop, MoveFlags.BishopPromotion)]
    [TestCase(PieceType.Rook, MoveFlags.RookPromotion)]
    [TestCase(PieceType.Queen, MoveFlags.QueenPromotion)]
    [TestCase(PieceType.Knight, MoveFlags.KnightPromotion | MoveFlags.Capture)]
    [TestCase(PieceType.Bishop, MoveFlags.BishopPromotion | MoveFlags.Capture)]
    [TestCase(PieceType.Rook, MoveFlags.RookPromotion | MoveFlags.Capture)]
    [TestCase(PieceType.Queen, MoveFlags.QueenPromotion | MoveFlags.Capture)]
    public void TestGetPromotion(PieceType piece, MoveFlags flags)
    {
        var move = new Move(0, 0, flags);
        piece.ShouldBe(move.Promotion);
        move.Valid.ShouldBeTrue();
    }

    [TestCase(false, MoveFlags.QuietMove)]
    [TestCase(false, MoveFlags.DoublePawnPush)]
    [TestCase(false, MoveFlags.QueenCastle)]
    [TestCase(false, MoveFlags.KingCastle)]
    [TestCase(false, MoveFlags.QueenCastle)]
    [TestCase(false, MoveFlags.KnightPromotion)]
    [TestCase(false, MoveFlags.BishopPromotion)]
    [TestCase(false, MoveFlags.RookPromotion)]
    [TestCase(false, MoveFlags.QueenPromotion)]
    [TestCase(true, MoveFlags.Capture)]
    [TestCase(true, MoveFlags.EnPassantCapture)]
    [TestCase(true, MoveFlags.KnightPromotion | MoveFlags.Capture)]
    [TestCase(true, MoveFlags.BishopPromotion | MoveFlags.Capture)]
    [TestCase(true, MoveFlags.RookPromotion | MoveFlags.Capture)]
    [TestCase(true, MoveFlags.QueenPromotion | MoveFlags.Capture)]
    public void TestCaptures(bool captures, MoveFlags flags)
    {
        var move = new Move(0, 0, flags);
        move.Captures.ShouldBe(captures);
        move.Valid.ShouldBeTrue();
    }

    [TestCase(false, MoveFlags.QuietMove)]
    [TestCase(true, MoveFlags.DoublePawnPush)]
    [TestCase(false, MoveFlags.EnPassantCapture)]
    public void TestDoublePawnPush(bool push, MoveFlags flags)
    {
        var move = new Move(0, 0, flags);
        move.DoublePawnPush.ShouldBe(push);
        move.Valid.ShouldBeTrue();
    }

    [TestCase(false, MoveFlags.QuietMove)]
    [TestCase(true, MoveFlags.KingCastle)]
    [TestCase(true, MoveFlags.QueenCastle)]
    public void TestCastle(bool castle, MoveFlags flags)
    {
        var move = new Move(0, 0, flags);
        move.Castle.ShouldBe(castle);
        move.Valid.ShouldBeTrue();
    }

    [TestCase(true, MoveFlags.QuietMove)]
    [TestCase(true, MoveFlags.KingCastle)]
    [TestCase(true, MoveFlags.QueenCastle)]
    [TestCase(false, MoveFlags.Invalid)]
    [TestCase(false, MoveFlags.Invalid | MoveFlags.KingCastle)]
    public void TestInvalidMove(bool valid, MoveFlags flags)
    {
        var move = new Move(0, 0, flags);
        move.Valid.ShouldBe(valid);
    }

    [TestCase(true, MoveFlags.QuietMove)]
    [TestCase(false, MoveFlags.KingCastle)]
    [TestCase(false, MoveFlags.QueenCastle)]
    [TestCase(false, MoveFlags.Invalid)]
    [TestCase(false, MoveFlags.Invalid | MoveFlags.KingCastle)]
    public void TestQuietMove(bool quiet, MoveFlags flags)
    {
        var move = new Move(0, 0, flags);
        move.QuietMove.ShouldBe(quiet);
    }

    [TestCase( 8, 24, MoveFlags.DoublePawnPush, 16)]
    [TestCase(15, 31, MoveFlags.DoublePawnPush, 23)]
    [TestCase(48, 32, MoveFlags.DoublePawnPush, 40)]
    [TestCase(55, 39, MoveFlags.DoublePawnPush, 47)]
    [TestCase(11, 19, MoveFlags.QuietMove, -1)]
    public void TestEnPassantTarget( int from, int to, MoveFlags flags, int enPassantTarget )
    {
        var move = new Move(from, to, flags);
        move.EnPassantTarget.ShouldBe(enPassantTarget);
    }

    [TestCase(2, 11, MoveFlags.QuietMove, 2, 11, MoveFlags.QuietMove, true)]
    [TestCase(18, 26, MoveFlags.QuietMove, 18, 26, MoveFlags.Capture, true)]
    [TestCase(2, 11, MoveFlags.QuietMove, 18, 26, MoveFlags.QuietMove, false)]
    public void TestEquality( int a1, int b1, MoveFlags f1, int a2, int b2, MoveFlags f2, bool areEqual )
    {
        var left = new Move(a1, b1, f1);
        var right = new Move(a2, b2, f2);
        (left == right).ShouldBe(areEqual);
        (left != right).ShouldNotBe(areEqual);
        left.Equals(right).ShouldBe(areEqual);
        (right == left).ShouldBe(areEqual);
        (right != left).ShouldNotBe(areEqual);
        right.Equals(left).ShouldBe(areEqual);
    }
}
