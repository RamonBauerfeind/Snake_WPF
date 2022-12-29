using System;
using System.Collections.Generic;
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

        public MainWindow()
        {
            InitializeComponent();

            // TODO: Timer läuft sehr "hakelig"
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(GameTick);
            timer.Interval = TimeSpan.FromSeconds(0.5);
        }

        // Funktion wird bei jedem Tick des Timers ausgelöst
        private void GameTick(object sender, EventArgs e)
        {
            mySnake.MoveDown();
        }

        private void btn_Start_Click(object sender, RoutedEventArgs e)
        {
            myBoard = new Board(SnakeUI);

            mySnake = new SnakeAnimal(myBoard);

            timer.Start();
        }

        // TODO: Button funktioniert nicht
        private void btn_Pause_Click(object sender, RoutedEventArgs e)
        {
            if(timer.IsEnabled)
            {
                timer.Stop();
            }
            else 
            {
                timer.Start();
            }
        }
    }
}
