using System;

namespace Chess.Engine;

/// <summary>
/// Factory class for creating and initializing Board instances.
/// </summary>
public static class BoardFactory
{
    const string WHITE = "RNBQKBNRPPPPPPPP";
    const string BLACK = "pppppppprnbqkbnr";

    /// <summary>
    /// Creates a Board with the specified initialization type.
    /// </summary>
    public static Board Create(BoardInitialization boardInitialization)
    {
        var board = new Board();
        switch (boardInitialization)
        {
            case BoardInitialization.Blank:
                InitializeBlankBoard(board);
                break;
            case BoardInitialization.Standard:
                InitializeStandardBoard(board);
                break;
            case BoardInitialization.Chess960:
                InitializeChess960Board(board);
                break;
        }
        return board;
    }

    /// <summary>
    /// Construct from Forsyth–Edwards Notation (FEN),
    /// http://en.wikipedia.org/wiki/Forsyth%E2%80%93Edwards_Notation
    /// </summary>
    /// <param name="fen"></param>
    public static Board Create(string fen)
    {
        var board = new Board();
        board.Initialize(fen);
        return board;
    }

    internal static void InitializeBlankBoard(this Board board)
    {
        for (int i = 0; i < 64; i++)
        {
            board[i] = new Piece();
        }
        board.FullMoveNumber = 1;
    }

    internal static void InitializeStandardBoard(this Board board)
    {
        InitializeBlankBoard(board);
        for (int i = 0; i < 16; i++)
        {
            board[i] = new Piece(WHITE[i]);
        }
        for (int i = 0; i < 16; i++)
        {
            board[i + 48] = new Piece(BLACK[i]);
        }
    }

    internal static void InitializeChess960Board(this Board board)
    {
        // TODO: Setup a Chess960 board
        throw new NotImplementedException();
    }
}
