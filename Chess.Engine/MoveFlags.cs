using System;

namespace Chess.Engine;

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
