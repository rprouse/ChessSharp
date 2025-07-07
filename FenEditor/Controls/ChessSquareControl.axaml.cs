using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace FenEditor.Controls;

public partial class ChessSquareControl : UserControl
{
    public ChessSquareControl()
    {
        InitializeComponent();
        SetupBoard();
    }

    private void SetupBoard()
    {
        const int size = 8;

        // Add squares
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                var square = new ChessSquareControl
                {
                    Background = (row + col) % 2 == 0 ? Brushes.White : Brushes.Gray,
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(0.5)
                };

                Grid.SetRow(square, row);
                Grid.SetColumn(square, col);
                //BoardGrid.Children.Add(square);
            }
        }
    }
}
