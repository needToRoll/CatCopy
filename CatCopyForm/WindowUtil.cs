using System;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;

namespace CatCopyForm
{
    public class WindowUtil
    {
        public const int WM_GETTEXT = 0x000D;
        
        public static string GetActiveWindowTitle()
        {
            return GetWindowTitle(GetForegroundWindow());
        }

        public static string GetWindowTitle(IntPtr handle)
        {
            var activeWIndowHandle = handle;
            var titleBufferLength = GetWindowTextLength(activeWIndowHandle) + 16;
            var windowTitleBuffer = new StringBuilder(titleBufferLength);
            GetWindowText(activeWIndowHandle, windowTitleBuffer, titleBufferLength);
            return windowTitleBuffer.ToString();
        }
        public static IntPtr GetFocusedControlHandle()
        {
            var activeWindowHandle = GetForegroundWindow().ToInt32();
            GetWindowThreadProcessId(activeWindowHandle, out var activeWindowThreadId);
            var currentThreadId = GetCurrentThreadId();
            if (activeWindowThreadId != currentThreadId)
            {
                AttachThreadInput(activeWindowThreadId, currentThreadId, true);
            }
            return GetFocus();
        }

        [DllImport("user32.dll")]
        public static extern int SetForegroundWindow(int hwnd);
        
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetFocus();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(int handle, out int processId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        private static extern int AttachThreadInput(int idAttach, int idAttachTo, bool fAttach);

        [DllImport("kernel32.dll")]
        private static extern int GetCurrentThreadId();

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll")]
        private static extern int GetWindowTextLength(IntPtr hWnd);
    }
}