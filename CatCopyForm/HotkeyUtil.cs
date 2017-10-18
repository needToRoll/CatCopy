using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CatCopyForm
{
    internal static class HotkeyUtil
    {
        private static int HotKeyId = 2000;
        
        public const int WM_COPY = 0x0301;
        public const int WM_PASTE = 0x0302;
        public const int WM_HOTKEY = 0x0312;
        
        public const int CTRL_MODIFIER_KEY = 0x0002;
        public const int ALT_MODIFIER_KEY = 0x0001;
        public const int NO_REPEATE_MODIFIER_KEY = 0x4000;


        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public static bool RegisterSystemHotKey(IntPtr handle, Hotkey hotkey)
        {
            var registrationResult = RegisterHotKey(handle, hotkey.Id, hotkey.Modifier, (int) hotkey.Number);
            return registrationResult;
        }

        public static bool UnregisterSystemHotKey(IntPtr handle, Hotkey hotkey)
        {
            var unregistrationResult = UnregisterHotKey(handle, hotkey.Id); 
            return unregistrationResult;
        }

        public static int GetNextHotKeyId()
        {
            var id = HotKeyId;
            HotKeyId++;
            return id;
        }

        public static int SendMessageToHandle(IntPtr targetWindowHandle, int message)
        {
            return SendMessage(targetWindowHandle, message, IntPtr.Zero, IntPtr.Zero);
        }
        
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, IntPtr lparam, IntPtr wparam);
    }
}