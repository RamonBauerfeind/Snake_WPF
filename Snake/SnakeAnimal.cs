using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

        public int[] PositionX { get; set; }

        public int[] PositionY { get; set; }

        public Board Board { get; set; }

        public Food Fruit { get; set; }


        //Methoden
        public SnakeAnimal(Board myBoard, Food fruit)
        {
            Board = myBoard;

            Fruit = fruit;

            PositionX = new int[] { 2, 2, 2 };

            PositionY = new int[] { 2, 3, 4 };

            Length = PositionX.Length;

            CreateSnake(PositionX, PositionY);
        }

        //Schlange erstellen
        private void CreateSnake(int[] positionX, int[] positionY)
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

        //Bewegung nach oben
        public void MoveUp()
        {
            Board.Control[PositionY[0], PositionX[0]].Background = Brushes.White;

            for (int i = 0; i < Length - 1; i++)
            {
                PositionX[i] = PositionX[i + 1];
                PositionY[i] = PositionY[i + 1];
            }

            PositionY[Length - 1]--;

            CreateSnake(PositionX, PositionY);
        }

        //Bewegung nach unten
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

        //Bewegung nach links
        public void MoveLeft()
        {
            Board.Control[PositionY[0], PositionX[0]].Background = Brushes.White;

            for (int i = 0; i < Length - 1; i++)
            {
                PositionX[i] = PositionX[i + 1];
                PositionY[i] = PositionY[i + 1];
            }

            PositionX[Length - 1]--;

            CreateSnake(PositionX, PositionY);
        }

        //Bewegung nach rechts
        public void MoveReight()
        {
            Board.Control[PositionY[0], PositionX[0]].Background = Brushes.White;

            for (int i = 0; i < Length - 1; i++)
            {
                PositionX[i] = PositionX[i + 1];
                PositionY[i] = PositionY[i + 1];
            }

            PositionX[Length - 1]++;

            CreateSnake(PositionX, PositionY);
        }

        public void Eat()
        {
            if((Fruit.PosX == PositionX[PositionX.Length - 1]) && (Fruit.PosY == PositionY[PositionY.Length - 1])) 
            {
                // TODO: Schlange verlängern
                //if(Fruit.FruitType() == "apple")
                //{
                //    //for(int i = Length - 1; i >= 0; i--)
                //    //{
                //    //    PositionX[i + 1] = PositionX[i];
                //    //    PositionY[i + 1] = PositionY[i];
                //    //}
                //    //CreateSnake(PositionX, PositionY);
                //    Fruit.CreateFruit();
                //}

                // TODO: Probleme beim Erstellen der Frucht beheben (Timer???)
                Fruit = new Food(Board);
            }
        }
    }
}
