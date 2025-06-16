using System;

namespace Alteridem.Engine;

// http://chessprogramming.wikispaces.com/Encoding+Moves
[Flags]
public enum MoveFlags : byte
{
    QuietMove = 0x00,
    DoublePawnPush = 0x01,
    KingCastle = 0x02,
    QueenCastle = 0x03,
    Capture = 0x04,
    EnPassantCapture = 0x05,
    Promotion = 0x08,
    KnightPromotion = 0x08,
    BishopPromotion = 0x09,
    RookPromotion = 0x0A,
    QueenPromotion = 0x0B,

    Invalid = 0x80
}

public class Move : IEquatable<Move>
{
    #region Private Members

    private readonly MoveFlags _flags;

    #endregion

    #region Constructor

    /// <summary>
    /// Construct a move
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="flags"> </param>
    public Move(int from, int to, MoveFlags flags = MoveFlags.QuietMove)
    {
        From = @from;
        To = to;
        _flags = flags;
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// The index of the from square
    /// </summary>
    public int From { get; private set; }

    /// <summary>
    /// The index of the too square
    /// </summary>
    public int To { get; private set; }

    /// <summary>
    /// If the last move was a double pawn push, gets the EnPassant square
    /// </summary>
    public int EnPassantTarget
    {
        get { return DoublePawnPush ? To - (To - From) / 2 : -1; }
    }

    /// <summary>
    /// If the piece was promoted, what it was promoted to
    /// </summary>
    public PieceType Promotion
    {
        get
        {
            if ((_flags & MoveFlags.Promotion) != MoveFlags.Promotion)
                return PieceType.None;
            return (PieceType)(((byte)_flags & 0x03) | 0x04);
        }
    }

    /// <summary>
    /// No captures, castles or stuff like that
    /// </summary>
    public bool QuietMove
    {
        get { return _flags == MoveFlags.QuietMove; }
    }

    /// <summary>
    /// True if this move captures a piece
    /// </summary>
    public bool Captures
    {
        get { return (_flags & MoveFlags.Capture) == MoveFlags.Capture; }
    }

    /// <summary>
    /// True if this move is a double pawn push
    /// </summary>
    public bool DoublePawnPush
    {
        get { return !EnPassantCapture && (_flags & MoveFlags.DoublePawnPush) == MoveFlags.DoublePawnPush; }
    }

    /// <summary>
    /// True if this move is a king castle
    /// </summary>
    public bool KingCastle
    {
        get { return (_flags & MoveFlags.KingCastle) == MoveFlags.KingCastle; }
    }

    /// <summary>
    /// True if this move is a queen castle
    /// </summary>
    public bool QueenCastle
    {
        get { return (_flags & MoveFlags.QueenCastle) == MoveFlags.QueenCastle; }
    }

    /// <summary>
    /// True if this move is a castle
    /// </summary>
    public bool Castle
    {
        get { return KingCastle || QueenCastle; }
    }

    /// <summary>
    /// True if this move is an En Passant capture
    /// </summary>
    public bool EnPassantCapture
    {
        get { return (_flags & MoveFlags.EnPassantCapture) == MoveFlags.EnPassantCapture; }
    }

    /// <summary>
    /// True if this move is valid
    /// </summary>
    public bool Valid
    {
        get { return (_flags & MoveFlags.Invalid) != MoveFlags.Invalid; }
    }

    #endregion

    #region Equality members

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <returns>
    /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
    /// </returns>
    /// <param name="other">An object to compare with this object.</param>
    public bool Equals(Move other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return From == other.From && To == other.To;
    }

    /// <summary>
    /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
    /// </summary>
    /// <returns>
    /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
    /// </returns>
    /// <param name="obj">The object to compare with the current object. </param><filterpriority>2</filterpriority>
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Move)obj);
    }

    /// <summary>
    /// Serves as a hash function for a particular type. 
    /// </summary>
    /// <returns>
    /// A hash code for the current <see cref="T:System.Object"/>.
    /// </returns>
    /// <filterpriority>2</filterpriority>
    public override int GetHashCode()
    {
        unchecked
        {
            return (From * 397) ^ To;
        }
    }

    public static bool operator ==(Move left, Move right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Move left, Move right)
    {
        return !Equals(left, right);
    }

    #endregion
}
