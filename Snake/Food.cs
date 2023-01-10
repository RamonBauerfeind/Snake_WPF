using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Snake
{
    class Food
    {
        public int PosX { get; set; }

        public int PosY { get; set; }

        public char Fruit { get; set; } 

        Board Board { get; set; }

        private int positionX;

        private int positionY;

        public Food(Board myboard)
        {
            Board = myboard;

            PosX = PositionX(Board);

            PosY = PositionY(Board);

            Fruit = FruitType();

            CreateFruit();
        }

        private int PositionX(Board board)
        {
            Random randX = new Random();
            positionX = randX.Next(1, board.Columns - 1);
            
            return positionX;
        }

        private int PositionY(Board board) 
        {
            Random randY = new Random();
            positionY = randY.Next(1, board.Rows - 1);

            return positionY;
        }

        // TODO: mehrere Arten von Früchten erstellen
        public char FruitType()
        {
            //a == apple
            return 'a';
        }

        public void CreateFruit()
        {
            switch(Fruit) 
            {
                case 'a':
                    Board.Control[PosY, PosX].Background = Brushes.Violet;
                    break;
                default:
                    Board.Control[PosY, PosX].Background = Brushes.Violet;
                    break;
            }
        }

    }
}
