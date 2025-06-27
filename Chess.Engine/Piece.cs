using System.Diagnostics;
using System.Globalization;

namespace Chess.Engine;

[DebuggerDisplay("{Character}")]
public struct Piece
{
    const string VALID_PIECES = "PNBRQKpnbrqk";

    private readonly byte _store = 0x00;

    /// <summary>
    /// Constructs a blank piece
    /// </summary>
    public Piece() { }

    public Piece(PieceType type, PieceColour colour)
    {
        _store = (byte)((byte)type | (byte)colour);
    }

    /// <summary>
    /// Creates a piece from the FEN diagram character
    /// </summary>
    /// <param name="piece"></param>
    public Piece(char piece)
    {
        if (!VALID_PIECES.Contains(piece))
        {
            return;
        }
        PieceColour colour = char.IsUpper(piece) ? PieceColour.White : PieceColour.Black;
        PieceType type = PieceType.None;

        switch (char.ToLower(piece))
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

        _store = (byte)((byte)type | (byte)colour);
    }

    /// <summary>
    /// The type of the piece, for example, pawn, knight, etc.
    /// </summary>
    public PieceType Type => (PieceType)(_store & 0x0F);

    /// <summary>
    /// The colour of the piece, black or white
    /// </summary>
    public PieceColour Colour => (PieceColour)(_store & 0x10);

    /// <summary>
    /// Returns the character that represents the piece as per a FEN diagram
    /// </summary>
    public char Character
    {
        get
        {
            switch (Type)
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
