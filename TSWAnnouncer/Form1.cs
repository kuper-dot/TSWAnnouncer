using System.Media;

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
            Console.WriteLine("You pressed me");
            label1.Text = "YAYAYAYA";
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();

            player.SoundLocation = "phone.wav";
            player.Play();
        }
    }
}
