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

        public static int JSpaCountArr(string filename, string path)
        {
            var token = JToken.Parse(File.ReadAllText(filename));
            var count = token.SelectTokens(path).Count();
            return count;
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

    public class playback
    {
        private WaveOutEvent? outputDevice;
        private AudioFileReader? audioFile;
        private static bool soundPlayed;
        private static List<string> audioqueue = new();
        public static int? curStop;
        public static int? lasStop;

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
                }

                var playlist = new ConcatenatingSampleProvider(queue);
                //Createing a one audio file out of many
                outputDevice.Init(playlist);
                outputDevice.Play();// And then playing it.
                while (outputDevice.PlaybackState != PlaybackState.Stopped) ;
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
        }

        public void AddQueue(string filename) 
        {
            if (audioqueue == null) { List<string> audioqueue = new() { files.GetPathAudio(filename) }; }
            else { audioqueue.Add(files.GetPathAudio(filename)); }
            
        }

        public static void StatCheck(string route)
        {
            if (curStop == null)
            {
                curStop = 0;
            }
            if (lasStop == null)
            {
                lasStop = files.JSpaCountArr(route, "$.statlist[*]");
            }
        }

        public static void Appr(string route, string pack)
        {
            curStop = 1;
            int curSequence = 0;
            playback.StatCheck(route);
            // System.Windows.Forms.MessageBox.Show(Convert.ToString(lasstop));
            var sequenceCount = files.JSpaCountArr(route, "$.statlist[" + curStop + "].seq[*]");
            while (curSequence != sequenceCount)
            {
                string sequenceStr = files.JSpa(route, "$.statlist[" + curStop + "].seq[" + curSequence + "].name");
                if (sequenceStr.Contains("#")) //Going though sequence in route JSON file
                {
                    int curAudio = 0;
                    int totalAudio = files.JSpaCountArr(pack, "$.conf.appr." + sequenceStr + "[*]");
                    while (curAudio < totalAudio) //This is executing any predifined sequnses from soundPack
                    {
                        string audioName = files.JSpa(pack, "$.conf.appr." + sequenceStr + "[" + curAudio + "].name");
                        if (audioName.Contains("!"))
                        {
                            if (audioName == "!curstat")
                            {
                                var statName = files.JSpa(route, "$.statlist[" + curStop + "].name");
                                var folder = files.JSpa(pack, "$.info.foldername");
                                new playback().AddQueue(folder + "\\stations\\low\\" + statName + ".mp3");
                            }
                        }
                        else
                        {
                            var temp = files.JSpa(pack, "$.main.appr." + audioName);
                            new playback().AddQueue(temp);
                        }
                        curAudio++;
                    }
                    new playback().playQueue();
                }
                else if (sequenceStr.Contains("\\")) //Change here for actions
                {
                    Thread.Sleep(1000);
                    var filename = files.JSpa(route, "$.statlist[" + curStop + "].seq[" + curSequence + "].*");
                    new playback().AddQueue(filename);
                    new playback().playQueue();
                }
                curSequence++;
            }
            new playback().playQueue();
        }

    }
}