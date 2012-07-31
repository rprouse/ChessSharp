namespace Alteridem.Engine
{
   /// <summary>
   /// The type of the piece, for example, pawn, knight, etc.
   /// </summary>
   public enum PieceType : byte
   {
      None = 0x00,
      WhitePawn = 0x01,
      BlackPawn = 0x02,
      Knight = 0x03,
      Bishop = 0x04,
      Rook = 0x05,
      Queen = 0x06,
      King = 0x07
   }
}