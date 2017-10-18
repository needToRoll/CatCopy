using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CatCopyForm
{
    public partial class Form1 : Form
    {
        HotkeyController HotkeyController = new HotkeyController();

        public Form1()
        {
            InitializeComponent();
            var registrationResult = HotkeyController.RegisterNumPadHotkeys(Handle);
            var message = "registration status: ";
            message += registrationResult ? "success" : "fail";
            Debug.WriteLine(message);
        }

        protected override void WndProc(ref Message  message)
        {
            if (message.Msg == HotkeyUtil.WM_HOTKEY)
            {
                var hotkeyId = message.WParam.ToInt32();
                HotkeyController.PerformHotkeyAction(hotkeyId);
                Debug.WriteLine("Action: "+hotkeyId+" performed");
            }

            base.WndProc(ref message);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            var unregistrationResult = HotkeyController.UnregisterAllHotkeys(Handle);
            var message = "unregistration status: ";
            message += unregistrationResult ? "success" : "fail";
            Debug.WriteLine(message);
            base.OnFormClosing(e);
        }
    }
}