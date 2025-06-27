using System;
using Shouldly;
using NUnit.Framework;

namespace Chess.Engine.Test
{
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
    }
}
