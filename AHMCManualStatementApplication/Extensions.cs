using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace AHMCManualStatementApplication
{
    public static class Extensions
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        private const uint WM_SYSKEYDOWN = 0x104;

        public static void Open(this MetroFramework.Controls.MetroDateTime obj)
        {
            SendMessage(obj.Handle, WM_SYSKEYDOWN, (int)Keys.Down, 0);
        }
    }
}
