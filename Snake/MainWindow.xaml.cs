using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Snake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Board myBoard;

        SnakeAnimal mySnake;

        Food myFruit;

        DispatcherTimer timer;

        public char direction;

        public bool extend = false;

        public int score = 0;

        private string highscore = "C:\\Users\\Public\\Documents\\Snake\\Highscore.txt";

        public MainWindow()
        {
            InitializeComponent();

            myBoard = new Board(SnakeUI);

            myFruit = new Food(myBoard);

            mySnake = new SnakeAnimal(myBoard, myFruit);

            TypeOfFruit();

            ShowScore();

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(GameTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
        }

        //Funktion wird bei jedem Tick des Timers ausgelöst
        private void GameTick(object? sender, EventArgs e)
        {
           PlayGame();
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void btn_Pause_Click(object sender, EventArgs e)
        {
            PauseGame();
        }

        private void btn_Restart_Click(object sender, RoutedEventArgs e)
        {
            RestartGame();
        }

        private void btn_Stop_Quit(object sender, RoutedEventArgs e)
        {
            QuitGame();
        }

        //Start
        private void StartGame()
        {
            if(timer.IsEnabled == false)
            {
                timer.Start();
            }
        }

        //Stop
        private void PauseGame()
        {
            if (timer.IsEnabled == true)
            {
                timer.Stop();
            }
        }

        //Restart
        private void RestartGame()
        {
            timer.Stop();
            SaveScore();
            MainWindow newWindow = new MainWindow();
            Application.Current.MainWindow = newWindow;
            newWindow.Show();
            this.Close();
        }

        //Beenden
        private void QuitGame()
        {
            timer.Stop();
            SaveScore();
            Application.Current.Shutdown();
        }
        
        //Spielablauf
        public void PlayGame()
        {
            TypeOfFruit();

            //Schlange verlängern und Punkte hochzählen
            if (mySnake.Eat() == true)
            {
                extend = true;

                //apple == score + 10
                if(mySnake.fruit.Equals('a'))
                {
                    score = score + 10;
                }
                //banana == score + 20
                else
                {
                    score = score + 20;
                }

                lb_Score.Content = "Score: " + score;
            }

            //Bewegung der Schlange
            switch (direction) 
            {
                case 'l':
                    CheckCollisionSnake();
                    CheckCollisionBoard();
                    mySnake.MoveLeft(extend);
                    break;
                case 'r':
                    CheckCollisionSnake();
                    CheckCollisionBoard();
                    mySnake.MoveReight(extend);
                    break;
                case 'u':
                    CheckCollisionSnake();
                    CheckCollisionBoard();
                    mySnake.MoveUp(extend);
                    break;
                case 'd':
                    CheckCollisionSnake();
                    CheckCollisionBoard();
                    mySnake.MoveDown(extend);
                    break;
                default:
                    CheckCollisionSnake();
                    CheckCollisionBoard();
                    mySnake.MoveDown(extend);
                    break;
            }

            extend = false;
        }

        //Tastaturbelegung
        private void KeyEventArgs(object sender, KeyEventArgs e)
        {
            switch (e.Key) 
            {
                case Key.Left:
                    direction = 'l';
                    break;
                case Key.Right:
                    direction = 'r';
                    break;
                case Key.Up:
                    direction = 'u';
                    break;
                case Key.Down:
                    direction = 'd';
                    break;
                case Key.F1:
                    StartGame();
                    break;
                case Key.F2:
                    PauseGame();
                    break;
                case Key.F3:
                    RestartGame();
                    break;
                case Key.F4:
                    QuitGame();
                    break;
                default:
                    break;
            }
        }

        //Kollisionsprüfung Schlange
        private void CheckCollisionSnake()
        {
            bool collisionSnake = mySnake.CollisionSnake(score.ToString());

            if (collisionSnake == true)
            {
                RestartGame();
            }
        }

        //Kollisionsprüfung Board
        private void CheckCollisionBoard()
        {
            bool goThrough;
            
            if(cb_GoThrough.IsChecked == true) 
            {
                goThrough = true;
            }
            else
            {
                goThrough = false;
            }

            bool collisionBoard = mySnake.CollisionBoard(direction, goThrough, score.ToString());

            if (collisionBoard == true)
            {
                RestartGame();
            }
        }

        //Speichern des Highscore
        private void SaveScore()
        {
            string strHighscore;
            int intHighscore;

            if(File.Exists(highscore))
            {
                strHighscore = File.ReadAllText(highscore);
                intHighscore = int.Parse(strHighscore);

                if(intHighscore > score)
                {
                    File.WriteAllText(highscore, intHighscore.ToString());
                }
                else
                {
                    File.WriteAllText(highscore, score.ToString());
                }
            }
            else
            {
                File.WriteAllText(highscore, score.ToString());
            }
        }
        
        //Anzeigen des Highscore
        private void ShowScore()
        {
            string strHighscore;

            if (File.Exists(highscore))
            {
                strHighscore = File.ReadAllText(highscore);
                lbl_highscore.Content = strHighscore;
            }
            else
            {
                lbl_highscore.Content = "0";
            }
        }

        private void TypeOfFruit()
        {
            switch (mySnake.fruitForLabel)
            {
                case 'a':
                    lbl_TypeOfFruit.Content = "Apple";
                    lbl_TypeOfFruit.Foreground = Brushes.Green;
                    break;
                case 'b':
                    lbl_TypeOfFruit.Content = "Banana";
                    lbl_TypeOfFruit.Foreground = Brushes.GreenYellow;
                    break;
                default:
                    lbl_TypeOfFruit.Content = "Fruit";
                    lbl_TypeOfFruit.Foreground = Brushes.Red;
                    break;
            }
        }
    }
}
