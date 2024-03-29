﻿using System.Windows;
using System.Windows.Controls;

namespace Snake
{
    class Board
    {
        //Attribute
        public int Rows { get; set; }
        public int Columns { get; set; }
        public Label[,] Control { get; set; }

        //Methoden
        public Board(Grid Snake)
        {
            Rows = Snake.RowDefinitions.Count;
            Columns= Snake.ColumnDefinitions.Count;
            Control = new Label[Rows, Columns];

            CreateBoard(Snake);
        }

        public void CreateBoard(Grid Snake)

        {
            for(int i = 0; i < Rows - 1; i++)
            {
                for (int j = 0; j < Columns - 1; j++)
                {
                    Control[i, j] = new Label();
                    Control[i, j].BorderThickness = new Thickness(0.5);
                    Grid.SetRow(Control[i, j], i);
                    Grid.SetColumn(Control[i, j], j);
                    Snake.Children.Add(Control[i, j]);
                }
            }
        }
    }
}
