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
        [DllImport("User32.dll")]
        private static extern bool RegisterHotKey([In] IntPtr hWnd, [In] int id, [In] uint fsModifiers, [In] uint vk);

        [DllImport("User32.dll")]
        private static extern bool UnregisterHotKey([In] IntPtr hWnd, [In] int id);

        private HwndSource _source;
        private const int HOTKEY_ID = 9000;

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            IntPtr handle = new WindowInteropHelper(this).Handle;
            _source = HwndSource.FromHwnd(handle);
            _source.AddHook(HwndHook);

            RegisterHotKey();
        }
        protected override void OnClosed(EventArgs e)
        {
            _source.RemoveHook(HwndHook);
            _source = null;
            UnregisterHotKey();
            base.OnClosed(e);
        }

        private void RegisterHotKey()
        {
            var helper = new WindowInteropHelper(this);
            const uint VK_F10 = 0x79;
            const uint MOD_CTRL = 0x0002;
            if (!RegisterHotKey(helper.Handle, HOTKEY_ID, MOD_CTRL, VK_F10))
            {
                // handle error
            }
        }
        private void UnregisterHotKey()
        {
            var helper = new WindowInteropHelper(this);
            UnregisterHotKey(helper.Handle, HOTKEY_ID);
        }

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;
            switch (msg)
            {
                case WM_HOTKEY:
                    switch (wParam.ToInt32())
                    {
                        case HOTKEY_ID:
                            HandleHotKey();
                            handled = true;
                            break;
                    }
                    break;
            }
            return IntPtr.Zero;
        }

        private void HandleHotKey()
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
