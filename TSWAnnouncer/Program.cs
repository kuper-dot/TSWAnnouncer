using NAudio;
using NAudio.Wave;
using System.IO;
using System.Reflection;


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

    /* public class playlist
     {   
         public ISampleProvider makeGpPlaylistVar(Queue<string> playlist, AudioFileReader followed)
         {
             if (playlist.FirstOrDefault<string>() == null) { return followed.ToSampleProvider(); }
             AudioFileReader first = new AudioFileReader(playlist.First());
             if (followed == null)
             {
                 playlist.Dequeue();
                 return MergeSoundfiles(playlist, first);
             }
             if (playlist.Count > 1)
             {
                 playlist.Dequeue();
                 return followed.FollowedBy(MergeSoundfiles(playlist, first));
             }
             return followed.FollowedBy(first);
         }
     }*/
    public class files
    {
        public static string getPath(string FileNameToFetch) {
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Audio\" + FileNameToFetch);
        FileInfo fileInfo = new FileInfo(path);
        return path;
        }

       /* public ISampleProvider makeGpPlaylistVar(Queue<string> playlist, AudioFileReader followed)
        {
            if (playlist.FirstOrDefault<string>() == null) { return followed.ToSampleProvider(); }
            AudioFileReader first = new AudioFileReader(playlist.First());
            if (followed == null)
            {
                playlist.Dequeue();
                return Combine(playlist, first);
            }
            if (playlist.Count > 1)
            {
                playlist.Dequeue();
                return followed.FollowedBy(MergeSoundfiles(playlist, first));
            }
            return followed.FollowedBy(first);
        }*/
    }
}