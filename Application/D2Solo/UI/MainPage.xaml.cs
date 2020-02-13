using D2Solo.Backend;
using System.Windows;
using System.Windows.Controls;

namespace D2Solo.UI
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private bool isMinimized = false;

        public MainPage()
        {
            this.DataContext = MainPageViewModel.getInstance();
            InitializeComponent();
        }

        private void minimize(object sender, System.Windows.RoutedEventArgs e)
        {
            Window appWindow = Application.Current.MainWindow;
            if (!isMinimized)
            {
                appWindow.Width = 50;
                appWindow.Height = 10;
                controls.SetValue(Grid.ColumnProperty, 0);
            }
            else
            {
                appWindow.Width = 275;
                appWindow.Height = 75;
                controls.SetValue(Grid.ColumnProperty, 2);
            }

            ((MainWindow)Application.Current.MainWindow).snapTopRight();
            isMinimized = !isMinimized;
        }

        private void exitProg(object sender, System.Windows.RoutedEventArgs e)
        {
            if (MainPageViewModel.getInstance().SoloEnabled)
                FirewallCommands.OpenMatchmakingPorts();

            Application.Current.Shutdown();
        }

        private void lockToggle(object sender, System.Windows.RoutedEventArgs e)
        {
            bool current = MainPageViewModel.getInstance().CanToggle;
            MainPageViewModel.getInstance().CanToggle = !current;
        }

        private void toggleSoloMode(object sender, System.Windows.RoutedEventArgs e)
        {
            if (MainPageViewModel.getInstance().SoloEnabled)
                FirewallCommands.OpenMatchmakingPorts();
            else
                FirewallCommands.BlockMatchmakingPorts();
        }
    }
}
