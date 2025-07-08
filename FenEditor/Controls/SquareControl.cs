using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Chess.Engine;

namespace FenEditor.Controls;

public class SquareControl : Panel
{
    Avalonia.Svg.Svg _svg;

    //const string ChessSet = "Alpha";
    const string ChessSet = "Merida";

    public static readonly StyledProperty<Piece> PieceProperty =
        AvaloniaProperty.Register<SquareControl, Piece>(nameof(Piece));

    public Piece Piece
    {
        get => GetValue(PieceProperty);
        set => SetValue(PieceProperty, value);
    }

    public SquareControl(int row, int col)
    {
        var isLight = (row + col) % 2 == 0;
        Background = isLight ? Brushes.SteelBlue : Brushes.LightSteelBlue;

        _svg = new Avalonia.Svg.Svg(new System.Uri(@"avares://FenEditor"))
        {
            Stretch = Stretch.Uniform,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(12)
        };
        Children.Add(_svg);

        this.GetObservable(PieceProperty).Subscribe(new AnonymousObserver<Piece>(OnPieceChanged));
    }

    private void OnPieceChanged(Piece newPiece)
    {
        string pieceImage = newPiece.Character switch
        {
            'P' => $"Assets/Pieces/{ChessSet}/wp.svg",
            'p' => $"Assets/Pieces/{ChessSet}/bp.svg",
            'N' => $"Assets/Pieces/{ChessSet}/wn.svg",
            'n' => $"Assets/Pieces/{ChessSet}/bn.svg",
            'B' => $"Assets/Pieces/{ChessSet}/wb.svg",
            'b' => $"Assets/Pieces/{ChessSet}/bb.svg",
            'R' => $"Assets/Pieces/{ChessSet}/wr.svg",
            'r' => $"Assets/Pieces/{ChessSet}/br.svg",
            'Q' => $"Assets/Pieces/{ChessSet}/wq.svg",
            'q' => $"Assets/Pieces/{ChessSet}/bq.svg",
            'K' => $"Assets/Pieces/{ChessSet}/wk.svg",
            'k' => $"Assets/Pieces/{ChessSet}/bk.svg",
            _ => "Assets/Pieces/empty.svg"
        };

        _svg.Path = pieceImage;
    }
}
