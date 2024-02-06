using System.IO;
using System.Media;
using System.Numerics;
using System.Reflection;
using System.Security.Policy;
using WMPLib;


namespace TSWAnnouncer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\Audio\\TRTS.wav";
            player.Play();

           /* string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Audio\GTW.mp3");
            WMPLib.WindowsMediaPlayer player = new WindowsMediaPlayer();
            FileInfo fileInfo = new FileInfo(path);
            player.URL = path;
            player.controls.play();*/
        }
    }
}
