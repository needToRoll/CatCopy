using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CatCopy.Control;

namespace CatCopyForm
{
    class HotkeyController
    {
        private static Dictionary<int, Hotkey> registry = new Dictionary<int, Hotkey>();

        public bool RegisterNumPadHotkeys(IntPtr handle)
        {
            const int NUM_PAD_0 = (int) Keys.NumPad0;
            const int NUM_PAD_9 = (int) Keys.NumPad9;
            var result = true;
            for (int keyId = NUM_PAD_0; keyId < NUM_PAD_9; keyId++)
            {
                var copyHotkey = new Hotkey(ClipboardAction.COPY, keyId);
                var pasteHotkey = new Hotkey(ClipboardAction.PASTE, keyId);
                result &= RegisterHotkey(handle, copyHotkey);
                result &= RegisterHotkey(handle, pasteHotkey);
            }
            return result;
        }

        public bool RegisterHotkey(IntPtr handle, Hotkey hotkey)
        {
            var result = HotkeyUtil.RegisterSystemHotKey(handle, hotkey);
            if (result)
            {
                registry.Add(hotkey.Id, hotkey);
            }
            return result;
        }

        public bool UnregisterHotkey(IntPtr handle, Hotkey hotkey)
        {
            var result = HotkeyUtil.UnregisterSystemHotKey(handle, hotkey);
            if (result)
            {
                registry.Remove(hotkey.Id);
            }
            return result;
        }

        public void PerformHotkeyAction(int id)
        {
            var hotkey = registry[id];
            var task = new ClipboardTask(hotkey);            
            task.Perfom();
        }

        public bool UnregisterAllHotkeys(IntPtr handle)
        {
            Hotkey[] hotkeys = new Hotkey[registry.Values.Count];
            registry.Values.CopyTo(hotkeys, 0);
            return hotkeys.Aggregate(true, (current, hotkey) => current & UnregisterHotkey(handle, hotkey));
        }
    }
}