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
