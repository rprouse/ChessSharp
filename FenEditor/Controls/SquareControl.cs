using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace FenEditor.Controls;

public class SquareControl : Panel
{
    public SquareControl(int row, int col)
    {
        var isLight = (row + col) % 2 == 0;
        Background = isLight ? Brushes.SteelBlue : Brushes.LightSteelBlue;

        var image = new Avalonia.Svg.Svg(new System.Uri(@"avares://FenEditor"))
        {
            Path = "Assets/Pieces/Book Diagram/wq.svg",
            Stretch = Stretch.Uniform,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(12)
        };
        Children.Add(image);
    }
}
