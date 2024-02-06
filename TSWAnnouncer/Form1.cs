using NAudio.Wave;
using NAudio.Wave.SampleProviders;
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
        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;
        private bool soundPlayed = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (soundPlayed == false) //Is sound playing now?
            {
                soundPlayed = true; //Now it is playing
                if (outputDevice == null) //If there is no player - then create one
                {
                    outputDevice = new WaveOutEvent();
                    outputDevice.PlaybackStopped += OnPlaybackStopped;
                }
                if (audioFile == null) //Same with audio source
                {
                    string path = files.getPath("welcome.mp3");
                    audioFile = new AudioFileReader(path);
                    outputDevice.Init(audioFile);
                }
                outputDevice.Play();
            }
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs args) //Actions to do when files are played
        {
            outputDevice.Dispose();
            outputDevice = null;
            if (audioFile != null)
            {
                audioFile.Dispose();
                audioFile = null;
            }
            soundPlayed = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (soundPlayed == false)
            {
                soundPlayed = true;
                if (outputDevice == null)
                {
                    outputDevice = new WaveOutEvent();
                    outputDevice.PlaybackStopped += OnPlaybackStopped;
                }
                var first = new AudioFileReader(files.getPath("welcome.mp3"));
                var second = new AudioFileReader(files.getPath(null));

                var playlist = new ConcatenatingSampleProvider(new[] { first, second }); //Createing a one audio file out of many
                outputDevice.Init(playlist);

                outputDevice.Play(); // And then playing it.
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
