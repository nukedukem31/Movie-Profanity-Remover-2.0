using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace Movie_Profanity_Remover_2._0
{
    public static class Tool
    {
        public static Settings Settings;

        public static List<string> SearchDirectory(string dir, bool subdirectories)
        {
            List<string> files = new List<string>();
            try
            {
                foreach (string file in Directory.GetFiles(dir))
                    files.Add(file);
                if (subdirectories)
                    foreach (string directory in Directory.GetDirectories(dir))
                        files.AddRange(SearchDirectory(directory, subdirectories));
            }
            catch { }

            return files;
        }

        public static void ExtractToDirectory(byte[] source, string destinationDirectory, bool overwrite)
        {
            string Temp = Path.GetTempPath() + Path.GetRandomFileName() + ".zip";
            File.WriteAllBytes(Temp, source);

            ZipArchive archive = new ZipArchive(new FileStream(Temp, FileMode.Open));


            foreach (ZipArchiveEntry file in archive.Entries)
            {
                string completeFileName = Path.Combine(destinationDirectory, file.FullName);
                if (overwrite || !File.Exists(completeFileName))
                {
                    string directory = Path.GetDirectoryName(completeFileName);

                    if (!Directory.Exists(directory))
                        Directory.CreateDirectory(directory);

                    if (file.Name != "")
                        file.ExtractToFile(completeFileName, true);
                }
            }

            try
            {
                File.Delete(Temp);
            }
            catch { }
        }

        public static void LoadSettings()
        {
            if (File.Exists(Info.Settings))
                Settings = (Settings)Data.Load(Info.Settings);
            else
                Settings = new Settings();
        }

        public static void SaveSettings()
        {
            Data.Save(Info.Settings, Settings);
        }

        public static void CreateFFMPEG()
        {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ffmpeg.exe"))
                ExtractToDirectory(Properties.Resources.ffmpeg, AppDomain.CurrentDomain.BaseDirectory, true);
        }

        public static void RemoveReadOnlyProperty(string location)
        {
            FileInfo fi = new FileInfo(location);

            if (fi.Exists && fi.Attributes.HasFlag(FileAttributes.ReadOnly))
                fi.Attributes = FileAttributes.Normal;
        }

        public static KeyValuePair<TimeSpan, TimeSpan> GetSubtitlesTimeSpan(string timestamp)
        {
            int sHours = int.Parse(timestamp.Substring(0, 2));
            int sMin = int.Parse(timestamp.Substring(3, 2));
            int sSec = int.Parse(timestamp.Substring(6, 2));
            int sMillisec = int.Parse(timestamp.Substring(9, 3));

            int eHours = int.Parse(timestamp.Substring(17, 2));
            int eMin = int.Parse(timestamp.Substring(20, 2));
            int eSec = int.Parse(timestamp.Substring(23, 2));
            int eMillisec = int.Parse(timestamp.Substring(26, 3));

            TimeSpan startTimeSpan = new TimeSpan(0, sHours, sMin, sSec, sMillisec);
            TimeSpan endTimeSpan = new TimeSpan(0, eHours, eMin, eSec, eMillisec);

            return new KeyValuePair<TimeSpan, TimeSpan>(startTimeSpan, endTimeSpan);
        }

        public static async void DelayDelete(string path, int delay)
        {
            await Task.Delay(delay);

            try
            {
                File.Delete(path);
            }
            catch { }
        }
    }
}
