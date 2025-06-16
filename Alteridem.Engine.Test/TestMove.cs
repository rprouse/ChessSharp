using NUnit.Framework;

namespace Alteridem.Engine.Test
{
    [TestFixture]
    public class TestMove
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
            Assert.That(piece, Is.EqualTo(move.Promotion));
            Assert.That(true, Is.EqualTo(move.Valid));
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
            Assert.That(captures, Is.EqualTo(move.Captures));
            Assert.That(true, Is.EqualTo(move.Valid));
        }

        [TestCase(false, MoveFlags.QuietMove)]
        [TestCase(true, MoveFlags.DoublePawnPush)]
        [TestCase(false, MoveFlags.EnPassantCapture)]
        public void TestDoublePawnPush(bool push, MoveFlags flags)
        {
            var move = new Move(0, 0, flags);
            Assert.That(push, Is.EqualTo(move.DoublePawnPush));
            Assert.That(true, Is.EqualTo(move.Valid));
        }

        [TestCase(false, MoveFlags.QuietMove)]
        [TestCase(true, MoveFlags.KingCastle)]
        [TestCase(true, MoveFlags.QueenCastle)]
        public void TestCastle(bool castle, MoveFlags flags)
        {
            var move = new Move(0, 0, flags);
            Assert.That(castle, Is.EqualTo(move.Castle));
            Assert.That(true, Is.EqualTo(move.Valid));
        }

        [TestCase(true, MoveFlags.QuietMove)]
        [TestCase(true, MoveFlags.KingCastle)]
        [TestCase(true, MoveFlags.QueenCastle)]
        [TestCase(false, MoveFlags.Invalid)]
        [TestCase(false, MoveFlags.Invalid | MoveFlags.KingCastle)]
        public void TestInvalidMove(bool valid, MoveFlags flags)
        {
            var move = new Move(0, 0, flags);
            Assert.That(valid, Is.EqualTo(move.Valid));
        }

        [TestCase(true, MoveFlags.QuietMove)]
        [TestCase(false, MoveFlags.KingCastle)]
        [TestCase(false, MoveFlags.QueenCastle)]
        [TestCase(false, MoveFlags.Invalid)]
        [TestCase(false, MoveFlags.Invalid | MoveFlags.KingCastle)]
        public void TestQuietMove(bool quiet, MoveFlags flags)
        {
            var move = new Move(0, 0, flags);
            Assert.That(quiet, Is.EqualTo(move.QuietMove));
        }

        [TestCase( 8, 24, MoveFlags.DoublePawnPush, 16)]
        [TestCase(15, 31, MoveFlags.DoublePawnPush, 23)]
        [TestCase(48, 32, MoveFlags.DoublePawnPush, 40)]
        [TestCase(55, 39, MoveFlags.DoublePawnPush, 47)]
        [TestCase(11, 19, MoveFlags.QuietMove, -1)]
        public void TestEnPassantTarget( int from, int to, MoveFlags flags, int enPassantTarget )
        {
            var move = new Move(from, to, flags);
            Assert.That(enPassantTarget, Is.EqualTo(move.EnPassantTarget));
        }

        [TestCase(2, 11, MoveFlags.QuietMove, 2, 11, MoveFlags.QuietMove, true)]
        [TestCase(18, 26, MoveFlags.QuietMove, 18, 26, MoveFlags.Capture, true)]
        [TestCase(2, 11, MoveFlags.QuietMove, 18, 26, MoveFlags.QuietMove, false)]
        public void TestEquality( int a1, int b1, MoveFlags f1, int a2, int b2, MoveFlags f2, bool areEqual )
        {
            var left = new Move(a1, b1, f1);
            var right = new Move(a2, b2, f2);
            Assert.That(areEqual, Is.EqualTo(left == right));
            Assert.That(areEqual, Is.Not.EqualTo(left != right));
            Assert.That(areEqual, Is.EqualTo(left.Equals(right)));
            Assert.That(areEqual, Is.EqualTo(right == left));
            Assert.That(areEqual, Is.Not.EqualTo(right != left));
            Assert.That(areEqual, Is.EqualTo(right.Equals(left)));
        }
    }
}
