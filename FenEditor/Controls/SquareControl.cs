using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace FenEditor.Controls;

public class SquareControl : UserControl
{
    public SquareControl(int row, int col)
    {
        var isLight = (row + col) % 2 == 0;

        var border = new Border
        {
            Background = isLight ? Brushes.SteelBlue : Brushes.LightSteelBlue,
            BorderBrush = Brushes.Black,
            BorderThickness = new Thickness(0.5)
        };
        Content = border;
    }
}
