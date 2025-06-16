namespace Alteridem.Engine.Test;

[TestFixture]
public class TestPiece
{
    [TestCaseSource(nameof(GetPieceTypes))]
    public void TestWhitePieceType(PieceType type)
    {
        TestPieceType(type, PieceColour.White);
    }

    [TestCaseSource(nameof(GetPieceTypes))]
    public void TestBlackPieceType(PieceType type)
    {
        TestPieceType(type, PieceColour.Black);
    }

    private static void TestPieceType(PieceType type, PieceColour colour)
    {
        var piece = new Piece(type, colour);
        piece.Type.ShouldBe(type);
        piece.Colour.ShouldBe(colour);
    }

    [TestCaseSource(nameof(GetPieceTypeCharacters))]
    public void TestPieceTypeCharacters(char c)
    {
        var piece = new Piece(c);
        piece.Character.ShouldBe(c);
    }

    public static Array GetPieceTypes() =>
       Enum.GetValues(typeof(PieceType));

    public static IEnumerable<char> GetPieceTypeCharacters()
    {
        const string PIECES = "PNBRQKpnbrqk ";
        foreach (char piece in PIECES)
        {
            yield return piece;
        }
    }
}
