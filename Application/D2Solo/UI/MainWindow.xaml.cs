using D2Solo.Backend;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
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
            HideFromWindowManager();
            InitializeComponent();
        }

        /**** Hotkeys ****/
        /**** https://stackoverflow.com/questions/11377977/global-hotkeys-in-wpf-working-from-every-window ****/
        private HwndSource _source;
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            IntPtr handle = new WindowInteropHelper(this).Handle;
            _source = HwndSource.FromHwnd(handle);
            _source.AddHook(HotkeyHandler.HwndHook);

            HotkeyHandler.RegisterHotKey(this);
        }
        protected override void OnClosed(EventArgs e)
        {
            _source.RemoveHook(HotkeyHandler.HwndHook);
            _source = null;
            HotkeyHandler.UnregisterHotKey(this);
            base.OnClosed(e);
        }
        public void HandleHotKey()
        {
            switch (this.Visibility)
            {
                case Visibility.Visible:
                    this.Visibility = Visibility.Hidden;
                    break;
                case Visibility.Hidden:
                    this.Visibility = Visibility.Visible;
                    snapTopRight();
                    break;
            }
        }
        /**** end hotkey code ****/


        /**** Always-on-top ****/
        //MWL 2-13-2020: currently these two methods are identical but eventually they may have different purposes so I'm not combining them
        private void Window_Activated(object sender, EventArgs e)
        {
            snapTopRight();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            snapTopRight();
        }


        //TODO: Position options
        public void snapTopRight()
        {
            this.Topmost = true;
            this.Left = Screen.PrimaryScreen.Bounds.Width - this.Width; //Screen class from WinForms
            this.Top = 0; //TODO: Make this handle the taskbar
        }
        private void HideFromWindowManager()
        {
            //https://social.msdn.microsoft.com/Forums/vstudio/en-US/8e3a788e-1e14-4751-a756-2d68358f898b/hide-icon-in-alttab?forum=wpf
            Window w = new Window(); // Create helper window
            w.Top = -100; // Location of new window is outside of visible part of screen
            w.Left = -100;
            w.Width = 1; // size of window is enough small to avoid its appearance at the beginning
            w.Height = 1;

            w.WindowStyle = WindowStyle.ToolWindow; // Set window style as ToolWindow to avoid its icon in AltTab 
            w.ShowInTaskbar = false;
            w.Show(); // We need to show window before set is as owner to our main window
            this.Owner = w; // Okey, this will result to disappear icon for main window.
            w.Hide(); // Hide helper window just in case
        }
    }
}
