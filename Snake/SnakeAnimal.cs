using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

        public char fruit;


        //Methoden
        public SnakeAnimal(Board myBoard, Food myFruit)
        {
            Board = myBoard;

            Fruit = myFruit;

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
            for (int i = 0; i < Length; i++)
            {
                if (i == Length - 1)
                {
                    Board.Control[positionY[i], positionX[i]].Background = Brushes.DarkViolet;
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
            if(extend == false)
            {
                NotExtend();

                PositionY[Length - 1]--;
            }
            else
            {
                Extend();

                PositionY[Length - 1]--;
            }

            CreateSnake(PositionX, PositionY);
        }

        //Bewegung nach unten
        public void MoveDown(bool extend)
        {
            if(extend == false)
            {
                NotExtend();

                PositionY[Length - 1]++;
            }
            else
            {
                Extend();

                PositionY[Length - 1]++;
            }

            CreateSnake(PositionX, PositionY);
        }

        //Bewegung nach links
        public void MoveLeft(bool extend)
        {
           if(extend == false)
           {
                NotExtend();

                PositionX[Length - 1]--;
           }
           else
            {
                Extend();

                PositionX[Length - 1]--;
            }

            CreateSnake(PositionX, PositionY);
        }

        //Bewegung nach rechts
        public void MoveReight(bool extend)
        {
            if(extend == false)
            {
                NotExtend();

                PositionX[Length - 1]++;
            }
            else
            {
                Extend();

                PositionX[Length - 1]++;
            }

            CreateSnake(PositionX, PositionY);
        }

        //Schlange nicht verlängern
        private void NotExtend()
        {
            Board.Control[PositionY[0], PositionX[0]].Background = Brushes.White;

            for (int i = 0; i < Length - 1; i++)
            {
                PositionX[i] = PositionX[i + 1];
                PositionY[i] = PositionY[i + 1];
            }
        }

        //Schlange verlängern
        private void Extend()
        {
            //apple -> Verlängern um 1 Feld
            if(fruit.Equals('a'))
            {
                PositionX.Add(PositionX[PositionX.Count - 1]);
                PositionY.Add(PositionY[PositionY.Count - 1]);
                Board.Control[PositionY[0], PositionX[0]].Background = Brushes.White;
                Length++;
            }
            //banana -> Verlängern um 2 Felder
            else
            {
                PositionX.Add(PositionX[PositionX.Count - 1]);
                PositionY.Add(PositionY[PositionY.Count - 1]);
                PositionX.Add(PositionX[PositionX.Count - 2]);
                PositionY.Add(PositionY[PositionY.Count - 2]);
                Board.Control[PositionY[0], PositionX[0]].Background = Brushes.White;
                Board.Control[PositionY[1], PositionX[1]].Background = Brushes.White;
                Length = Length + 2;
            }
            
            for (int i = 0; i < Length - 1; i++)
            {
                PositionX[i] = PositionX[i + 1];
                PositionY[i] = PositionY[i + 1];
            }
        }

        //Fressen
        public bool Eat()
        {
            bool eat = false;

            if((Fruit.PosX == PositionX[Length - 1]) && (Fruit.PosY == PositionY[Length - 1])) 
            {
                fruit = Fruit.Fruit;
                // TODO: ausschließen dass Food in Schlange erstellt wird)
                Fruit = new Food(Board);

                eat = true;

                return eat;
            }

            return eat;
        }

        //Prüfung ob Schlange mit sich selbst kollidiert
        public bool CollisionSnake()
        {
            bool collisonSnake = false;

            for (int i = 0; i < Length - 1; i++)
            {
                if (PositionX[Length - 1] == PositionX[i] && PositionY[Length - 1] == PositionY[i])
                {
                    MessageBox.Show("Game Over");
                    collisonSnake = true;
                }
            }

            return collisonSnake;
        }

        //Prüfung ob Schlange das Spielfeld verlässt
        public bool CollisionBoard(char direction, bool goThrough)
        {
            bool collisionBoard = false;

            //wenn checkbox GoThrough == false -> GameOver am Spielfeldrand
            if(goThrough == false)
            {
                if (PositionY[Length - 1] == Board.Rows - 2 && direction == 'd')
                {
                    MessageBox.Show("Game Over");
                    collisionBoard = true;
                }
                else if (PositionY[Length - 1] == 1 && direction == 'u')
                {
                    MessageBox.Show("Game Over");
                    collisionBoard = true;
                }
                else if (PositionX[Length - 1] == Board.Columns - 2 && direction == 'r')
                {
                    MessageBox.Show("Game Over");
                    collisionBoard = true;
                }
                else if (PositionX[Length - 1] == 1 && direction == 'l')
                {
                    MessageBox.Show("Game Over");
                    collisionBoard = true;
                }
            }
            //wenn checkbox GoThrough == true -> Durchlaufen des Spielfeldes
            else
            {
                if (PositionY[Length - 1] == Board.Rows - 2 && direction == 'd')
                {
                    Board.Control[PositionY[0], PositionX[0]].Background = Brushes.White;

                    for (int i = 0; i < Length - 1; i++)
                    {
                        PositionY[i] = PositionY[i + 1];
                    }
                    
                    PositionY[Length - 1] = 1;
                }
                else if (PositionY[Length - 1] == 1 && direction == 'u')
                {
                    Board.Control[PositionY[0], PositionX[0]].Background = Brushes.White;

                    for (int i = 0; i < Length - 1; i++)
                    {
                        PositionY[i] = PositionY[i + 1];
                    }

                    PositionY[Length - 1] = Board.Rows - 2;
                }
                else if (PositionX[Length - 1] == Board.Columns - 2 && direction == 'r')
                {
                    Board.Control[PositionY[0], PositionX[0]].Background = Brushes.White;

                    for (int i = 0; i < Length - 1; i++)
                    {
                        PositionX[i] = PositionX[i + 1];
                    }

                    PositionX[Length - 1] = 1;
                }
                else if (PositionX[Length - 1] == 1 && direction == 'l')
                {
                    Board.Control[PositionY[0], PositionX[0]].Background = Brushes.White;

                    for (int i = 0; i < Length - 1; i++)
                    {
                        PositionX[i] = PositionX[i + 1];
                    }

                    PositionX[Length - 1] = Board.Columns - 2;
                }
            }
            
            return collisionBoard;
        }
    }
}
