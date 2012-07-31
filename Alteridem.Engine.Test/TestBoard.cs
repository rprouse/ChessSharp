using NUnit.Framework;

namespace Alteridem.Engine.Test
{
   [TestFixture]
   public class TestBoard
   {
      [TestCase( "-",  -1 )]
      [TestCase( "a1", 00 )]
      [TestCase( "a8", 56 )]
      [TestCase( "a5", 32 )]
      [TestCase( "b1", 01 )]
      [TestCase( "b7", 49 )]
      [TestCase( "c6", 42 )]
      [TestCase( "d5", 35 )]
      [TestCase( "e4", 28 )]
      [TestCase( "h1", 07 )]
      [TestCase( "h8", 63 )]
      public void TestIndexFromSquare( string square, int index )
      {
         Assert.AreEqual( index, Board.IndexFromSquare( square ) );
      }

      [TestCase( "-", -1 )]
      [TestCase( "a1", 00 )]
      [TestCase( "a8", 56 )]
      [TestCase( "a5", 32 )]
      [TestCase( "b1", 01 )]
      [TestCase( "b7", 49 )]
      [TestCase( "c6", 42 )]
      [TestCase( "d5", 35 )]
      [TestCase( "e4", 28 )]
      [TestCase( "h1", 07 )]
      [TestCase( "h8", 63 )]
      public void TestSquareFromIndex( string square, int index )
      {
         Assert.AreEqual( square, Board.SquareFromIndex( index ) );
      }


      [TestCase( -1, -1, -1 )]
      [TestCase( 0, 0, 00 )]
      [TestCase( 0, 7, 56 )]
      [TestCase( 0, 4, 32 )]
      [TestCase( 1, 0, 01 )]
      [TestCase( 1, 6, 49 )]
      [TestCase( 2, 5, 42 )]
      [TestCase( 3, 4, 35 )]
      [TestCase( 4, 3, 28 )]
      [TestCase( 7, 0, 07 )]
      [TestCase( 7, 7, 63 )]
      public void TestIndex( int file, int rank, int expectedIndex )
      {
         Assert.AreEqual( expectedIndex, Board.Index( rank, file ) );
      }

      [TestCase( "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1" )]
      [TestCase( "rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1" )]
      [TestCase( "rnbqkbnr/pp1ppppp/8/2p5/4P3/8/PPPP1PPP/RNBQKBNR w KQkq c6 0 2" )]
      [TestCase( "rnbqkbnr/pp1ppppp/8/2p5/4P3/5N2/PPPP1PPP/RNBQKB1R b KQkq - 1 2" )]
      [TestCase( "rnbqk2r/pp2ppbp/3p1np1/2pN4/2B1P3/5Q2/PPPP1PPP/R1B1K1NR w KQkq - 5 6" )]
      [TestCase( "8/2p4k/2p2K1p/2Pb2p1/p5p1/P1P5/8/8 w - - 4 37" ) ]
      [TestCase( "r1b1k2r/2p1b3/p1p4p/1p4p1/8/8/PPP2PPP/RNB1Q1K1 b kq - 2 16" )]
      public void TestFen( string fen )
      {
         var board = new Board( fen );
         string newFen = board.FEN;
         Assert.AreEqual( fen, newFen );
      }
   }
}
