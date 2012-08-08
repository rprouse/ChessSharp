using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Alteridem.Engine
{
    /// <summary>
    /// How should we initialize the board
    /// </summary>
    public enum BoardInitialization
    {
        /// <summary>
        /// A blank board with no pieces on it
        /// </summary>
        Blank,
        /// <summary>
        /// A standard chess setup
        /// </summary>
        Standard,
        /// <summary>
        /// A Chess960 (Fischer Random Chess) setup
        /// </summary>
        Chess960
    }

    [DebuggerDisplay("{FEN}")]
    public class Board
    {
        // An 8*8 Board 
        //
        //      a  b  c  d  e  f  g  h
        //    -------------------------
        // 8 | 56 57 58 59 60 61 62 63 | 8
        // 7 | 48 49 50 51 52 53 54 55 | 7
        // 6 | 40 41 42 43 44 45 46 47 | 6
        // 5 | 32 33 34 35 36 37 38 39 | 5
        // 4 | 24 25 26 27 28 29 30 31 | 4
        // 3 | 16 17 18 19 20 21 22 23 | 3
        // 2 | 08 09 10 11 12 13 14 15 | 2
        // 1 | 00 01 02 03 04 05 06 07 | 1
        //    -------------------------
        //      a  b  c  d  e  f  g  h
        #region Private Members

        private readonly Piece[] _board = new Piece[64];

        // The index into the board if a pawn just made a two square move. It is the square behind the pawn. -1 otherwise.
        private int _enPassantTarget = -1;

        // Castling Availability
        private bool _whiteKingside = true;
        private bool _whiteQueenside = true;
        private bool _blackKingside = true;
        private bool _blackQueenside = true;

        // Who has the next move?
        private PieceColour _activeColour = PieceColour.White;

        // This is the number of halfmoves since the last pawn advance or capture. This is used to determine if a draw can be claimed under the fifty-move rule.
        private int _halfMoveClock = 0;

        // The number of the full move. It starts at 1, and is incremented after Black's move.
        private int _fullMoveNumber = 0;

        #endregion

        #region Construction

        /// <summary>
        /// Constructs a board based on the given setup
        /// </summary>
        public Board(BoardInitialization board)
        {
            switch (board)
            {
                case BoardInitialization.Blank:
                    InitializeBlankBoard();
                    break;
                case BoardInitialization.Standard:
                    InitializeStandardBoard();
                    break;
                case BoardInitialization.Chess960:
                    InitializeChess960Board();
                    break;
            }
        }

        /// <summary>
        /// Construct from Forsyth–Edwards Notation (FEN), 
        /// http://en.wikipedia.org/wiki/Forsyth%E2%80%93Edwards_Notation
        /// </summary>
        /// <param name="fen"></param>
        public Board(string fen)
        {
            var parts = fen.Split(new[] { ' ' }, 6);
            if (parts.Length != 6)
            {
                throw new ArgumentException("fen is in an incorrect format. It does not have 6 parts.");
            }

            var ranks = parts[0].Split(new[] { '/' });
            if (ranks.Length != 8)
            {
                throw new ArgumentException("fen is in an incorrect format. The first part does not have 8 ranks.");
            }

            // Setup the board
            InitializeBlankBoard();
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
                        _board[i++] = new Piece(p);
                    }
                }
            }

            // Active Colour
            _activeColour = parts[1].ToLowerInvariant() == "w" ? PieceColour.White : PieceColour.Black;

            // Castling availability
            _whiteKingside = false;
            _whiteQueenside = false;
            _blackKingside = false;
            _blackQueenside = false;
            foreach (char c in parts[2])
            {
                switch (c)
                {
                    case 'K':
                        _whiteKingside = true;
                        break;
                    case 'Q':
                        _whiteQueenside = true;
                        break;
                    case 'k':
                        _blackKingside = true;
                        break;
                    case 'q':
                        _blackQueenside = true;
                        break;
                }
            }

            // En passant target square
            _enPassantTarget = IndexFromSquare(parts[3]);

            // Halfmove clock
            if (!Int32.TryParse(parts[4], NumberStyles.Integer, CultureInfo.InvariantCulture, out _halfMoveClock))
            {
                throw new ArgumentException("fen is in an incorrect format. Halfmove clock is not an int.");
            }

            // Fullmove number
            if (!Int32.TryParse(parts[5], NumberStyles.Integer, CultureInfo.InvariantCulture, out _fullMoveNumber))
            {
                throw new ArgumentException("fen is in an incorrect format. Fullmove number is not an int.");
            }
        }

        private void InitializeBlankBoard()
        {
            for (int i = 0; i < 64; i++)
            {
                _board[i] = new Piece(0x0);
            }
        }

        private void InitializeStandardBoard()
        {
            // Clear all the squares
            InitializeBlankBoard();

            // Setup white
            const string WHITE = "RNBQKBNRPPPPPPPP";
            for (int i = 0; i < 16; i++)
            {
                _board[i] = new Piece(WHITE[i]);
            }

            // Setup black
            const string BLACK = "pppppppprnbqkbnr";
            for (int i = 0; i < 16; i++)
            {
                _board[i + 48] = new Piece(BLACK[i]);
            }
        }

        private void InitializeChess960Board()
        {
            // TODO: Setup a Chess960 board
            throw new System.NotImplementedException();
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Gets the Forsyth–Edwards Notation (FEN) for this board, 
        /// http://en.wikipedia.org/wiki/Forsyth%E2%80%93Edwards_Notation
        /// </summary>
        public string FEN
        {
            get
            {
                // Board setup
                var board = new StringBuilder(80);
                for (int rank = 7; rank >= 0; rank--)
                {
                    int skip = 0;
                    for (int file = 0; file < 8; file++)
                    {
                        int i = Index(rank, file);
                        char piece = _board[i].Character;
                        if (piece != ' ')
                        {
                            if (skip > 0)
                            {
                                board.Append(skip);
                                skip = 0;
                            }
                            board.Append(piece);
                        }
                        else
                        {
                            skip++;
                        }
                    }
                    if (skip > 0)
                    {
                        board.Append(skip);
                    }
                    if (rank > 0)
                    {
                        board.Append('/');
                    }
                }

                // Active Colour
                char activeColour = _activeColour == PieceColour.White ? 'w' : 'b';

                // Castling availability
                var castling = new StringBuilder(4);
                if (!_whiteKingside && !_blackKingside & !_whiteQueenside && !_blackQueenside)
                {
                    castling.Append('-');
                }
                if (_whiteKingside)
                    castling.Append('K');
                if (_whiteQueenside)
                    castling.Append('Q');
                if (_blackKingside)
                    castling.Append('k');
                if (_blackQueenside)
                    castling.Append('q');

                // En passant target square
                string target = SquareFromIndex(_enPassantTarget);

                // Halfmove clock

                // Fullmove number

                return string.Format("{0} {1} {2} {3} {4} {5}", board, activeColour, castling, target, _halfMoveClock, _fullMoveNumber);
            }
        }

        /// <summary>
        /// A simple text representation of the board
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return FEN;
        }

        /// <summary>
        /// Gets an index into the board array for a given square designation.
        /// </summary>
        /// <param name="square">The square designation, ex</param>
        /// <returns></returns>
        public static int IndexFromSquare(string square)
        {
            if (string.IsNullOrWhiteSpace(square) || square.Length != 2)
            {
                return -1;
            }
            square = square.ToLowerInvariant();

            if (square[0] < 'a' || square[0] > 'h' ||
                 square[1] < '1' || square[1] > '8')
            {
                return -1;
            }

            // Zero based
            int file = square[0] - 'a';
            int rank = square[1] - '1';

            return Index(rank, file);
        }

        /// <summary>
        /// Gets an index into the board from a rank and file
        /// </summary>
        /// <param name="rank">0 to 7</param>
        /// <param name="file">0 to 7</param>
        /// <returns></returns>
        public static int Index(int rank, int file)
        {
            if (rank < 0 || rank > 7 || file < 0 || file > 7)
                return -1;

            return rank * 8 + file;
        }

        /// <summary>
        /// Given an index into the board, returns a square designation
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string SquareFromIndex(int index)
        {
            if (index < 0 || index > 63)
            {
                return "-";
            }
            var file = (char)('a' + index % 8);
            var rank = (char)('1' + index / 8);
            return string.Format("{0}{1}", file, rank);
        }

        #endregion

        public Move MakeMove( string from, string to )
        {
            return MakeMove(IndexFromSquare(from), IndexFromSquare(to));
        }

        private Move MakeMove(int from, int to)
        {
            // Are the squares valid?
            if (from < 0 || from > 63 || to < 0 || to > 63)
                return new Move(from, to, MoveFlags.Invalid);

            if (_board[from].Type == PieceType.None)
                return new Move(from, to, MoveFlags.Invalid);

            // Is it that players turn?
            if (_board[from].Colour != _activeColour)
                return new Move(from, to, MoveFlags.Invalid);

            // Are we capturing our own piece?
            if (_board[to].Type != PieceType.None &&
                 _board[to].Colour == _activeColour)
                return new Move(from, to, MoveFlags.Invalid);

            Move move;
            if (_board[from].Type != PieceType.BlackPawn && _board[from].Type != PieceType.WhitePawn)
            {
                _enPassantTarget = -1;
            }
            switch (_board[from].Type)
            {
                case PieceType.BlackPawn:
                    move = BlackPawnMove(from, to);
                    break;
                case PieceType.WhitePawn:
                    move = WhitePawnMove(from, to);
                    break;
                case PieceType.Knight:
                    move = KnightMove(from, to);
                    break;
                case PieceType.Bishop:
                    move = BishopMove(from, to);
                    break;
                case PieceType.Queen:
                    move = QueenMove(from, to);
                    break;
                case PieceType.King:
                    move = KingMove(from, to);
                    break;
                default:
                    // There isn't a piece on the from square
                    return new Move(from, to, MoveFlags.Invalid);
            }

            // If move is valid, update the game info
            if (move.Valid)
            {
                // Update player, clock, etc
                if (_activeColour == PieceColour.Black)
                {
                    _activeColour = PieceColour.White;
                    _fullMoveNumber++;
                }
                else
                {
                    _activeColour = PieceColour.Black;
                }
                // TODO: Correctly increment the halfmove clock
                //if (move.QuietMove)
                //{
                //    _halfMoveClock++;
                //}
                //else
                //{
                //    _halfMoveClock = 0;
                //}
                // Update the board
                _board[move.To] = _board[move.From];
                _board[move.From] = new Piece(0x0);

                // TODO: Handle castling

                // TODO: Add to move list
            }
            return move;
        }

        private Move BlackPawnMove(int from, int to)
        {
            int diff = from - to;

            // Single row move
            if (diff == 8)
            {
                // Check for block
                if (_board[to].Type != PieceType.None)
                    return new Move(from, to, MoveFlags.Invalid);

                _enPassantTarget = -1;
                return new Move(from, to);
            }

            // First double row move
            if (diff == 16)
            {
                if (from < 48 || from > 55 ||
                     _board[to].Type != PieceType.None ||
                     _board[from - 8].Type != PieceType.None)
                    return new Move(from, to, MoveFlags.Invalid);

                _enPassantTarget = from - 8;
                return new Move(from, to, MoveFlags.DoublePawnPush);
            }

            // Capture
            if (diff == 9 || diff == 7)
            {
                // En Passant Capture
                if (to == _enPassantTarget)
                {
                    // Remove captured piece, normally, this is done in the calling function, but this is a bit special
                    _board[_enPassantTarget + 8] = new Piece(0x0);
                    _enPassantTarget = -1;
                    return new Move(from, to, MoveFlags.EnPassantCapture);
                }

                // Regular capture
                if (_board[to].Type != PieceType.None)
                {
                    _enPassantTarget = -1;
                    return new Move(from, to, MoveFlags.Capture);
                }
            }
            return new Move(from, to, MoveFlags.Invalid);
        }

        private Move WhitePawnMove(int from, int to)
        {
            int diff = to - from;

            // Single row move
            if (diff == 8)
            {
                // Check for block
                if (_board[to].Type != PieceType.None)
                    return new Move(from, to, MoveFlags.Invalid);

                _enPassantTarget = -1;
                return new Move(from, to);
            }

            // First double row move
            if (diff == 16)
            {
                if (from < 08 || from > 15 ||
                     _board[to].Type != PieceType.None ||
                     _board[from + 8].Type != PieceType.None)
                    return new Move(from, to, MoveFlags.Invalid);

                _enPassantTarget = from + 8;
                return new Move(from, to, MoveFlags.DoublePawnPush);
            }

            // Capture
            if (diff == 9 || diff == 7)
            {
                // En Passant Capture
                if (to == _enPassantTarget)
                {
                    // Remove captured piece, normally, this is done in the calling function, but this is a bit special
                    _board[_enPassantTarget-8] = new Piece(0x0);
                    _enPassantTarget = -1;
                    return new Move(from, to, MoveFlags.EnPassantCapture);
                }

                // Regular capture
                if (_board[to].Type != PieceType.None)
                {
                    _enPassantTarget = -1;
                    return new Move(from, to, MoveFlags.Capture);
                }
            }
            return new Move(from, to, MoveFlags.Invalid);
        }

        private Move KnightMove(int from, int to)
        {
            // TODO: Implement move
            return new Move(from, to);
        }

        private Move BishopMove(int from, int to)
        {
            // TODO: Implement move
            return new Move(from, to);
        }

        private Move QueenMove(int from, int to)
        {
            // TODO: Implement move
            return new Move(from, to);
        }

        private Move KingMove(int from, int to)
        {
            // TODO: Implement move
            return new Move(from, to);
        }
    }
}
