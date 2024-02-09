using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TSWAnnouncer
{
    public partial class main : Form
    {
        public string SelSoundPck;
        public string SelRoute;
        //public static string jsonprofile = File.ReadAllText(files.GetPathRoot(@"Profiles\Packs\TL.json"));
        //private System.Windows.Forms.OpenFileDialog openFileDialog1;
        // this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
        public main() => InitializeComponent();


        private void button3_Click(object sender, EventArgs e) //Select pack JSON file button
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                Title = "Select TOC soundpack file",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "json",
                Filter = "JSON Files (*.json)|*.json",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = false,
                ShowReadOnly = false
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SelSoundPck = openFileDialog1.FileName;
                LblSoundPckName.Text = "Sound pack name is: " + files.JSpa(SelSoundPck, "$.info.name");
            }

            if (SelSoundPck != null && SelRoute != null)
            {
                ButStart.Enabled = true;
            }
        }

        private void ButSelJSONRoute_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                Title = "Select route config file",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "json",
                Filter = "JSON Files (*.json)|*.json",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = false,
                ShowReadOnly = false
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SelRoute = openFileDialog1.FileName;
                LblSelRoute.Text = "Route pack name: " + files.JSpa(SelRoute, "$.info.name");
                LblFrom.Text = "Starting from: " + files.JSpa(SelRoute, "$.initdata.firstop");
                LblTo.Text = "Terminating at: " + files.JSpa(SelRoute, "$.initdata.lasstop");
            }

            if (SelSoundPck != null && SelRoute != null)
            {
                ButStart.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e) //Start Button
        {
           
        }
    }
}
