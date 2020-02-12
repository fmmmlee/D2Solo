using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Navigation;

namespace D2Solo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            setFullSizeLoc((Window)sender);
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            setFullSizeLoc((Window)sender);
        }

        public static void setFullSizeLoc(Window appWindow)
        {
            appWindow.Topmost = true;
            appWindow.Left = Screen.PrimaryScreen.Bounds.Width - appWindow.Width; //Screen is from WinForms
            appWindow.Top = 0; //TODO: Make this handle the taskbar
        }

    }
}
