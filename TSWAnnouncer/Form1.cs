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
            if (soundPlayed == false)
            {
                soundPlayed = true;
                if (outputDevice == null)
                {
                    outputDevice = new WaveOutEvent();
                    outputDevice.PlaybackStopped += OnPlaybackStopped;
                }
                if (audioFile == null)
                {
                    string path = files.getPath("welcome.mp3");
                    audioFile = new AudioFileReader(path);
                    outputDevice.Init(audioFile);
                }
                outputDevice.Play();
            }
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs args)
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
                var second = new AudioFileReader(files.getPath("GTW.mp3"));

                var playlist = new ConcatenatingSampleProvider(new[] { first, second });
                outputDevice.Init(playlist);

                outputDevice.Play();
            }
        }
    }
}
