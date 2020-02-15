using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace D2Solo.Backend
{
    class HotkeyHandler
    {
        /**** Credit to:
         * https://stackoverflow.com/questions/11377977/global-hotkeys-in-wpf-working-from-every-window ****/
        private const int HOTKEY_ID = 9000;

        [DllImport("User32.dll")]
        private static extern bool RegisterHotKey([In] IntPtr hWnd, [In] int id, [In] uint fsModifiers, [In] uint vk);

        [DllImport("User32.dll")]
        private static extern bool UnregisterHotKey([In] IntPtr hWnd, [In] int id);

        public static void RegisterHotKey(Window appWindow)
        {
            var helper = new WindowInteropHelper(appWindow);
            const uint VK_F10 = 0x79;
            const uint MOD_CTRL = 0x0002;
            //to allow custom keybinding, change arguments on next line
            if (!RegisterHotKey(helper.Handle, HOTKEY_ID, MOD_CTRL, VK_F10))
            {
                // handle error
            }
        }
        public static void UnregisterHotKey(Window appWindow)
        {
            var helper = new WindowInteropHelper(appWindow);
            UnregisterHotKey(helper.Handle, HOTKEY_ID);
        }

        public static IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;
            switch (msg)
            {
                case WM_HOTKEY:
                    switch (wParam.ToInt32())
                    {
                        case HOTKEY_ID:
                            ((MainWindow)Application.Current.MainWindow).HandleHotKey();
                            handled = true;
                            break;
                    }
                    break;
            }
            return IntPtr.Zero;
        }

    }
}
