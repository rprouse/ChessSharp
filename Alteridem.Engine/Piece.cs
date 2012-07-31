namespace Alteridem.Engine
{
   public struct Piece
   {
      private readonly byte _store;

      public Piece( PieceType type, PieceColour colour )
      {
         _store = (byte) ((byte) type | (byte) colour);
      }

      /// <summary>
      /// Creates a piece from the FEN diagram character
      /// </summary>
      /// <param name="piece"></param>
      public Piece( char piece )
      {
         if ( !"PNBRQKpnbrqk".Contains( piece.ToString() ) )
         {
            _store = 0x00;
            return;
         }
         PieceColour colour = char.IsUpper( piece ) ? PieceColour.White : PieceColour.Black;
         PieceType type = PieceType.None;

         switch (char.ToLower( piece ))
         {
            case 'p':
               type = colour == PieceColour.White ? PieceType.WhitePawn : PieceType.BlackPawn;
               break;
            case 'n':
               type = PieceType.Knight;
               break;
            case 'b':
               type = PieceType.Bishop;
               break;
            case 'r':
               type = PieceType.Rook;
               break;
            case 'q':
               type = PieceType.Queen;
               break;
            case 'k':
               type = PieceType.King;
               break;
         }
            
         _store = (byte) ((byte) type | (byte) colour);
      }

      public Piece( byte piece )
      {
         _store = piece;
      }

      /// <summary>
      /// The type of the piece, for example, pawn, knight, etc.
      /// </summary>
      public PieceType Type
      {
         get { return (PieceType)(_store & 0x07); }
      }

      /// <summary>
      /// The colour of the piece, black or white
      /// </summary>
      public PieceColour Colour
      {
         get { return (PieceColour)(_store & 0x08); }
      }

      /// <summary>
      /// Returns the character that represents the piece as per a FEN diagram
      /// </summary>
      public char Character
      {
         get
         {
            switch ( Type )
            {
               case PieceType.WhitePawn:
                  return 'P';
               case PieceType.BlackPawn:
                  return 'p';
               case PieceType.Knight:
                  return Colour == PieceColour.White ? 'N' : 'n';
               case PieceType.Bishop:
                  return Colour == PieceColour.White ? 'B' : 'b';
               case PieceType.Rook:
                  return Colour == PieceColour.White ? 'R' : 'r';
               case PieceType.Queen:
                  return Colour == PieceColour.White ? 'Q' : 'q';
               case PieceType.King:
                  return Colour == PieceColour.White ? 'K' : 'k';
               default:
                  return ' ';
            }
         }
      }
   }
}