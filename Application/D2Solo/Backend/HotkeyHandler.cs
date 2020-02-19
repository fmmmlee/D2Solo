using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace D2Solo.Backend
{
    class HotkeyHandler
    {
        //https://stackoverflow.com/questions/10484085/get-all-keys-that-are-pressed
        //https://stackoverflow.com/questions/11377977/global-hotkeys-in-wpf-working-from-every-window


        private static readonly byte[] VirtualKeyCodes = Enumerable
            .Range(0, 256)
            .Select(KeyInterop.KeyFromVirtualKey)
            .Where(item => item != Key.None)
            .Distinct()
            .Select(item => (byte)KeyInterop.VirtualKeyFromKey(item))
            .ToArray();
        
        
        private const int HOTKEY_ID = 9000;
        const uint VK_F10 = 0x79;
        const uint MOD_CTRL = 0x0002;


        [DllImport("User32.dll")]
        private static extern bool RegisterHotKey([In] IntPtr hWnd, [In] int id, [In] uint fsModifiers, [In] uint vk);

        [DllImport("User32.dll")]
        private static extern bool UnregisterHotKey([In] IntPtr hWnd, [In] int id);

        [DllImport("User32.dll")]
        private static extern bool GetKeyboardState(byte[] lpKeyState);



        //should save to a config file and then application reads config file on startup
        public static void SetHotKeys()
        {
            byte[] keyboardState = new byte[256];
            GetKeyboardState(keyboardState);

            List<int> pressedKeyCodes = new List<int>();
            foreach (var virtualKey in VirtualKeyCodes)
            {
                if ((keyboardState[virtualKey] & 0x80) != 0)
                    pressedKeyCodes.Add(virtualKey);
            }

            //write list to config file

            //register those keys as the application hotkeys
        }


        public static void RegisterHotKey(Window appWindow)
        {
            var helper = new WindowInteropHelper(appWindow);
            
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
