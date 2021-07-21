using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Movie_Profanity_Remover_2._0
{
    public static class Subtitles
    {
        public static List<Subtitle> GetIntervals(Video video)
        {
            return Locations(video);
        }

        public static List<string> Read(string path)
        {
            List<string> lstSubtitles = new List<string>();

            using (FileStream fs = new FileStream(path, FileMode.Open))
            using (StreamReader sr = new StreamReader(fs))
                while (sr.Peek() != -1)
                    lstSubtitles.Add(sr.ReadLine());

            return lstSubtitles;
        }

        private static List<Subtitle> Locations(Video video)
        {
            List<Subtitle> subtitles = new List<Subtitle>();

            Subtitle subtitle = new Subtitle();

            List<string> SubtitlesOriginal = video.SubtitlesOriginal;

            for (int i = 0; i < SubtitlesOriginal.Count; i++)
            {
                string line = SubtitlesOriginal[i];

                if (TimeStamp(line))
                {
                    string[] Lines = subtitle.Text.Split('\n');
                    Lines = Lines.Take(Lines.Length - 2).ToArray();
                    subtitle.Text = string.Join("\n", Lines).Replace("\r", "");

                    if (subtitle.Text != null && subtitle.Text != "")
                        subtitles.Add(subtitle);

                    subtitle = new Subtitle();

                    string start = line.Substring(0, 12).Replace(",", ".");
                    string end = line.Substring(17, 12).Replace(",", ".");

                    subtitle.Start = TimeSpan.Parse(start, CultureInfo.InvariantCulture);
                    subtitle.End = TimeSpan.Parse(end, CultureInfo.InvariantCulture);
                }
                else if (line != "")
                {
                    subtitle.Text += line + Environment.NewLine;
                }
            }

            video.Subtitles = subtitles;

            for (int i = 0; i < video.Subtitles.Count; i++)
            {
                foreach (string word in video.SwearWordsFull)
                {
                    KeyValuePair<int, int> Indexes = SwearWord(video.Subtitles[i].Text, word);

                    if (Indexes.Key != -1 && Indexes.Value != -1)
                    {
                        video.Subtitles[i].Remove.Add(new KeyValuePair<TimeSpan, TimeSpan>(video.Subtitles[i].Start, video.Subtitles[i].End));
                        video.Subtitles[i].RemoveFlag = true;
                        break;
                    }
                }

                foreach (string word in video.SwearWordsSingle)
                {
                    KeyValuePair<int, int> Indexes = SwearWord(video.Subtitles[i].Text, word);

                    if (Indexes.Key != -1 && Indexes.Value != -1)
                    {
                        video.Subtitles[i].RemoveFlag = true;
                        var remove = video.Subtitles[i].Remove;
                        remove.AddRange(SwearWordIntervals(video.Subtitles[i], word, video.SingleWordBefore, video.SingleWordAfter));

                        int RemoveCount = remove.Count - 1;

                        for (int j = 0; j < RemoveCount; j++)
                        {
                            if (remove[j].Value > remove[j + 1].Key)
                            {
                                remove[j] = new KeyValuePair<TimeSpan, TimeSpan>(remove[j].Key, remove[j + 1].Value);
                                remove.RemoveAt(j + 1);
                                j--;
                                RemoveCount--;
                            }
                        }

                        video.Subtitles[i].Remove = remove;
                    }
                }
            }

            return video.Subtitles;
        }

        private static bool TimeStamp(string line)
        {
            try
            {
                if (line.Length != 29)
                    return false;

                int.Parse(line.Substring(0, 2));
                int.Parse(line.Substring(3, 2));
                int.Parse(line.Substring(6, 2));
                int.Parse(line.Substring(9, 3));

                int.Parse(line.Substring(17, 2));
                int.Parse(line.Substring(20, 2));
                int.Parse(line.Substring(23, 2));
                int.Parse(line.Substring(26, 3));

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static List<KeyValuePair<TimeSpan, TimeSpan>> SwearWordIntervals(Subtitle subtitle, string word, int beforeMS, int afterMS)
        {
            List<KeyValuePair<TimeSpan, TimeSpan>> intervals = new List<KeyValuePair<TimeSpan, TimeSpan>>();
            List<int> indexes = AllIndexesOf(subtitle.Text.ToLower(), word);

            KeyValuePair<TimeSpan, TimeSpan> timespan = new KeyValuePair<TimeSpan, TimeSpan>(subtitle.Start, subtitle.End);
            double totalMS = (timespan.Value - timespan.Key).TotalMilliseconds;

            for (int i = 0; i < indexes.Count; i++)
            {
                int current = indexes[i] + (word.Length / 2);
                int timeframe = (int)((double)current / subtitle.Text.Length * totalMS);

                TimeSpan start = TimeSpan.FromMilliseconds(timespan.Key.TotalMilliseconds + timeframe - beforeMS);
                TimeSpan end = start + TimeSpan.FromMilliseconds(beforeMS + afterMS);

                intervals.Add(new KeyValuePair<TimeSpan, TimeSpan>(start, end));
            }

            return intervals;
        }

        public static List<int> AllIndexesOf(string line, string value)
        {
            List<int> indexes = new List<int>();
            for (int index = 0; ; index += value.Length)
            {
                index = line.IndexOf(value, index);
                if (index == -1)
                    return indexes;
                indexes.Add(index);
            }
        }


        public static KeyValuePair<int, int> SwearWord(string Lines, string Word)
        {
            int begin = -1;
            int end = -1;

            string alphabet = "abcdefghijklmnopqrstuvwxyz";

            if (Lines.ToLower().Contains(Word))
            {
                if (Lines.Length > Word.Length)
                {
                    begin = Lines.ToLower().IndexOf(Word);
                    end = begin + (Word.Length - 1);

                    if (begin > 0 && alphabet.Contains(Lines.ToLower()[begin - 1]))
                        return new KeyValuePair<int, int>(-1, -1);

                    if (end < Lines.Length - 1 && alphabet.Contains(Lines.ToLower()[end + 1]))
                        return new KeyValuePair<int, int>(-1, -1);

                    return new KeyValuePair<int, int>(begin, end);
                }
                else
                {
                    begin = 0;
                    end = Word.Length - 1;
                    return new KeyValuePair<int, int>(begin, end);
                }
            }
            else
                return new KeyValuePair<int, int>(-1, -1);
        }

        public static KeyValuePair<int, int> SwearWord(List<string> Subtitles, int Index, string Word)
        {
            int begin = -1;
            int end = -1;

            string alphabet = "abcdefghijklmnopqrstuvwxyz";

            if (Subtitles[Index].ToLower().Contains(Word))
            {
                if (Subtitles[Index].Length > Word.Length)
                {
                    begin = Subtitles[Index].ToLower().IndexOf(Word);
                    end = begin + (Word.Length - 1);

                    if (begin > 0 && alphabet.Contains(Subtitles[Index].ToLower()[begin - 1]))
                        return new KeyValuePair<int, int>(-1,-1);

                    if (end < Subtitles[Index].Length - 1 && alphabet.Contains(Subtitles[Index].ToLower()[end + 1]))
                        return new KeyValuePair<int, int>(-1, -1);

                    return new KeyValuePair<int, int>(begin, end);
                }
                else
                {
                    begin = 0;
                    end = Word.Length - 1;
                    return new KeyValuePair<int, int>(begin, end);
                }
            }
            else
                return new KeyValuePair<int, int>(-1, -1);
        }


        public static void Create(Video video, bool embed, SubtitlesType subtitlesType)
        {
            if (subtitlesType == SubtitlesType.Normal)
                video.SubtitlesPathNewNormal = GenerateName(video.SubtitlesPathOriginal, embed, SubtitlesType.Normal);

            if (subtitlesType == SubtitlesType.Exclusive)
                video.SubtitlesPathNewExlusive = GenerateName(video.SubtitlesPathOriginal, embed, SubtitlesType.Exclusive);

            if (subtitlesType == SubtitlesType.Both)
            {
                video.SubtitlesPathNewNormal = GenerateName(video.SubtitlesPathOriginal, embed, SubtitlesType.Both, true);
                video.SubtitlesPathNewExlusive = GenerateName(video.SubtitlesPathOriginal, embed, SubtitlesType.Both, false);
            }

            ReplaceWords(video);
            SaveSubtitles(video, subtitlesType);
        }

        private static string GenerateName(string subtitle, bool embed, SubtitlesType subtitlesType, bool normal = false)
        {
            FileInfo fi = new FileInfo(subtitle);
            string _new = "";

            if (subtitlesType == SubtitlesType.Normal)
            {
                _new = subtitle.Substring(0, subtitle.Length - fi.Extension.Length) + Tool.Settings.CustomAffix + ".srt";
            }
            else if (subtitlesType == SubtitlesType.Exclusive)
            {
                _new = subtitle.Substring(0, subtitle.Length - fi.Extension.Length) + Tool.Settings.CustomAffix + ".srt";
            }
            else if (subtitlesType == SubtitlesType.Both && normal)
            {
                _new = subtitle.Substring(0, subtitle.Length - fi.Extension.Length) + "_Normal" + Tool.Settings.CustomAffix + ".srt";
            }
            else if (subtitlesType == SubtitlesType.Both && !normal)
            {
                _new = subtitle.Substring(0, subtitle.Length - fi.Extension.Length) + "_Exlusive" + Tool.Settings.CustomAffix + ".srt";
            }

            if (embed)
                _new = Path.GetTempPath() + new FileInfo(_new).Name;

            return _new;
        }

        private static void ReplaceWords(Video video)
        {
            video.SubtitlesNew = new List<Subtitle>();
            //List<string> Lines = new List<string>();

            for (int i = 0; i < video.Subtitles.Count; i++)
            {
                Subtitle subtitle = video.Subtitles[i];
                //Lines.Add(sentence);

                for (int swear = 0; swear < video.SwearWordsFull.Count; swear++)
                {
                    KeyValuePair<int, int> Indexes = SwearWord(subtitle.Text, video.SwearWordsFull[swear]);
                    int begin = Indexes.Key;
                    int end = Indexes.Value;

                    while (begin != -1 && end != -1)
                    {
                        //Lines.RemoveAt(Lines.Count - 1);

                        string censoredChar = "";

                        foreach (char c in video.SwearWordsFull[swear])
                            censoredChar += "*";

                        subtitle.Text = subtitle.Text.Substring(0, begin) + censoredChar + subtitle.Text.Substring(end + 1);
                        //Lines.Add(sentence);

                        Indexes = SwearWord(subtitle.Text, video.SwearWordsFull[swear]);
                        begin = Indexes.Key;
                        end = Indexes.Value;
                    }
                }

                for (int swear = 0; swear < video.SwearWordsSingle.Count; swear++)
                {
                    KeyValuePair<int, int> Indexes = SwearWord(subtitle.Text, video.SwearWordsSingle[swear]);
                    int begin = Indexes.Key;
                    int end = Indexes.Value;
                    while (begin != -1 && end != -1)
                    {
                        //Lines.RemoveAt(Lines.Count - 1);

                        string censoredChar = "";

                        foreach (char c in video.SwearWordsSingle[swear])
                            censoredChar += "*";

                        subtitle.Text = subtitle.Text.Substring(0, begin) + censoredChar + subtitle.Text.Substring(end + 1);
                        //Lines.Add(sentence);

                        Indexes = SwearWord(subtitle.Text, video.SwearWordsSingle[swear]);
                        begin = Indexes.Key;
                        end = Indexes.Value;
                    }
                }

                video.SubtitlesNew.Add(subtitle);
            }
        }

        private static void SaveSubtitles(Video video, SubtitlesType subtitlesType)
        {
            //Add try catch for open subtitles
            if ((subtitlesType == SubtitlesType.Normal || subtitlesType == SubtitlesType.Both) && File.Exists(video.SubtitlesPathNewNormal))
                File.Delete(video.SubtitlesPathNewNormal);

            if ((subtitlesType == SubtitlesType.Exclusive || subtitlesType == SubtitlesType.Both) && File.Exists(video.SubtitlesPathNewExlusive))
                File.Delete(video.SubtitlesPathNewExlusive);

            List<string> Subs;

            if (subtitlesType == SubtitlesType.Normal || subtitlesType == SubtitlesType.Both)
            {
                Subs = new List<string>();

                for (int i = 0; i < video.SubtitlesNew.Count; i++)
                {
                    Subs.Add((i + 1).ToString());
                    Subs.Add(GetTimeStamp(video.SubtitlesNew[i].Start, video.SubtitlesNew[i].End));
                    Subs.AddRange(video.SubtitlesNew[i].Text.Split('\n'));
                    Subs.Add("");
                }

                File.WriteAllLines(video.SubtitlesPathNewNormal, Subs);
            }

            if (subtitlesType == SubtitlesType.Exclusive || subtitlesType == SubtitlesType.Both)
            {
                Subs = new List<string>();

                //int Count = 1;


                for (int i = 0; i < video.SubtitlesNew.Count; i++)
                {
                    if (!video.SubtitlesNew[i].RemoveFlag)
                    {
                        Subs.Add((i + 1).ToString());
                        Subs.Add(GetTimeStamp(video.SubtitlesNew[i].Start, video.SubtitlesNew[i].End));
                        Subs.Add(" ");
                        Subs.Add("");
                    }
                    else
                    {
                        Subs.Add((i + 1).ToString());
                        Subs.Add(GetTimeStamp(video.SubtitlesNew[i].Start, video.SubtitlesNew[i].End));
                        Subs.AddRange(video.SubtitlesNew[i].Text.Split('\n'));
                        Subs.Add("");
                    }
                }

                File.WriteAllLines(video.SubtitlesPathNewExlusive, Subs);
            }
        }

        static string GetTimeStamp(TimeSpan start, TimeSpan end)
        {
            string time = start.Hours.ToString("00") + ":" + start.Minutes.ToString("00") + ":" + start.Seconds.ToString("00") + "," + start.Milliseconds.ToString("000");
            time += " --> ";
            time += end.Hours.ToString("00") + ":" + end.Minutes.ToString("00") + ":" + end.Seconds.ToString("00") + "," + end.Milliseconds.ToString("000");

            return time;
        }
    }
}
