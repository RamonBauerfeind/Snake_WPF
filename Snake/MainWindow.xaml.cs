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
        Food myFruit;
        DispatcherTimer timer;
        public char direction;
        public bool extend = false;

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

        //Start
        private void StartGame()
        {
            // TODO: Clear Board
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
            // TODO: Food wird nicht immer erstellt -> ausschließen, dass Food in Schlange erstellt wird)
            if(mySnake.Eat() == true)
            {
                //myFruit = new Food(myBoard);
                extend = true;
            }

            switch (direction) 
            {
                case 'l':
                    mySnake.MoveLeft(extend);
                    break;
                case 'r':
                    mySnake.MoveReight(extend);
                    break;
                case 'u':
                    mySnake.MoveUp(extend);
                    break;
                case 'd':
                    mySnake.MoveDown(extend);
                    break;
                default:
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
                default:
                    break;
            }
        }
    }
}
