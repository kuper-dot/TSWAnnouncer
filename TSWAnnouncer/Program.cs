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
        [STAThread]
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
        public static string GetPathAudio(string FileNameToFetch)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Audio\" + FileNameToFetch);
            FileInfo fileInfo = new FileInfo(path);
            return path;
        }

        public static string GetPathRoot(string FileNameToFetch)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), FileNameToFetch);
            FileInfo fileInfo = new FileInfo(path);
            return path;
        }

        public static string JSpa(string filename, string path)
        {
            JObject o = JObject.Parse(File.ReadAllText(filename));
            JToken acme = o.SelectToken(path);
            return Convert.ToString(acme);
        }
        public static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
    }

    public class sounds
    {
        private WaveOutEvent? outputDevice;
        private AudioFileReader? audioFile;
        private bool soundPlayed;
        private static List<string> audioqueue = new();

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
            audioqueue.Clear();

        }

        public void playQueue()
        {

            if (soundPlayed == false)
            {
                List<AudioFileReader> queue = new();
                foreach (string i in audioqueue)
                {
                    var audio = new AudioFileReader(i);
                    queue.Add(audio);
                }
                if (queue.Count == 0) { return; }
                soundPlayed = true;
                if (outputDevice == null)
                {
                    outputDevice = new WaveOutEvent();
                    outputDevice.PlaybackStopped += OnPlaybackStopped;
                }

                var playlist = new ConcatenatingSampleProvider(queue);
                //Createing a one audio file out of many
                outputDevice.Init(playlist);
                outputDevice.Play();// And then playing it.
            }
        }

        public void AddQueue(string filepath, string jsonpath) 
        {
            var filename = files.JSpa(filepath, jsonpath);
            if (audioqueue == null) { List<string> audioqueue = new() { files.GetPathAudio(filename) }; }
            else { audioqueue.Add(files.GetPathAudio(filename)); }
            
        }
    }

    public class playback
    {
        public static void first(string CurStop, string LastStop)
        {

        }
    }
}