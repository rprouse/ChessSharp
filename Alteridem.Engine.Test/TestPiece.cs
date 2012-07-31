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

      public Array GetPieceTypes()
      {
         return Enum.GetValues( typeof (PieceType) );
      }

      public IEnumerable<char> GetPieceTypeCharacters()
      {
         const string PIECES = "PNBRQKpnbrqk ";
         foreach (char piece in PIECES)
         {
            yield return piece;
         }
      }
   }
}
