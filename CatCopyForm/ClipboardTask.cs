using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Windows.Forms;
using CatCopy.Control;

namespace CatCopyForm
{
    public class ClipboardTask
    {
        private static ClipboardController ClipboardController = ClipboardController.GetInstance();

        private Keys SlotKey;
        private ClipboardAction Action;
        private ClipbordDataObject SlotData;
        private ClipbordDataObject OriginalClipboardData;

        public ClipboardTask(Keys slotKey, ClipboardAction action)
        {
            SlotKey = slotKey;
            Action = action;
        }

        public ClipboardTask(Hotkey hotkey) : this(hotkey.Number, hotkey.Action)
        {
            
        }

        public void Perfom()
        {
            IntPtr handle = WindowUtil.GetForegroundWindow(); 

            if (Action == ClipboardAction.COPY)
            {
                SaveOriginalClipbordData();
                Clipboard.Clear();
                SendKeys.SendWait("^c");
//                Thread.Sleep(2000);
                StoreClipbordDataToSlot();
                RestoreOriginalClipboardData();
            }
            else
            {
                SaveOriginalClipbordData();
                LoadSlotDataIntoSystemClipboard();
                WindowUtil.SetForegroundWindow(handle.ToInt32());
                SendKeys.SendWait("^v");
                Debug.WriteLine("Pasted "+Clipboard.GetText());
                RestoreOriginalClipboardData();
                
            }
        }

        private void SaveOriginalClipbordData()
        {
            OriginalClipboardData = ClipboardController.SystemClipboardContent;
        }

        private void LoadSlotDataIntoSystemClipboard()
        {
            SlotData = ClipboardController.GetDataObjectForKey(SlotKey);
            ClipboardController.LoadDataIntoSystemClipboard(SlotData);
        }

        private void StoreClipbordDataToSlot()
        {
            var data = ClipboardController.SystemClipboardContent;
            ClipboardController.LoadDataIntoClipboardSlot(SlotKey, data);
        }

        private void RestoreOriginalClipboardData()
        {
            ClipboardController.LoadDataIntoSystemClipboard(OriginalClipboardData);
        }
    }
}