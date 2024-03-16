using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TSWAnnouncer
{
    public partial class main : Form
    {
        const int mActionHotKeyID = 1;
        public string path;
        public string route;
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        public main(string path, string route)
        {
            RegisterHotKey(this.Handle, mActionHotKeyID, 0, (int)Keys.F7);
            this.path = path;
            this.route = route;
            InitializeComponent();
            var routeName = files.JSpa(route, "$.info.name");
            LblCurRoute.Text = "Current Route: " + routeName;
            string statName = files.JSpa(route, "$.statlist[0].name");
            LblCurStat.Text = "Next stop: " + statName;
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312 && m.WParam.ToInt32() == mActionHotKeyID)
            {
                callForPlayback();
            }
            base.WndProc(ref m);
        }

        private void callForPlayback()
        {
            var retrn = playback.play(route, path);
            int curStat = Convert.ToInt32(retrn);
            string statName = files.JSpa(route, "$.statlist[" + curStat + "].name");
            LblCurStat.Text = "Next stop: " + statName;

            int annon = Convert.ToInt32(retrn);
            if (annon == 0)
            {
                LblCurAnon.Text = "Next announcement: Departing from " + statName;
            }
            else if (annon == 1)
            {
                LblCurAnon.Text = "Next announcement: Arriving to " + statName;
            }
            else if (annon == 2)
            {
                LblCurAnon.Text = "Next announcement: We are now at " + statName;
            }
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            callForPlayback();
        }
    }
}
