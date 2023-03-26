using System;
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

        //Zufallszahl für X-Koordinate
        private int PositionX(Board board)
        {
            Random randX = new Random();
            positionX = randX.Next(1, board.Columns - 1);
            
            return positionX;
        }

        //Zufallszahl für Y-Koordinate
        private int PositionY(Board board) 
        {
            Random randY = new Random();
            positionY = randY.Next(1, board.Rows - 1);

            return positionY;
        }

        //Art der Frucht
        public char FruitType()
        {
            char fruit;
            Random typeOfFruit = new Random();
            int type = typeOfFruit.Next(1, 3);

            //a == apple -> 1 -> Verlängern der Schlange um 1 Feld
            //b == banana -> 2 -> -> Verlängern der Schlange um 2 Felder
            switch (type)
            {
                case 1:
                    fruit = 'a';
                    break;
                case 2:
                    fruit = 'b';
                    break;
                default:
                    fruit = 'a';
                    break;
            }

            return fruit;
        }

        //Erstellen einer Frucht
        public void CreateFruit()
        {
            switch(Fruit) 
            {
                case 'a':
                    Board.Control[PosY, PosX].Background = Brushes.Green;
                    break;
                case 'b':
                    Board.Control[PosY, PosX].Background = Brushes.GreenYellow;
                    break;
                default:
                    Board.Control[PosY, PosX].Background = Brushes.Green;
                    break;
            }
        }
    }
}
