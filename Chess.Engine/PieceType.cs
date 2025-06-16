namespace Chess.Engine;

/// <summary>
/// The type of the piece, for example, pawn, knight, etc.
/// </summary>
public enum PieceType : byte
{
    None = 0x00,
    WhitePawn = 0x01,
    BlackPawn = 0x02,
    Knight = 0x04,
    Bishop = 0x05,
    Rook = 0x06,
    Queen = 0x07,
    King = 0x08
}