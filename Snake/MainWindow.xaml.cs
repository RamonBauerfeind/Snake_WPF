using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
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
        //TODO: Bugfixes im gesamten Spiel
        //TODO: Highscore speichern und anzeigen
        Board myBoard;
        SnakeAnimal mySnake;
        Food myFruit;
        DispatcherTimer timer;
        public char direction;
        public bool extend = false;
        public int score = 0;

        public MainWindow()
        {
            InitializeComponent();

            myBoard = new Board(SnakeUI);

            myFruit = new Food(myBoard);

            mySnake = new SnakeAnimal(myBoard, myFruit);

            // TODO: Timer läuft sehr "hakelig"
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(GameTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 250);
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
            Application.Current.Shutdown();
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
                    mySnake.MoveLeft(extend);
                    CheckCollisionBoard();
                    break;
                case 'r':
                    CheckCollisionSnake();
                    mySnake.MoveReight(extend);
                    CheckCollisionBoard();
                    break;
                case 'u':
                    CheckCollisionSnake();
                    mySnake.MoveUp(extend);
                    CheckCollisionBoard();
                    break;
                case 'd':
                    CheckCollisionSnake();
                    mySnake.MoveDown(extend);
                    CheckCollisionBoard();
                    break;
                default:
                    CheckCollisionSnake();
                    mySnake.MoveDown(extend);
                    CheckCollisionBoard();
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
                    mySnake.MoveLeft(extend);
                    direction = 'l';
                    break;
                case Key.Right:
                    mySnake.MoveReight(extend);
                    direction = 'r';
                    break;
                case Key.Up:
                    mySnake.MoveUp(extend);
                    direction = 'u';
                    break;
                case Key.Down:
                    mySnake.MoveDown(extend);
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
            bool collisionSnake = mySnake.CollisionSnake();

            if (collisionSnake == true)
            {
                timer.Stop();
                Application.Current.Shutdown();
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

            bool collisionBoard = mySnake.CollisionBoard(direction, goThrough);

            if (collisionBoard == true)
            {
                timer.Stop();
                Application.Current.Shutdown();
            }
        }
    }
}
