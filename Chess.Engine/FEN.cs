using System;
using System.Globalization;
using System.Text;

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

        // Setup the fen
        BoardFactory.InitializeBlankBoard(board);
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

    /// <summary>
    /// Gets the Forsyth–Edwards Notation (FEN) for this fen,
    /// http://en.wikipedia.org/wiki/Forsyth%E2%80%93Edwards_Notation
    /// </summary>
    public static string ToFEN(this Board board)
    {

        // Board setup
        var fen = new StringBuilder(80);
        for (int rank = 7; rank >= 0; rank--)
        {
            int skip = 0;
            for (int file = 0; file < 8; file++)
            {
                int i = Board.Index(rank, file);
                char piece = board[i].Character;
                if (piece != ' ')
                {
                    if (skip > 0)
                    {
                        fen.Append(skip);
                        skip = 0;
                    }
                    fen.Append(piece);
                }
                else
                {
                    skip++;
                }
            }
            if (skip > 0)
            {
                fen.Append(skip);
            }
            if (rank > 0)
            {
                fen.Append('/');
            }
        }

        // Active Colour
        char activeColour = board.ActiveColour == PieceColour.White ? 'w' : 'b';

        // Castling availability
        var castling = new StringBuilder(4);
        if (!board.WhiteKingside && !board.BlackKingside & !board.WhiteQueenside && !board.BlackQueenside)
        {
            castling.Append('-');
        }
        if (board.WhiteKingside)
            castling.Append('K');
        if (board.WhiteQueenside)
            castling.Append('Q');
        if (board.BlackKingside)
            castling.Append('k');
        if (board.BlackQueenside)
            castling.Append('q');

        // En passant target square
        string target = Board.SquareFromIndex(board.EnPassantTarget);

        // Halfmove clock
        // Fullmove number
        return string.Format("{0} {1} {2} {3} {4} {5}", fen, activeColour, castling, target, board.HalfMoveClock, board.FullMoveNumber);
    }
}
