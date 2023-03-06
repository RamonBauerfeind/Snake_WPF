using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Snake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Todo: Icon
        Board myBoard;
        SnakeAnimal mySnake;
        Food myFruit;
        DispatcherTimer timer;
        public char direction;
        public bool extend = false;
        public int score = 0;
        private string highscore = "C:\\Users\\ramon\\source\\repos\\Snake_WPF\\Snake\\Snake\\Highscore.txt";

        public MainWindow()
        {
            InitializeComponent();

            myBoard = new Board(SnakeUI);

            myFruit = new Food(myBoard);

            mySnake = new SnakeAnimal(myBoard, myFruit);

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

        //Beenden
        private void QuitGame()
        {
            timer.Stop();
            SaveScore();
            Process.GetCurrentProcess().Kill();
        }
        
        //Spielablauf
        public void PlayGame()
        {
            //Schlange verlängern und Punkte hochzählen
            if(mySnake.Eat() == true)
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
                // TODO: F1 funktioniert nicht um das Spiel zu starten
                case Key.F1:
                    StartGame();
                    break;
                case Key.F2:
                    PauseGame();
                    break;
                case Key.F3:
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
                QuitGame();
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
                QuitGame();
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
    }
}
