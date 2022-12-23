using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Snake
{
    class SnakeAnimal
    {
        //Attribute
        public int Length { get; set; }

        public int[,] Position { get; set; }

        public Board Board { get; set; }


        //Methoden
        public SnakeAnimal(Board myBoard)
        {
            // TODO: Wert muss dynamisch werden
            Length = 3;
            
            Position = StartPosition();

            Board = myBoard;

            //CreateSnake(Position, myBoard);
        }

        private int[,] StartPosition()
        {
            // TODO: Länge dynamisch
            int[,] pos = new int[3, 2] { { 4, 2 }, { 3, 2 }, { 2, 2 } };

            return pos;
        }

        // TODO: Hier liegt ein Fehler vor.
        public void CreateSnake(int[,] position)
        {
            for (int i = 1; i < Length; i++)
            {
                for (int j = 1; j < Length; j++)
                {
                    Board.Control[j, i].Background = Brushes.Red;
                }
            }
        }
    }
}
