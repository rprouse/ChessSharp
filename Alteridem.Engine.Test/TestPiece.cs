using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Alteridem.Engine.Test
{
   [TestFixture]
   public class TestPiece
   {
      [TestCaseSource( "GetPieceTypes" )]
      public void TestWhitePieceType( PieceType type )
      {
         TestPieceType( type, PieceColour.White );
      }

      [TestCaseSource( "GetPieceTypes" )]
      public void TestBlackPieceType( PieceType type )
      {
         TestPieceType( type, PieceColour.Black );
      }

      private static void TestPieceType( PieceType type, PieceColour colour )
      {
         var piece = new Piece( type, colour );
         Assert.AreEqual( type, piece.Type );
         Assert.AreEqual( colour, piece.Colour );
      }

      [TestCaseSource( "GetPieceTypeCharacters" )]
      public void TestPieceTypeCharacters( char c )
      {
         var piece = new Piece( c );
         Assert.AreEqual( c, piece.Character );
      }

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

      public Array GetPieceTypes()
      {
         return Enum.GetValues( typeof (PieceType) );
      }

      public IEnumerable<char> GetPieceTypeCharacters()
      {
         const string pieces = "PNBRQKpnbrqk ";
         foreach (char piece in pieces)
         {
            yield return piece;
         }
      }
   }
}
