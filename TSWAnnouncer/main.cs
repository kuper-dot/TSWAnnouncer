using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TSWAnnouncer
{
    public partial class main : Form
    {
        public static string jsonprofile = File.ReadAllText(files.getPathRoot(@"Profiles\Packs\TL.json"));
        public main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] array2 = new int[5];
            System.Windows.Forms.MessageBox.Show(Convert.ToString(array2[3]));
            Array.Clear(array2);
            System.Windows.Forms.MessageBox.Show(Convert.ToString(array2[3]));
            if (array2[0] == null)
            {
                System.Windows.Forms.MessageBox.Show("Yay");
            }
            new sounds().playQueue();
        }
    }
}
