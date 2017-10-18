using System.Reflection;
using System.Security.Policy;
using System.Windows.Forms;
using System.Windows.Input;
namespace CatCopyForm
{
    public class Hotkey
    {
        public int Id { get; }
        public Keys Number { get; }
        public ClipboardAction Action { get; }
        public int Modifier = HotkeyUtil.CTRL_MODIFIER_KEY | HotkeyUtil.NO_REPEATE_MODIFIER_KEY;

        public Hotkey(ClipboardAction action, Keys numberKey)
        {
            Action = action;
            Id = HotkeyUtil.GetNextHotKeyId();
            if (Action == ClipboardAction.PASTE)
            {
                Modifier = Modifier | HotkeyUtil.ALT_MODIFIER_KEY;
            }
            Number = numberKey;
        }

        public Hotkey(ClipboardAction action, int numberKeyId) : this(action, (Keys) numberKeyId)
        {
        }
    }
}