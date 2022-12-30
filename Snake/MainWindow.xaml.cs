using System;
using System.Collections.Generic;
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
        Board myBoard;
        SnakeAnimal mySnake;
        DispatcherTimer timer;
        private int count = 0;
        public string direction;

        public MainWindow()
        {
            InitializeComponent();

            // TODO: Timer läuft sehr "hakelig"
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(GameTick);
            timer.Interval = TimeSpan.FromSeconds(0.5);
        }

        //Funktion wird bei jedem Tick des Timers ausgelöst
        private void GameTick(object sender, EventArgs e)
        {
           PlayGame();
        }

        // TODO: Button funktioniert nur einmalig beim Start
        private void btn_Start_Click(object sender, RoutedEventArgs e)
        {
            StartGame();
        }

        // TODO: Button funktioniert nicht
        private void btn_Pause_Click(object sender, RoutedEventArgs e)
        {
            PauseGame();
        }

        //Start
        private void StartGame()
        {
            myBoard = new Board(SnakeUI);

            mySnake = new SnakeAnimal(myBoard);

            timer.Start();

        }

        //Stop
        private void PauseGame()
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
            }
            else
            {
                timer.Start();
            }
        }
        
        //Spielablauf
        public void PlayGame()
        {
            switch (direction) 
            {
                case "left":
                    mySnake.MoveLeft();
                    break;
                case "right":
                    mySnake.MoveReight();
                    break;
                case "up":
                    mySnake.MoveUp();
                    break;
                case "down":
                    mySnake.MoveDown();
                    break;
                default:
                    mySnake.MoveDown();
                    break;
            }
        }

        //Tastaturbelegung
        private void KeyEventArgs(object sender, KeyEventArgs e)
        {
            switch (e.Key) 
            {
                case Key.Left:
                    mySnake.MoveLeft();
                    direction = "left";
                    break;
                case Key.Right:
                    mySnake.MoveReight();
                    direction = "right";
                    break;
                case Key.Up:
                    mySnake.MoveUp();
                    direction = "up";
                    break;
                case Key.Down:
                    mySnake.MoveDown();
                    direction = "down";
                    break;
                // TODO: Buttons F1 und F2 funktionieren nicht
                case Key.F1:
                    StartGame();
                    break;
                case Key.F2:
                    PauseGame();
                    break;
                default:
                    break;
            }
        }
    }
}
