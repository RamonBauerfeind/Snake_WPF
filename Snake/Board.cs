﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
            for(int i = 1; i < Rows; i++)
            {
                for (int j = 1; j < Columns; j++)
                {
                    Control[i, j] = new Label();
                    Control[i, j].BorderThickness = new Thickness(1);
                    Grid.SetRow(Control[i, j], i);
                    Grid.SetColumn(Control[i, j], j);
                    Snake.Children.Add(Control[i, j]);
                }
            }
        }
    }
}
