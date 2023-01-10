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

        public List<int> PositionX { get; set; }

        public List<int> PositionY { get; set; }

        public Board Board { get; set; }

        public Food Fruit { get; set; }


        //Methoden
        public SnakeAnimal(Board myBoard, Food fruit)
        {
            Board = myBoard;

            Fruit = fruit;

            PositionX = new List<int>();
            PositionX.Add(2);
            PositionX.Add(2);
            PositionX.Add(2);

            PositionY = new List<int>();
            PositionY.Add(2);
            PositionY.Add(3);
            PositionY.Add(4);

            Length = PositionX.Count;

            CreateSnake(PositionX, PositionY);
        }

        //Schlange erstellen
        private void CreateSnake(List<int> positionX, List<int> positionY)
        {
            for (int i = 0; i < positionX.Count; i++)
            {
                if (i == positionX.Count - 1)
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
        public void MoveUp(bool extend)
        {
            int x = 0;
            int y = 0;

            if(extend == false)
            {
                Board.Control[PositionY[0], PositionX[0]].Background = Brushes.White;

                for (int i = 0; i < Length - 1; i++)
                {
                    PositionX[i] = PositionX[i + 1];
                    PositionY[i] = PositionY[i + 1];
                }

                PositionY[Length - 1]--;
            }
            else
            {
                // TODO: Verlängerung der Schlange für alle Richtungen integrieren / funktioniert nur einmalig
                PositionX.Add(PositionX[PositionX.Count - 1]);
                PositionY.Add(PositionY[PositionY.Count - 1]);
                Board.Control[PositionY[0], PositionX[0]].Background = Brushes.White;
                Length++;

                for (int i = 0; i < Length - 1; i++)
                {
                    PositionX[i] = PositionX[i + 1];
                    PositionY[i] = PositionY[i + 1];
                }

                PositionY[Length - 1]--;
            }

            CreateSnake(PositionX, PositionY);
        }

        //Bewegung nach unten
        public void MoveDown(bool extend)
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
        public void MoveLeft(bool extend)
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
        public void MoveReight(bool extend)
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

        public bool Eat()
        {
            bool eat = false;

            if((Fruit.PosX == PositionX[Length - 1]) && (Fruit.PosY == PositionY[Length - 1])) 
            {
                eat = true;

                return eat;
            }

            return eat;
        }
    }
}
