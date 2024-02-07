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
            string filepath = @"Packs\TL.json";
            new sounds().addQueue(filepath, files.JSpa(filepath, "$.main[:1].misc[:1].and"));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new sounds().playQueue();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            string filepath = @"Packs\TL.json";
            new sounds().addQueue(filepath, files.JSpa(filepath, "$.main[:1].misc[:1].only"));
        }
    }
}
