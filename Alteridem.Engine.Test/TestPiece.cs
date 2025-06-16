using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Alteridem.Engine.Test
{
   [TestFixture]
   public class TestPiece
   {
      [TestCaseSource( nameof(GetPieceTypes))]
      public void TestWhitePieceType( PieceType type )
      {
         TestPieceType( type, PieceColour.White );
      }

      [TestCaseSource( nameof(GetPieceTypes))]
      public void TestBlackPieceType( PieceType type )
      {
         TestPieceType( type, PieceColour.Black );
      }

      private static void TestPieceType( PieceType type, PieceColour colour )
      {
         var piece = new Piece( type, colour );
         Assert.That( type, Is.EqualTo(piece.Type) );
         Assert.That( colour, Is.EqualTo(piece.Colour) );
      }

      [TestCaseSource( nameof(GetPieceTypeCharacters))]
      public void TestPieceTypeCharacters( char c )
      {
         var piece = new Piece( c );
         Assert.That( c, Is.EqualTo(piece.Character) );
      }

      public static Array GetPieceTypes() =>
         Enum.GetValues( typeof (PieceType) );

      public static IEnumerable<char> GetPieceTypeCharacters()
      {
         const string PIECES = "PNBRQKpnbrqk ";
         foreach (char piece in PIECES)
         {
            yield return piece;
         }
      }
   }
}
