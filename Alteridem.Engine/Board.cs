using System;
using System.Text;

namespace Alteridem.Engine
{
   /// <summary>
   /// How should we initialize the board
   /// </summary>
   public enum BoardInitialization
   {
      /// <summary>
      /// A blank board with no pieces on it
      /// </summary>
      Blank,
      /// <summary>
      /// A standard chess setup
      /// </summary>
      Standard,
      /// <summary>
      /// A Chess960 (Fischer Random Chess) setup
      /// </summary>
      Chess960
   }

   public class Board
   {
      // An 8*8 Board 
      //
      //      a  b  c  d  e  f  g  h
      //    -------------------------
      // 8 | 56 57 58 59 60 61 62 63 | 8
      // 7 | 48 49 50 51 52 53 54 55 | 7
      // 6 | 40 41 42 43 44 45 46 47 | 6
      // 5 | 32 33 34 35 36 37 38 39 | 5
      // 4 | 24 25 26 27 28 29 30 31 | 4
      // 3 | 16 17 18 19 20 21 22 23 | 3
      // 2 | 08 09 10 11 12 13 14 15 | 2
      // 1 | 00 01 02 03 04 05 06 07 | 1
      //    -------------------------
      //      a  b  c  d  e  f  g  h
      #region Private Members

      private readonly Piece[] _board = new Piece[64];

      // The index into the board if a pawn just made a two square move. It is the square behind the pawn. 0xFF otherwise.
      private byte _enPassantTarget = 0xFF;

      // Castling Availability
      private bool _whiteKingside = true;
      private bool _whiteQueenside = true;
      private bool _blackKingside = true;
      private bool _blackQueenside = true;

      // Who has the next move?
      private PieceColour _activeColour = PieceColour.White;

      // This is the number of halfmoves since the last pawn advance or capture. This is used to determine if a draw can be claimed under the fifty-move rule.
      private int _halfMoveClock = 0;

      // The number of the full move. It starts at 1, and is incremented after Black's move.
      private int _fullMoveNumber = 0;

      #endregion

      #region Construction

      /// <summary>
      /// Constructs a board based on the given setup
      /// </summary>
      public Board( BoardInitialization board )
      {
         switch ( board )
         {
            case BoardInitialization.Blank:
               InitializeBlankBoard();
               break;
            case BoardInitialization.Standard:
               InitializeStandardBoard();
               break;
            case BoardInitialization.Chess960:
               InitializeChess960Board();
               break;
         }
      }

      private void InitializeBlankBoard()
      {
         for ( int i = 0; i < 64; i++ )
         {
            _board[i] = new Piece( 0x0 );
         }
      }

      private void InitializeStandardBoard()
      {
         // Clear all the squares
         InitializeBlankBoard();

         // Setup white
         const string WHITE = "RNBQKBNRPPPPPPPP";
         for (int i = 0; i < 16; i++)
         {
            _board[i] = new Piece( WHITE[i] );
         }

         // Setup black
         const string BLACK = "pppppppprnbqkbnr";
         for ( int i = 0; i < 16; i++ )
         {
            _board[i+48] = new Piece( BLACK[i] );
         }
      }

      private void InitializeChess960Board()
      {
         // TODO: Setup a Chess960 board
         throw new System.NotImplementedException();
      }

      // TODO: Add a FEN Constructor, http://en.wikipedia.org/wiki/Forsyth%E2%80%93Edwards_Notation

      #endregion

      #region Helper Methods

      /// <summary>
      /// A simple text representation of the board
      /// </summary>
      /// <returns></returns>
      public override string ToString()
      {
         var builder  = new StringBuilder(80);
         for (int rank = 7; rank >= 0; rank--)
         {
            for (int file = 0; file < 8; file++)
            {
               int i = rank*8 + file;
               char piece = _board[i].Character;
               if ( piece == ' ' )
                  piece = '-';
               builder.Append( piece );
            }
            builder.Append( Environment.NewLine );
         }
         return builder.ToString();
      }

      /// <summary>
      /// Gets an index into the board array for a given square designation.
      /// </summary>
      /// <param name="square">The square designation, ex</param>
      /// <returns></returns>
      public static int IndexFromSquare( string square )
      {
         if ( string.IsNullOrWhiteSpace( square ) )
         {
            throw new ArgumentNullException( "square" );
         }
         if ( square.Length != 2 ||
              !char.IsLetter( square[0] ) ||
              square[1] < '1' || square[1] > '8' ||
              char.ToLowerInvariant( square[0] ) > 'h'
            )
         {
            throw new ArgumentException( "square must be in the form a5" );
         }
         // Zero based
         int file = char.ToLowerInvariant( square[0] ) - 'a';
         int rank = square[1] - '1';

         return rank*8 + file;
      }

      /// <summary>
      /// Given an index into the board, returns a square designation
      /// </summary>
      /// <param name="index"></param>
      /// <returns></returns>
      public static string SquareFromIndex( int index )
      {
         if ( index < 0 || index > 63 )
         {
            throw new ArgumentException( "index must be between 0 and 63" );
         }
         StringBuilder builder = new StringBuilder( 2 );
         char file = (char)(index % 8);
         char rank = (char)(index / 8);
         builder.Append( (char)('a' + file) );
         builder.Append( (char)('1' + rank) );
         return builder.ToString();
      }

      #endregion
   }
}
