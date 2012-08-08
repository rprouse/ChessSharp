using System;

namespace Alteridem.Engine
{
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

    public class Move
    {
        private readonly MoveFlags _flags;

        /// <summary>
        /// The index of the from square
        /// </summary>
        public int From { get; private set; }

        /// <summary>
        /// The index of the too square
        /// </summary>
        public int To { get; private set; }

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
            get { return (_flags & MoveFlags.DoublePawnPush) == MoveFlags.DoublePawnPush; }
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
        /// True if this move is valid
        /// </summary>
        public bool Valid
        {
            get { return (_flags & MoveFlags.Invalid) != MoveFlags.Invalid; }
        }

        /// <summary>
        /// Construct a move
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="flags"> </param>
        public Move(int from, int to, MoveFlags flags = MoveFlags.QuietMove)
        {
            From = from;
            To = to;
            _flags = flags;
        }
    }
}
