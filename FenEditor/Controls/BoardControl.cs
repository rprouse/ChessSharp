using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace FenEditor.Controls;

public class BoardControl : Grid
{
    public BoardControl()
    {
        BuildGrid();
        SizeChanged += OnSizeChanged;
    }

    private void BuildGrid()
    {
        RowDefinitions.Clear();
        ColumnDefinitions.Clear();

        // Create the grid structure for the chessboard
        // Rank labels on the left and file labels on the bottom
        ColumnDefinitions.Add(new ColumnDefinition(GridLength.Auto));
        RowDefinitions.Add(new RowDefinition(GridLength.Auto));
        for (int i = 0; i < 8; i++)
        {
            RowDefinitions.Add(new RowDefinition(GridLength.Star));
            ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));
        }
        ColumnDefinitions.Add(new ColumnDefinition(GridLength.Auto));
        RowDefinitions.Add(new RowDefinition(GridLength.Auto));

        for (int row = 1; row <= 8; row++)
        {
            // Add rank labels (1-8) on the left and right sides
            AddRankLabel(row, 0);
            AddRankLabel(row, 9);

            // Add file labels (a-h) at the top and bottom
            AddColumnLabel(0, row);
            AddColumnLabel(9, row);

            for (int col = 1; col <= 8; col++)
            {
                var square = new SquareControl(row - 1, col - 1);
                SetRow(square, row);
                SetColumn(square, col);
                Children.Add(square);
            }
        }
    }

    private void AddColumnLabel(int row, int col)
    {
        CreateLabel(((char)('A' + col - 1)).ToString(), row, col);
    }

    private void AddRankLabel(int row, int col)
    {
        CreateLabel((8 - row + 1).ToString(), row, col);
    }

    private void CreateLabel(string label, int row, int col)
    {
        var textBlock = new TextBlock
        {
            Text = label,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(3, 3, 3, 3),
            FontWeight = FontWeight.Bold
        };
        SetRow(textBlock, row);
        SetColumn(textBlock, col);
        Children.Add(textBlock);
    }

    private void OnSizeChanged(object? sender, SizeChangedEventArgs e)
    {
        double size = Math.Min(e.NewSize.Width, e.NewSize.Height);
        Width = size;
        Height = size;
    }
}
