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
    }

    public class playback
    {
        private WaveOutEvent? outputDevice;
        private AudioFileReader? audioFile;
        private static bool soundPlayed;
        private static List<string> audioqueue = new();
        public static int? curStop;
        public static int? lasStop;
        public static int? lasAnnon; //0 - dep 1 - shortly 2-atstat
        public static string route;
        public static string pack;

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
                while (outputDevice.PlaybackState != PlaybackState.Stopped)
                {
                    Thread.Sleep(25);
                }
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
            if (lasAnnon == null)
            {
                lasAnnon = 0;
            }
        }

        public static void play(string rt, string pck)
        {
            route = rt;
            pack = pck;
            playback.StatCheck(route);
            if (lasAnnon == 2)
            {
                var skip = files.JSpa(route, "$.skip.dep");
                if (skip == "true") { Appr(); }
                else { Dep(); }
            }
            else if (lasAnnon == 0)
            {
                var skip = files.JSpa(route, "$.skip.arr");
                if (skip == "true") { atStat(); }
                else { Appr(); }
            }
            else if (lasAnnon == 1)
            {
                var skip = files.JSpa(route, "$.skip.at");
                if (skip == "true") { Dep(); }
                else { atStat(); }
            }
        }
        public static void Appr()
        {
            int curSequence = 0;
            var packFolder = files.JSpa(pack, "$.info.foldername");
            
            //System.Windows.Forms.MessageBox.Show(Convert.ToString(curStop));
            var sequenceCount = files.JSpaCountArr(route, "$.statlist[" + curStop + "].arrseq[*]");
            if (sequenceCount == 0)
            {
                lasAnnon = 1;
                return;
            }
            while (curSequence != sequenceCount)
            {
                int sleep = 1000;
                string sequenceStr = files.JSpa(route, "$.statlist[" + curStop + "].arrseq[" + curSequence + "].name");
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
                                new playback().AddQueue(packFolder + "\\stations\\low\\" + statName + ".mp3");
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
                    var fileName = files.JSpa(pack, "$.main.appr.change");
                    new playback().AddQueue(fileName);
                    fileName = files.JSpa(route, "$.statlist[" + curStop + "].arrseq[" + curSequence + "].*");
                    new playback().AddQueue(fileName);
                    new playback().playQueue();
                }   
                else if (sequenceStr.Contains("!"))
                {
                    sequenceStr = sequenceStr.Remove(0, 1);
                    var fileName = files.JSpa(pack, "$.main.appr."+sequenceStr);
                    new playback().AddQueue(fileName);
                    new playback().playQueue();
                }
                if (curSequence + 1 < sequenceCount)
                {
                    int nxtSequence = curSequence + 1;
                    string nextSqeStr = files.JSpa(route, "$.statlist[" + curStop + "].arrseq[" + nxtSequence + "].name");
                    if (nextSqeStr.Contains("@"))
                    {
                        nextSqeStr = nextSqeStr.Remove(0, 1);
                        sleep = Convert.ToInt32(nextSqeStr) * 1000;
                        curSequence++;
                    }
                }
                Thread.Sleep(sleep);
                curSequence++;
            }
            lasAnnon = 1;
        }


        public static void atStat()
        {
            int curSequence = 0;
            var packFolder = files.JSpa(pack, "$.info.foldername");

            //System.Windows.Forms.MessageBox.Show(Convert.ToString(curStop));
            var sequenceCount = files.JSpaCountArr(route, "$.statlist[" + curStop + "].atseq[*]");
            if (sequenceCount == 0)
            {
                lasAnnon = 2;
                curStop++;
                return;
            }
            while (curSequence != sequenceCount)
            {
                int sleep = 1000;
                string sequenceStr = files.JSpa(route, "$.statlist[" + curStop + "].atseq[" + curSequence + "].name");
                if (sequenceStr.Contains("#")) //Going though sequence in route JSON file
                {
                    int curAudio = 0;
                    int totalAudio = files.JSpaCountArr(pack, "$.conf.atStat." + sequenceStr + "[*]");
                    while (curAudio < totalAudio) //This is executing any predifined sequnses from soundPack
                    {
                        string audioName = files.JSpa(pack, "$.conf.atStat." + sequenceStr + "[" + curAudio + "].name");
                        if (audioName.Contains("!"))
                        {
                            if (audioName == "!curstat")
                            {
                                var statName = files.JSpa(route, "$.statlist[" + curStop + "].name");
                                new playback().AddQueue(packFolder + "\\stations\\low\\" + statName + ".mp3");
                            }
                            else if (audioName == "!lasstop")
                            {
                                var statName = files.JSpa(route, "$.initdata.lasstop");
                                new playback().AddQueue(packFolder + "\\stations\\low\\" + statName + ".mp3");
                            }
                            else if (audioName == "!calling")
                            {
                                for(int seq = Convert.ToInt32(curStop) + 1; seq != lasStop; seq++)
                                {
                                    var statName = files.JSpa(route, "$.statlist[" + seq + "].name");
                                    if (seq == lasStop - 1)
                                    {
                                        var temp = files.JSpa(pack, "$.main.misc.and");
                                        new playback().AddQueue(temp);
                                        new playback().AddQueue(packFolder + "\\stations\\low\\" + statName + ".mp3");
                                    }
                                    else { new playback().AddQueue(packFolder + "\\stations\\high\\" + statName + ".mp3"); }
                                   
                                }
                            }
                        }
                        else //Conplete two other ! func
                        {
                            var temp = files.JSpa(pack, "$.main.atStat." + audioName);
                            new playback().AddQueue(temp);
                        }
                        curAudio++;
                    }
                    new playback().playQueue();
                }
                else if (sequenceStr.Contains("\\")) //Change here for actions
                {
                    var fileName = files.JSpa(pack, "$.main.atStat.change");
                    new playback().AddQueue(fileName);
                    fileName = files.JSpa(route, "$.statlist[" + curStop + "].arrseq[" + curSequence + "].*");
                    new playback().AddQueue(fileName);
                    new playback().playQueue();
                }
                else if (sequenceStr.Contains("!"))
                {
                    sequenceStr = sequenceStr.Remove(0, 1);
                    var fileName = files.JSpa(pack, "$.main.atStat." + sequenceStr);
                    new playback().AddQueue(fileName);
                    new playback().playQueue();
                }
                else if (sequenceStr.Contains("&"))
                {
                    if (sequenceStr.Contains("low"))
                    {
                        sequenceStr = sequenceStr.Substring(sequenceStr.IndexOf('&') + 1);
                        new playback().AddQueue(packFolder + "\\stations\\low\\" + sequenceStr + ".mp3");
                        new playback().playQueue();
                    }
                }
                if (curSequence + 1 < sequenceCount)
                {
                    int nxtSequence = curSequence + 1;
                    string nextSqeStr = files.JSpa(route, "$.statlist[" + curStop + "].atseq[" + nxtSequence + "].name");
                    if (nextSqeStr.Contains("@"))
                    {
                        nextSqeStr = nextSqeStr.Remove(0, 1);
                        sleep = Convert.ToInt32(nextSqeStr) * 1000;
                        curSequence++;
                    }
                }
                Thread.Sleep(sleep);
                curSequence++;
            }
            lasAnnon = 2;
            curStop++;
        }
        public static void Dep()
        {
            int curSequence = 0;
            var packFolder = files.JSpa(pack, "$.info.foldername");

            //System.Windows.Forms.MessageBox.Show(Convert.ToString(curStop));
            var sequenceCount = files.JSpaCountArr(route, "$.statlist[" + curStop + "].depseq[*]");
            if (sequenceCount == 0)
            {
                lasAnnon = 0;
                return;
            }
            while (curSequence != sequenceCount)
            {
                // string sequenceStr = files.JSpa(route, "$.statlist[" + curStop + "].arrseq[" + curSequence + "].name");
            }
        }
    }
}
