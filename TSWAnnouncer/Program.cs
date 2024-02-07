using System.IO;
using System.Text;
using System.Reflection;
using System.Data;
using Newtonsoft.Json;
using NAudio;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using Newtonsoft.Json.Linq;

namespace TSWAnnouncer
{
    internal static class Program
    {
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new main());
        }
    }
    public class files
    {
        public static string getPath(string FileNameToFetch)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Audio\" + FileNameToFetch);
            FileInfo fileInfo = new FileInfo(path);
            return path;
        }

        public static string getPathRoot(string FileNameToFetch)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), FileNameToFetch);
            FileInfo fileInfo = new FileInfo(path);
            return path;
        }

        public static string JSpa(string filename, string path)
        {
            JObject o = JObject.Parse(File.ReadAllText(files.getPathRoot(@"Profiles\" + filename)));
            JToken acme = o.SelectToken(path);
            return Convert.ToString(acme);
        }
    }

    public class sounds
    {
        private WaveOutEvent? outputDevice;
        private AudioFileReader? audioFile;
        private bool soundPlayed;
        private static List<string> audioqueue = new List<string>();
        private List<AudioFileReader> queue;

        public void OnPlaybackStopped(object sender, StoppedEventArgs args) //Actions to do when files are played
        {
            outputDevice.Dispose();
            outputDevice = null;
            if (audioFile != null)
            {
                audioFile.Dispose();
                audioFile = null;
            }
            soundPlayed = false;
            //queue.Clear();

        }

        public void playQueue()
        {

            if (soundPlayed == false)
            {
                soundPlayed = true;
                if (outputDevice == null)
                {
                    outputDevice = new WaveOutEvent();
                    outputDevice.PlaybackStopped += OnPlaybackStopped;
                }

                List<AudioFileReader> audio = new List<AudioFileReader>();
                foreach (string i in audioqueue)
                {
                    audio.Add(new AudioFileReader(i));
                }
                var playlist = new ConcatenatingSampleProvider(audio);
                //Createing a one audio file out of many
                outputDevice.Init(playlist);
                outputDevice.Play();// And then playing it.
            }
        }

        public void addQueue(string filepath, string filename) //TO DO APPEND TO QUEUE ARRAY
        {
            //var audio = new AudioFileReader(files.getPath(filename));
            // if (queue == null) { List<AudioFileReader> queue = new List<AudioFileReader> { audio }; }
            // else { queue.Add(audio); }
            if (audioqueue == null) { List<string> audioqueue = new List<string>() { files.getPath(filename) }; }
            else { audioqueue.Add(files.getPath(filename)); }
            
        }
    }
}