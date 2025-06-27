using System;

namespace Chess.Engine
{
    /// <summary>
    /// Factory class for creating and initializing Board instances.
    /// </summary>
    public static class BoardFactory
    {
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

        public static void InitializeBlankBoard(Board board)
        {
            for (int i = 0; i < 64; i++)
            {
                board[i] = new Piece();
            }
            board.FullMoveNumber = 1;
        }

        public static void InitializeStandardBoard(Board board)
        {
            InitializeBlankBoard(board);
            const string WHITE = "RNBQKBNRPPPPPPPP";
            const string BLACK = "pppppppprnbqkbnr";
            for (int i = 0; i < 16; i++)
            {
                board[i] = new Piece(WHITE[i]);
            }
            for (int i = 0; i < 16; i++)
            {
                board[i + 48] = new Piece(BLACK[i]);
            }
        }

        public static void InitializeChess960Board(Board board)
        {
            // TODO: Setup a Chess960 board
            throw new NotImplementedException();
        }
    }
}
