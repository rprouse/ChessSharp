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
            Assert.AreEqual(piece, move.Promotion);
            Assert.AreEqual(true, move.Valid);
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
            Assert.AreEqual(captures, move.Captures);
            Assert.AreEqual(true, move.Valid);
        }

        [TestCase(false, MoveFlags.QuietMove)]
        [TestCase(true, MoveFlags.DoublePawnPush)]
        public void TestDoublePawnPush(bool push, MoveFlags flags)
        {
            var move = new Move(0, 0, flags);
            Assert.AreEqual(push, move.DoublePawnPush);
            Assert.AreEqual(true, move.Valid);
        }

        [TestCase(false, MoveFlags.QuietMove)]
        [TestCase(true, MoveFlags.KingCastle)]
        [TestCase(true, MoveFlags.QueenCastle)]
        public void TestCastle(bool castle, MoveFlags flags)
        {
            var move = new Move(0, 0, flags);
            Assert.AreEqual(castle, move.Castle);
            Assert.AreEqual(true, move.Valid);
        }

        [TestCase(true, MoveFlags.QuietMove)]
        [TestCase(true, MoveFlags.KingCastle)]
        [TestCase(true, MoveFlags.QueenCastle)]
        [TestCase(false, MoveFlags.Invalid)]
        [TestCase(false, MoveFlags.Invalid | MoveFlags.KingCastle)]
        public void TestInvalidMove(bool valid, MoveFlags flags)
        {
            var move = new Move(0, 0, flags);
            Assert.AreEqual(valid, move.Valid);
        }

        [TestCase(true, MoveFlags.QuietMove)]
        [TestCase(false, MoveFlags.KingCastle)]
        [TestCase(false, MoveFlags.QueenCastle)]
        [TestCase(false, MoveFlags.Invalid)]
        [TestCase(false, MoveFlags.Invalid | MoveFlags.KingCastle)]
        public void TestQuietMove(bool quiet, MoveFlags flags)
        {
            var move = new Move(0, 0, flags);
            Assert.AreEqual(quiet, move.QuietMove);
        }
    }
}
