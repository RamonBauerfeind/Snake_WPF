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

        public int[]? PositionX { get; set; }

        public int[]? PositionY { get; set; }

        public Board Board { get; set; }


        //Methoden
        public SnakeAnimal(Board myBoard)
        {
            Board = myBoard;

            StartPosition();

            // TODO: möglichen Fehler beheben
            Length = PositionX.Length;
        }

        public void StartPosition()
        {
            int[] posX = new int[] { 2, 2, 2 };
            int[] posY = new int[] { 2, 3, 4 };

            PositionX = posX;
            PositionY = posY;

            CreateSnake(PositionX, PositionY);
        }

        public void CreateSnake(int[] positionX, int[] positionY)
        {
            for (int i = 0; i < positionX.Length; i++)
            {
                if (i == positionX.Length - 1)
                {
                    Board.Control[positionY[i], positionX[i]].Background = Brushes.Green;
                }
                else
                {
                    Board.Control[positionY[i], positionX[i]].Background = Brushes.Red;
                }
            }
        }

        public void MoveUp()
        {
            Board.Control[PositionY[0], PositionX[0]].Background = Brushes.White;

            for (int i = 0; i < Length - 1; i++)
            {
                PositionX[i] = PositionX[i + 1];
                PositionY[i] = PositionX[i + 1];
            }

            PositionY[Length - 1]--;

            CreateSnake(PositionX, PositionY);
        }

        public void MoveDown()
        {
            Board.Control[PositionY[0], PositionX[0]].Background = Brushes.White;

            for (int i = 0; i < Length - 1; i++)
            {
                PositionX[i] = PositionX[i + 1];
                PositionY[i] = PositionY[i + 1];
            }

            PositionY[Length - 1]++;

            CreateSnake(PositionX, PositionY);
        }

        public void MoveLeft()
        {
            Board.Control[PositionY[0], PositionX[0]].Background = Brushes.White;

            for (int i = 0; i < Length - 1; i++)
            {
                PositionX[i] = PositionX[i + 1];
                PositionY[i] = PositionX[i + 1];
            }

            PositionX[Length - 1]--;

            CreateSnake(PositionX, PositionY);
        }

        public void MoveReight()
        {
            Board.Control[PositionY[0], PositionX[0]].Background = Brushes.White;

            for (int i = 0; i < Length - 1; i++)
            {
                PositionX[i] = PositionX[i + 1];
                PositionY[i] = PositionX[i + 1];
            }

            PositionX[Length - 1]++;

            CreateSnake(PositionX, PositionY);
        }
    }
}
