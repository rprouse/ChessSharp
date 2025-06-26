using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Chess.Engine;

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

    const string WHITE = "RNBQKBNRPPPPPPPP";
    const string BLACK = "pppppppprnbqkbnr";

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

    // Index property that gets or sets the Piece in Board
    internal Piece this[int i]
    {
        get { return _board[i]; }
        set { _board[i] = value; }
    }

    internal int EnPassantTarget
    {
        get { return _enPassantTarget; }
        set { _enPassantTarget = value; }
    }

    internal bool WhiteKingside
    {
        get { return _whiteKingside; }
        set { _whiteKingside = value; }
    }

    internal bool WhiteQueenside
    {
        get { return _whiteQueenside; }
        set { _whiteQueenside = value; }
    }

    internal bool BlackKingside
    {
        get { return _blackKingside; }
        set { _blackKingside = value; }
    }

    internal bool BlackQueenside
    {
        get { return _blackQueenside; }
        set { _blackQueenside = value; }
    }

    internal PieceColour ActiveColour
    { 
        get { return _activeColour; }
        set { _activeColour = value; }
    }

    internal int HalfMoveClock
    {
        get { return _halfMoveClock; }
        set { _halfMoveClock = value; }
    }

    internal int FullMoveNumber
    {
        get { return _fullMoveNumber; }
        set { _fullMoveNumber = value; }
    }

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
        this.Initialize(fen);
    }

    internal void InitializeBlankBoard()
    {
        for (int i = 0; i < 64; i++)
        {
            _board[i] = new Piece();
        }
        _fullMoveNumber = 1;
    }

    private void InitializeStandardBoard()
    {
        // Clear all the squares
        InitializeBlankBoard();

        // Setup white
        for (int i = 0; i < 16; i++)
        {
            _board[i] = new Piece(WHITE[i]);
        }

        // Setup black
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
    /// Gets a file (0-7) for an index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public static int File(int index)
    {
        return index % 8;
    }

    /// <summary>
    /// Gets a rank (0-7) for an index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public static int Rank(int index)
    {
        return index / 8;
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
        var file = (char)('a' + File(index));
        var rank = (char)('1' + Rank(index));
        return string.Format("{0}{1}", file, rank);
    }

    public List<Move> MovesFor(string from)
    {
        int i = IndexFromSquare(from);
        return MovesFor(i);
    }

    private List<Move> MovesFor(int i)
    {
        if (i > -1 &&
            _board[i].Type != PieceType.None &&
            _board[i].Colour == _activeColour)
        {
            switch (_board[i].Type)
            {
                case PieceType.BlackPawn:
                    return BlackPawnMoves(i);
                case PieceType.WhitePawn:
                    return WhitePawnMoves(i);
                case PieceType.Knight:
                    return KnightMoves(i);
                case PieceType.Bishop:
                    return BishopMoves(i);
                case PieceType.Rook:
                    return RookMoves(i);
                case PieceType.Queen:
                    return QueenMoves(i);
                case PieceType.King:
                    return KingMoves(i);
            }
        }
        // TODO: Prune out moves that put us in check
        return new List<Move>();
    }

    private bool IsBlocker(int index)
    {
        return _board[index].Type != PieceType.None && _board[index].Colour == _activeColour;
    }

    private bool IsCapture(int index)
    {
        return _board[index].Type != PieceType.None && _board[index].Colour != _activeColour;
    }

    private bool IsKingCapture(int index)
    {
        return IsCapture(index) && _board[index].Type == PieceType.King;
    }

    /// <summary>
    /// Checks if a move is blocked, is a capture, or just a regular move. Returns true if blocked or a capture.
    /// </summary>
    /// <param name="to">The square we are moving to</param>
    /// <param name="moves">The move list to add to</param>
    /// <returns>Returns true if this move will stop further moves of long range pieces.</returns>
    private bool CheckMove(int from, int to, List<Move> moves)
    {
        if (IsBlocker(to) || IsKingCapture(to))
            return true;

        if (IsCapture(to))
        {
            moves.Add(new Move(from, to, MoveFlags.Capture));
            return true;
        }
        moves.Add(new Move(from, to));
        return false;
    }

    /// <summary>
    /// Generates valid moves for long range pieces
    /// </summary>
    /// <param name="from">The starting square</param>
    /// <param name="diff">The direction to travel</param>
    /// <param name="moves">The move list to add to</param>
    /// <param name="kingMove">If true, will only go one square on the given vector</param>
    private void LongRangeMoves(int from, int diff, List<Move> moves, bool kingMove = false)
    {
        int i = from;
        while (true)
        {
            if (Rank(i) == 7 || File(i) == 7)
                break;

            i += diff;
            if (i > 63)
                break;

            if (CheckMove(from, i, moves))
                break;

            if (kingMove)
                break;
        }

        i = from;
        while (true)
        {
            if (Rank(i) == 0 || File(i) == 0)
                break;

            i -= diff;

            if (i < 0)
                break;

            if (CheckMove(from, i, moves))
                break;

            if (kingMove)
                break;
        }
    }

    private List<Move> BlackPawnMoves(int from)
    {
        var moves = new List<Move>();

        // One square
        if (from > 7 && _board[from - 8].Type == PieceType.None)
            moves.Add(new Move(from, from - 8));

        // Two square push
        if (from >= 48 && from <= 55 &&
            _board[from - 8].Type == PieceType.None &&
            _board[from - 16].Type == PieceType.None)
        {
            moves.Add(new Move(from, from - 16, MoveFlags.DoublePawnPush));
        }

        var captures = new[] { from - 7, from - 9 };

        foreach (int capture in captures)
        {
            // En-Passant Capture
            if (_enPassantTarget == capture)
            {
                moves.Add(new Move(from, capture, MoveFlags.EnPassantCapture));
            }
            else if (IsCapture(capture))
            {
                moves.Add(new Move(from, capture, MoveFlags.Capture));
            }
        }
        return moves;
    }

    private List<Move> WhitePawnMoves(int from)
    {
        var moves = new List<Move>();

        // One square
        if (from < 55 && _board[from + 8].Type == PieceType.None)
            moves.Add(new Move(from, from + 8));

        // Two square push
        if (from >= 8 && from <= 15 &&
            _board[from + 8].Type == PieceType.None &&
            _board[from + 16].Type == PieceType.None)
        {
            moves.Add(new Move(from, from + 16, MoveFlags.DoublePawnPush));
        }

        var captures = new[] { from + 7, from + 9 };

        foreach (int capture in captures)
        {
            // En-Passant Capture
            if (_enPassantTarget == capture)
            {
                moves.Add(new Move(from, capture, MoveFlags.EnPassantCapture));
            }
            else if (IsCapture(capture))
            {
                moves.Add(new Move(from, capture, MoveFlags.Capture));
            }
        }
        return moves;
    }

    private List<Move> KnightMoves(int from)
    {
        var moves = new List<Move>();
        int r = Rank(from);
        int f = File(from);

        if (r > 0 && f < 6) KnightMove(from, -6, moves);
        if (r > 0 && f > 1) KnightMove(from, -10, moves);
        if (r > 1 && f < 7) KnightMove(from, -15, moves);
        if (r > 1 && f > 0) KnightMove(from, -17, moves);

        if (r < 7 && f > 1) KnightMove(from, +6, moves);
        if (r < 7 && f < 6) KnightMove(from, +10, moves);
        if (r < 6 && f > 0) KnightMove(from, +15, moves);
        if (r < 6 && f < 7) KnightMove(from, +17, moves);

        return moves;
    }

    private void KnightMove(int from, int diff, List<Move> moves)
    {
        CheckMove(from, from + diff, moves);
    }

    private List<Move> BishopMoves(int from)
    {
        var moves = new List<Move>();

        LongRangeMoves(from, 9, moves);
        LongRangeMoves(from, 7, moves);

        return moves;
    }

    private List<Move> RookMoves(int from)
    {
        var moves = new List<Move>();

        LongRangeMoves(from, 8, moves);
        LongRangeMoves(from, 1, moves);

        return moves;
    }

    private List<Move> QueenMoves(int from)
    {
        var moves = new List<Move>();

        LongRangeMoves(from, 9, moves);
        LongRangeMoves(from, 7, moves);
        LongRangeMoves(from, 8, moves);
        LongRangeMoves(from, 1, moves);

        return moves;
    }

    private List<Move> KingMoves(int from)
    {
        var moves = new List<Move>();

        LongRangeMoves(from, 9, moves, true);
        LongRangeMoves(from, 7, moves, true);
        LongRangeMoves(from, 8, moves, true);
        LongRangeMoves(from, 1, moves, true);

        return moves;
    }

    public Move MakeMove(string from, string to)
    {
        return MakeMove(IndexFromSquare(from), IndexFromSquare(to));
    }

    private Move MakeMove(int from, int to)
    {
        Move move = GetMove(from, to);

        // If move is valid, update the game info
        if (move.Valid)
        {
            // Remove piece from En-Passant capture
            if (move.EnPassantCapture)
            {
                int diff = _activeColour == PieceColour.White ? -8 : 8;
                _board[move.To + diff] = new Piece();
            }

            _enPassantTarget = move.EnPassantTarget;

            // TODO: Disable move if it results in check

            // TODO: Disable castling if it passes through check

            // TODO: Handle pawn promotion

            // Disable castling flags
            if (_board[move.From].Type == PieceType.King)
            {
                if (_activeColour == PieceColour.White)
                {
                    _whiteKingside = false;
                    _whiteQueenside = false;
                }
                else
                {
                    _blackKingside = false;
                    _blackQueenside = false;
                }
            }
            else if (_board[move.From].Type == PieceType.Rook)
            {
                if (_activeColour == PieceColour.White && move.From == 0)
                    _whiteQueenside = false;
                else if (_activeColour == PieceColour.White && move.From == 7)
                    _whiteKingside = false;
                else if (_activeColour == PieceColour.Black && move.From == 56)
                    _blackQueenside = false;
                else if (_activeColour == PieceColour.Black && move.From == 63)
                    _blackKingside = false;
            }

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
            _board[move.From] = new Piece();

            // TODO: Add to move list
        }
        return move;
    }

    /// <summary>
    /// Given the current board and a move to make, constructs a Move object
    /// with the validity set correctly.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    private Move GetMove(int from, int to)
    {
        var move = new Move(from, to, MoveFlags.Invalid);
        var moves = MovesFor(from);
        foreach (Move candidate in moves)
        {
            if (move == candidate && candidate.Valid)
            {
                return candidate;
            }
        }
        return move;
    }
}
