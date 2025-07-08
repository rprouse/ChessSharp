using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Chess.Engine;

namespace FenEditor.Controls;

public class SquareControl : Panel
{
    Avalonia.Svg.Svg _svg;

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
            'P' => "Assets/Pieces/Book Diagram/wp.svg",
            'p' => "Assets/Pieces/Book Diagram/bp.svg",
            'N' => "Assets/Pieces/Book Diagram/wn.svg",
            'n' => "Assets/Pieces/Book Diagram/bn.svg",
            'B' => "Assets/Pieces/Book Diagram/wb.svg",
            'b' => "Assets/Pieces/Book Diagram/bb.svg",
            'R' => "Assets/Pieces/Book Diagram/wr.svg",
            'r' => "Assets/Pieces/Book Diagram/br.svg",
            'Q' => "Assets/Pieces/Book Diagram/wq.svg",
            'q' => "Assets/Pieces/Book Diagram/bq.svg",
            'K' => "Assets/Pieces/Book Diagram/wk.svg",
            'k' => "Assets/Pieces/Book Diagram/bk.svg",
            _ => "Assets/Pieces/empty.svg"
        };

        _svg.Path = pieceImage;
    }
}
