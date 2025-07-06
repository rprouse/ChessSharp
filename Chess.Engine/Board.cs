using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Chess.Engine;

[DebuggerDisplay("{FEN}")]
public class Board
{
    /// <summary>
    /// Default constructor for Board. Use BoardFactory to initialize.
    /// </summary>
    internal Board() { }

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
    private readonly Piece[] _board = new Piece[64];

    /// <summary>
    /// Index property that gets or sets the Piece in Board
    /// </summary>
    public Piece this[int i]
    {
        get { return _board[i]; }
        set { _board[i] = value; }
    }

    /// <summary>
    /// The index into the board if a pawn just made a two square move. It is the square behind the pawn. -1 otherwise.
    /// </summary>
    public int EnPassantTarget { get; set; } = -1;

    /// <summary>
    /// Castling availability for white kingside
    /// </summary>
    public bool WhiteKingside { get; set; } = true;

    /// <summary>
    /// Castling availability for white queenside
    /// </summary>
    public bool WhiteQueenside { get; set; } = true;

    /// <summary>
    /// Castling availability for black kingside
    /// </summary>
    public bool BlackKingside { get; set; } = true;

    /// <summary>
    /// Castling availability for black queenside
    /// </summary>
    public bool BlackQueenside { get; set; } = true;

    /// <summary>
    /// Who has the next move?
    /// </summary>
    public PieceColour ActiveColour { get; set; } = PieceColour.White;

    /// <summary>
    /// This is the number of half moves since the last pawn advance or capture.
    /// This is used to determine if a draw can be claimed under the fifty-move rule.
    /// </summary>
    public int HalfMoveClock { get; set; } = 0;

    /// <summary>
    /// The number of the full move. It starts at 1, and is incremented after Black's move.
    /// </summary>
    public int FullMoveNumber { get; set; } = 0;

    /// <summary>
    /// Gets the Forsyth–Edwards Notation (FEN) for this board,
    /// </summary>
    public string FEN => this.ToFEN();

    /// <summary>
    /// A simple text representation of the board
    /// </summary>
    /// <returns></returns>
    public override string ToString() => FEN;

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
            _board[i].Colour == ActiveColour)
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

    private bool IsBlocker(int index) =>
        _board[index].Type != PieceType.None && _board[index].Colour == ActiveColour;

    private bool IsCapture(int index) =>
        _board[index].Type != PieceType.None && _board[index].Colour != ActiveColour;

    private bool IsKingCapture(int index) =>
        IsCapture(index) && _board[index].Type == PieceType.King;

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
            if (EnPassantTarget == capture)
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
            if (EnPassantTarget == capture)
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

    public Move MakeMove(string from, string to) =>
        MakeMove(IndexFromSquare(from), IndexFromSquare(to));

    private Move MakeMove(int from, int to)
    {
        Move move = GetMove(from, to);

        // If move is valid, update the game info
        if (move.Valid)
        {
            // Remove piece from En-Passant capture
            if (move.EnPassantCapture)
            {
                int diff = ActiveColour == PieceColour.White ? -8 : 8;
                _board[move.To + diff] = new Piece();
            }

            EnPassantTarget = move.EnPassantTarget;

            // TODO: Disable move if it results in check
            if (ResultsInCheck(move))
            {
                move.Invalidate();
                return move;
            }

            // TODO: Disable castling if it passes through check

            // TODO: Handle pawn promotion

            // Disable castling flags
            if (_board[move.From].Type == PieceType.King)
            {
                if (ActiveColour == PieceColour.White)
                {
                    WhiteKingside = false;
                    WhiteQueenside = false;
                }
                else
                {
                    BlackKingside = false;
                    BlackQueenside = false;
                }
            }
            else if (_board[move.From].Type == PieceType.Rook)
            {
                if (ActiveColour == PieceColour.White && move.From == 0)
                    WhiteQueenside = false;
                else if (ActiveColour == PieceColour.White && move.From == 7)
                    WhiteKingside = false;
                else if (ActiveColour == PieceColour.Black && move.From == 56)
                    BlackQueenside = false;
                else if (ActiveColour == PieceColour.Black && move.From == 63)
                    BlackKingside = false;
            }

            // Update player, clock, etc
            if (ActiveColour == PieceColour.Black)
            {
                ActiveColour = PieceColour.White;
                FullMoveNumber++;
            }
            else
            {
                ActiveColour = PieceColour.Black;
            }
            // TODO: Correctly increment the halfmove clock
            //if (move.QuietMove)
            //{
            //    HalfMoveClock++;
            //}
            //else
            //{
            //    HalfMoveClock = 0;
            //}
            // Update the board
            _board[move.To] = _board[move.From];
            _board[move.From] = new Piece();

            // TODO: Add to move list
        }
        return move;
    }

    internal bool ResultsInCheck(Move move)
    {
        // This is a stub for now. We will need to implement a method to check if the move results in check.
        // This will involve simulating the move and checking if the king is still safe.
        int kingIndex = FindKing(ActiveColour);
        if (kingIndex == -1)
        {
            // No king found, this is an invalid state
            return true;
        }
        // Temporarily make the move
        Piece tempPiece = _board[move.To];
        _board[move.To] = _board[move.From];
        _board[move.From] = new Piece();

        // Update kingIndex if the moved piece is the king
        if (_board[move.To].Type == PieceType.King)
        {
            kingIndex = move.To;
        }

        bool inCheck = IsInCheck(kingIndex);

        // Undo the move
        _board[move.From] = _board[move.To];
        _board[move.To] = tempPiece;
        return inCheck;
    }

    internal bool IsInCheck(int kingIndex)
    {
        // Check if the king is in check by any opposing piece
        if (kingIndex < 0 || kingIndex >= 64 || _board[kingIndex].Type != PieceType.King)
        {
            return false; // Invalid king index
        }
        PieceColour opponentColour = _board[kingIndex].Colour == PieceColour.White ? PieceColour.Black : PieceColour.White;

        // Check horizontally and vertically for rooks and queens
        for (int i = 1; i < 8; i++)
        {
            // Check right
            if (kingIndex % 8 + i < 8)
            {
                int index = kingIndex + i;
                if (_board[index].Colour == opponentColour && (_board[index].Type == PieceType.Rook || _board[index].Type == PieceType.Queen))
                    return true;
                if (_board[index].Type != PieceType.None) break; // Blocked
            }
            // Check left
            if (kingIndex % 8 - i >= 0)
            {
                int index = kingIndex - i;
                if (_board[index].Colour == opponentColour && (_board[index].Type == PieceType.Rook || _board[index].Type == PieceType.Queen))
                    return true;
                if (_board[index].Type != PieceType.None) break; // Blocked
            }
            // Check down
            if (kingIndex / 8 + i < 8)
            {
                int index = kingIndex + i * 8;
                if (_board[index].Colour == opponentColour && (_board[index].Type == PieceType.Rook || _board[index].Type == PieceType.Queen))
                    return true;
                if (_board[index].Type != PieceType.None) break; // Blocked
            }
            // Check up
            if (kingIndex / 8 - i >= 0)
            {
                int index = kingIndex - i * 8;
                if (_board[index].Colour == opponentColour && (_board[index].Type == PieceType.Rook || _board[index].Type == PieceType.Queen))
                    return true;
                if (_board[index].Type != PieceType.None) break; // Blocked
            }
        }

        // Check diagonally for bishops and queens
        for (int i = 1; i < 8; i++)
        {
            // Check top-right
            if (kingIndex % 8 + i < 8 && kingIndex / 8 - i >= 0)
            {
                int index = kingIndex - i * 7;
                if (_board[index].Colour == opponentColour && (_board[index].Type == PieceType.Bishop || _board[index].Type == PieceType.Queen))
                    return true;
                if (_board[index].Type != PieceType.None) break; // Blocked
            }
            // Check top-left
            if (kingIndex % 8 - i >= 0 && kingIndex / 8 - i >= 0)
            {
                int index = kingIndex - i * 9;
                if (_board[index].Colour == opponentColour && (_board[index].Type == PieceType.Bishop || _board[index].Type == PieceType.Queen))
                    return true;
                if (_board[index].Type != PieceType.None) break; // Blocked
            }
            // Check bottom-right
            if (kingIndex % 8 + i < 8 && kingIndex / 8 + i < 8)
            {
                int index = kingIndex + i * 9;
                if (_board[index].Colour == opponentColour && (_board[index].Type == PieceType.Bishop || _board[index].Type == PieceType.Queen))
                    return true;
                if (_board[index].Type != PieceType.None) break; // Blocked
            }
            // Check bottom-left
            if (kingIndex % 8 - i >= 0 && kingIndex / 8 + i < 8)
            {
                int index = kingIndex + i * 7;
                if (_board[index].Colour == opponentColour && (_board[index].Type == PieceType.Bishop || _board[index].Type == PieceType.Queen))
                    return true;
                if (_board[index].Type != PieceType.None) break; // Blocked
            }
        }

        // Check for knights
        int[] knightMoves = { -17, -15, -10, -6, 6, 10, 15, 17 };
        foreach (int move in knightMoves)
        {
            int index = kingIndex + move;
            if (index >= 0 && index < 64)
            {
                int fileDiff = Math.Abs(File(kingIndex) - File(index));
                int rankDiff = Math.Abs(Rank(kingIndex) - Rank(index));
                // Valid knight moves have file and rank differences of (1,2) or (2,1)
                if ((fileDiff == 1 && rankDiff == 2) || (fileDiff == 2 && rankDiff == 1))
                {
                    if (_board[index].Colour == opponentColour && _board[index].Type == PieceType.Knight)
                    {
                        return true; // Knight attack
                    }
                }
            }
        }

        // Check for pawns
        int[] pawnAttacks;
        if (kingIndex % 8 == 0)
        {
            pawnAttacks = opponentColour == PieceColour.White ? [-7] : [9];
        }
        else if (kingIndex % 8 == 7)
        {
            pawnAttacks = opponentColour == PieceColour.White ? [-9] : [7];
        }
        else
        {
            pawnAttacks = opponentColour == PieceColour.White ? [-9, -7] : [9, 7];
        }
        PieceType pawnType = opponentColour == PieceColour.White ? PieceType.WhitePawn : PieceType.BlackPawn;
        foreach (int attack in pawnAttacks)
        {
            int index = kingIndex + attack;
            if (index >= 0 && index < 64 && _board[index].Type == pawnType)
            {
                return true; // Pawn attack
            }
        }

        return false; // King is safe
    }

    internal int FindKing(PieceColour colour)
    {
        for (int i = 0; i < 64; i++)
        {
            if (_board[i].Type == PieceType.King && _board[i].Colour == colour)
            {
                return i;
            }
        }
        return -1; // King not found
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
