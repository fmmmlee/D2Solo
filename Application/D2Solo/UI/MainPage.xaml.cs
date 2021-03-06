﻿using D2Solo.Backend;
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

        private void minimize(object sender, RoutedEventArgs e)
        {
            if (!isMinimized)
                MainPageViewModel.getInstance().UIVisibility = Visibility.Hidden;
            else
                MainPageViewModel.getInstance().UIVisibility = Visibility.Visible;

            ((MainWindow)Application.Current.MainWindow).snapTopRight();
            isMinimized = !isMinimized;
        }

        private void openWiki(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/fmmmlee/D2Solo/wiki");
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
