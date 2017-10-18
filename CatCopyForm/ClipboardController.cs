using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using CatCopyForm;

namespace CatCopy.Control
{
    public class ClipboardController
    {
        private ClipboardController()
        {
            ExtendedClipboard = new Dictionary<Keys, ClipbordDataObject>();
        }

        private static ClipboardController Instance = null;

        public static ClipboardController GetInstance()
        {
            if (Instance == null)
            {
                Instance = new ClipboardController();
            }
            return Instance;
        }

        private Dictionary<Keys, ClipbordDataObject> ExtendedClipboard;

        private ClipbordDataObject _systemClipboardContent;

        public ClipbordDataObject SystemClipboardContent
        {
            get
            {
                _systemClipboardContent = ClipbordDataObject.GenerateFromClipboard();
                return _systemClipboardContent;
            }
            private set { _systemClipboardContent = value; }
        }

        public ClipbordDataObject GetDataObjectForKey(Keys slotKey)
        {
            return ExtendedClipboard[slotKey];
        }

        public void LoadDataIntoClipboardSlot(Keys slotKey, ClipbordDataObject data)
        {
            ExtendedClipboard[slotKey] = data;
        }

        public void LoadDataIntoSystemClipboard(ClipbordDataObject data)
        {
            if (data == null) return;
            Clipboard.Clear();
            Clipboard.SetData(data.Format, data.Data);
        }
    }
}