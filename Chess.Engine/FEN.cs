using System;
using System.Globalization;

namespace Chess.Engine;

public static class FEN
{
    /// <summary>
    /// Construct from Forsyth–Edwards Notation (FEN), 
    /// http://en.wikipedia.org/wiki/Forsyth%E2%80%93Edwards_Notation
    /// </summary>
    /// <param name="fen"></param>
    public static void Initialize(this Board board, string fen)
    {
        var parts = fen.Split([' '], 6);
        if (parts.Length != 6)
        {
            throw new ArgumentException("fen is in an incorrect format. It does not have 6 parts.");
        }

        var ranks = parts[0].Split(['/']);
        if (ranks.Length != 8)
        {
            throw new ArgumentException("fen is in an incorrect format. The first part does not have 8 ranks.");
        }

        // Setup the board
        board.InitializeBlankBoard();
        int i = 0;
        // FEN starts with rank 8 and ends with rank 1
        for (int r = 7; r >= 0; r--)
        {
            foreach (char p in ranks[r])
            {
                if (char.IsDigit(p))
                {
                    // Blank squares to skip
                    int skip = p - '0';
                    if (skip > 8)
                    {
                        throw new ArgumentException("fen is in an incorrect format.");
                    }
                    i += skip;
                }
                else
                {
                    if (i > 63)
                    {
                        throw new ArgumentException("fen is in an incorrect format.");
                    }
                    board[i++] = new Piece(p);
                }
            }
        }

        if (i != 64)
        {
            throw new ArgumentException("fen is in an incorrect format. The board does not have 64 squares.");
        }

        // Active Colour
        board.ActiveColour = parts[1].ToLowerInvariant() == "w" ? PieceColour.White : PieceColour.Black;

        // Castling availability
        board.WhiteKingside = false;
        board.WhiteQueenside = false;
        board.BlackKingside = false;
        board.BlackQueenside = false;
        foreach (char c in parts[2])
        {
            switch (c)
            {
                case 'K':
                    board.WhiteKingside = true;
                    break;
                case 'Q':
                    board.WhiteQueenside = true;
                    break;
                case 'k':
                    board.BlackKingside = true;
                    break;
                case 'q':
                    board.BlackQueenside = true;
                    break;
            }
        }

        // En passant target square
        board.EnPassantTarget = Board.IndexFromSquare(parts[3]);

        // Halfmove clock
        int halfMoveClock;
        if (!Int32.TryParse(parts[4], NumberStyles.Integer, CultureInfo.InvariantCulture, out halfMoveClock))
        {
            throw new ArgumentException("fen is in an incorrect format. Halfmove clock is not an int.");
        }
        board.HalfMoveClock = halfMoveClock;

        // Fullmove
        int fullMoveNumber;
        if (!Int32.TryParse(parts[5], NumberStyles.Integer, CultureInfo.InvariantCulture, out fullMoveNumber))
        {
            throw new ArgumentException("fen is in an incorrect format. Fullmove number is not an int.");
        }

        if (fullMoveNumber < 1)
        {
            throw new ArgumentException("fen is in an incorrect format. Fullmove number must be greater than 0.");
        }
        board.FullMoveNumber = fullMoveNumber;
    }
}
