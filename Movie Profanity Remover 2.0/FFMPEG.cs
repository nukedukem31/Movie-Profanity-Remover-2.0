using FFmpeg.NET;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Movie_Profanity_Remover_2._0
{
    public static class FFMPEG
    {
        public static Engine engine;

        public static CancellationTokenSource tokenSource;

        public static void Mute(Video video, bool embed, SubtitlesType subtitlesType, bool deleteOriginalFiles, bool muteVocalsOnly)
        {
            Prepare(video);

            bool ContainsSwearWords = video.Subtitles.Where(s => s.RemoveFlag == true).Count() > 0;

            video.Output = video.Input.Substring(0, video.Input.Length - new FileInfo(video.Input).Extension.Length) + Tool.Settings.CustomAffix + "." + Tool.Settings.OutputType;

            if (ContainsSwearWords && !tokenSource.Token.IsCancellationRequested)
                GetSurroundStream(video);

            if (ContainsSwearWords && muteVocalsOnly && video.stream != null)
            {
                if (!tokenSource.Token.IsCancellationRequested)
                    ExtractChannels(video);

                if (!tokenSource.Token.IsCancellationRequested)
                    MuteChannels(video);

                if (!tokenSource.Token.IsCancellationRequested)
                    MergeChannels(video);

                if (!tokenSource.Token.IsCancellationRequested)
                    GenerateArgumentsAudio(video, embed, subtitlesType);
            }
            else if (!tokenSource.Token.IsCancellationRequested)
                GenerateArgumentsNormal(video, embed, ContainsSwearWords, subtitlesType);

            if (!tokenSource.Token.IsCancellationRequested)
                DeleteIfExist(video.Output);

            if (!tokenSource.Token.IsCancellationRequested)
                Execute(video);

            Cleanup(video, embed, subtitlesType, deleteOriginalFiles);
        }

        private static void Prepare(Video video)
        {
            video.Arguments = string.Empty;

            video.TotalDuration = GetTotalDuration(video);
            currentVideo = video;
        }

        private static void GetSurroundStream(Video video)
        {
            try
            {
                Tool.CreateFFMPEG();

                Process proc = new Process();
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.FileName = "ffmpeg";
                proc.StartInfo.Arguments = "-i \"" + video.Input + "\"";
                proc = Process.Start(proc.StartInfo);

                string[] output = proc.StandardError.ReadToEnd().ToLower().Split(new string[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);

                proc.WaitForExit();

                string streamIndex = "";
                string channel = "";

                for (int i = 0; i < output.Length; i++)
                {
                    string line = output[i].Trim();

                    if (line.StartsWith("stream #") && line.Contains(": audio:"))
                    {
                        line = line.Substring("stream #".Length);
                        streamIndex = line.Substring(0, 3).Replace(":", ".");

                        line = line.Substring(line.IndexOf(',') + 1);
                        line = line.Substring(line.IndexOf(',') + 1).TrimStart();
                        channel = line.Substring(0, line.IndexOf(",")).Replace(" ", "");

                        break;
                    }
                }

                if (string.IsNullOrEmpty(streamIndex) || string.IsNullOrEmpty(channel))
                {
                    video.stream = null;
                    return;
                }

                proc.StartInfo.Arguments = "-i \"" + video.Input + "\" -layouts";
                proc = Process.Start(proc.StartInfo);

                output = proc.StandardOutput.ReadToEnd().ToLower().Split(new string[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);

                proc.WaitForExit();

                for (int i = 0; i < output.Length; i++)
                {
                    string line = output[i].Trim();

                    string[] split = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                    if (split[0] == channel)
                    {
                        video.stream = new Stream();
                        video.stream.Channel = channel;
                        video.stream.Index = streamIndex;
                        video.stream.Channels = split[1].Split(new string[] { "+" }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.ToUpperInvariant()).ToList();

                        if (video.stream.Channels.Contains("FC") || video.stream.Channels.Contains("SL") || video.stream.Channels.Contains("SR"))
                            return;
                        else
                        {
                            video.stream = null;
                            return;
                        }
                    }
                }
            }
            catch { }

            video.stream = null;
        }


        private static void ExtractChannels(Video video)
        {
            try
            {
                Info.CurrentEngineStatus = CurrentEngineStatus.ExtractingChannels;

                Tool.CreateFFMPEG();

                string Arguments = "-i \"" + video.Input + "\"";

                for (int i = 0; i < video.stream.Channels.Count; i++)
                    Arguments += " -map_channel " + video.stream.Index + "." + i + " -acodec ac3 \"" + Path.ChangeExtension(video.Input, "") + video.stream.Channels[i] + ".wav\"";

                Task taskExecute = engine.ExecuteAsync(Arguments, tokenSource.Token);
                taskExecute.Wait();
            }
            catch { }
        }

        private static void MuteChannels(Video video)
        {
            try
            {
                Info.CurrentEngineStatus = CurrentEngineStatus.MutingChannels;

                Tool.CreateFFMPEG();

                string Intervals = GetIntervalArguments(video);

                for (int i = 0; i < video.stream.Channels.Count; i++)
                {
                    if (new string[] { "FC", "SL", "SR" }.Contains(video.stream.Channels[i]))
                    {
                        string Original = Path.ChangeExtension(video.Input, "") + video.stream.Channels[i] + ".wav";
                        string New = Path.ChangeExtension(video.Input, "") + video.stream.Channels[i] + "_.wav";

                        string Arguments = "-i \"" + Original + "\"";

                        Arguments += " -af \"";

                        Arguments += Intervals;

                        Arguments += " -acodec ac3 \"" + New + "\"";

                        Task taskExecute = engine.ExecuteAsync(Arguments, tokenSource.Token);
                        taskExecute.Wait();

                        File.Delete(Original);
                        File.Move(New, Original);
                    }
                }
            }
            catch { }
        }

        private static void MergeChannels(Video video)
        {
            try
            {
                Info.CurrentEngineStatus = CurrentEngineStatus.MergingChannels;

                Tool.CreateFFMPEG();

                string Arguments = "";

                for (int i = 0; i < video.stream.Channels.Count; i++)
                {
                    string path = Path.ChangeExtension(video.Input, "") + video.stream.Channels[i] + ".wav";
                    Arguments += " -i \"" + path + "\"";
                }

                Arguments += " -vcodec copy -filter_complex \"";

                for (int i = 0; i < video.stream.Channels.Count; i++)
                    Arguments += "[" + i + ":a]";

                Arguments += "join=inputs=" + video.stream.Channels.Count + ":channel_layout=" + video.stream.Channel + ":map=";

                for (int i = 0; i < video.stream.Channels.Count; i++)
                    Arguments += i + ".0-" + video.stream.Channels[i] + "|";

                Arguments = Arguments.Substring(0, Arguments.Length - 1);

                Arguments += "[a]\" -map \"[a]\" -acodec ac3";

                Arguments += " \"" + Path.ChangeExtension(video.Output, "wav") + "\"";

                //File.WriteAllText("args.txt", "ffmpeg " + Arguments);

                Task taskExecute = engine.ExecuteAsync(Arguments, tokenSource.Token);
                taskExecute.Wait();

                for (int i = 0; i < video.stream.Channels.Count; i++)
                {
                    try
                    {
                        string path = Path.ChangeExtension(video.Input, "") + video.stream.Channels[i] + ".wav";
                        File.Delete(path);
                    }
                    catch { }
                }
            }
            catch { }
        }

        private static void GenerateArgumentsAudio(Video video, bool embed, SubtitlesType subtitlesType)
        {
            Info.CurrentEngineStatus = CurrentEngineStatus.Default;

            //if (video.Intervals.Count <= 130)
            video.Arguments = "-i \"" + video.Input + "\"";
            video.Arguments += " -i \"" + Path.ChangeExtension(video.Output, "wav") + "\"";

            if (embed)
            {
                if (subtitlesType == SubtitlesType.Normal)
                    video.Arguments += " -i \"" + video.SubtitlesPathNewNormal + "\" -map 0 -map -0:s -map 2 -c:s mov_text -metadata:s:s:0 title=\"Normal\"";
                else if (subtitlesType == SubtitlesType.Exclusive)
                    video.Arguments += " -i \"" + video.SubtitlesPathNewExlusive + "\" -map 0 -map -0:s -map 2 -c:s mov_text -metadata:s:s:0 title=\"Exclusive\"";
                else if (subtitlesType == SubtitlesType.Both)
                    video.Arguments += " -i \"" + video.SubtitlesPathNewNormal + "\" -i \"" + video.SubtitlesPathNewExlusive + "\" -map 0 -map -0:s -map 2 -map 3 -c:s mov_text -metadata:s:s:0 title=\"Normal\" -metadata:s:s:1 title=\"Exclusive\"";
            }

            video.Arguments += " -filter:a \"volume=1.0\" -map_chapters -1";

            if (video.AspectRatio)
                video.Arguments += " -aspect " + video.AspectRatioWidth + ":" + video.AspectRatioHeight;

            video.Arguments += " -c:v copy -map -0:a? -map -0:v? -map 0:0 -map 1:0 -acodec ac3";

            video.Arguments += " \"" + video.Output + "\"";
        }

        private static void GenerateArgumentsNormal(Video video, bool embed, bool ContainsSwearWords, SubtitlesType subtitlesType)
        {
            Info.CurrentEngineStatus = CurrentEngineStatus.Default;

            //if (video.Intervals.Count <= 130)
            video.Arguments = "-i \"" + video.Input + "\"";

            if (embed)
            {
                if (subtitlesType == SubtitlesType.Normal)
                    video.Arguments += " -i \"" + video.SubtitlesPathNewNormal + "\" -map 0 -map -0:s -map 1 -c:s mov_text -metadata:s:s:0 title=\"Normal\"";
                else if (subtitlesType == SubtitlesType.Exclusive)
                    video.Arguments += " -i \"" + video.SubtitlesPathNewExlusive + "\" -map 0 -map -0:s -map 1 -c:s mov_text -metadata:s:s:0 title=\"Exclusive\"";
                else if (subtitlesType == SubtitlesType.Both)
                    video.Arguments += " -i \"" + video.SubtitlesPathNewNormal + "\" -i \"" + video.SubtitlesPathNewExlusive + "\" -map 0 -map -0:s -map 1 -map 2 -c:s mov_text -metadata:s:s:0 title=\"Normal\" -metadata:s:s:1 title=\"Exclusive\"";
            }

            video.Arguments += " -filter:a \"volume=1.0\" -vcodec copy -map_chapters -1";

            if (video.AspectRatio)
                video.Arguments += " -aspect " + video.AspectRatioWidth + ":" + video.AspectRatioHeight;

            if (ContainsSwearWords)
            {
                video.Arguments += " -af \"";
                video.Arguments += GetIntervalArguments(video);
            }

            video.Arguments += " \"" + video.Output + "\"";
        }

        private static string GetIntervalArguments(Video video)
        {
            string arguments = "";

            foreach (Subtitle subtitle in video.Subtitles)
            {
                if (subtitle.RemoveFlag)
                {
                    foreach (var remove in subtitle.Remove)
                    {
                        string interval = remove.Key.TotalSeconds.ToString().Replace(",", ".") + "," + remove.Value.TotalSeconds.ToString().Replace(",", ".");
                        arguments += "volume=enable='between(t," + interval + ")':volume=0,";
                    }
                }
            }

            arguments = arguments.Substring(0, arguments.Length - 1) + "\"";

            return arguments;
        }

        private static void DeleteIfExist(string file)
        {
            try
            {
                if (File.Exists(file))
                    File.Delete(file);
            }
            catch { }
        }


        public static Video currentVideo;
        private static void Execute(Video video)
        {
            try
            {
                Tool.CreateFFMPEG();

                //File.WriteAllText("args.txt", "ffmpeg " + video.Arguments);
                Task taskExecute = engine.ExecuteAsync(video.Arguments, tokenSource.Token);
                taskExecute.Wait();
            }
            catch { }
        }

        private static void Cleanup(Video video, bool embed, SubtitlesType subtitlesType, bool deleteOriginalFiles)
        {
            try
            {
                if (tokenSource.Token.IsCancellationRequested)
                {
                    Tool.DelayDelete(video.Output, 500);

                    if ((subtitlesType == SubtitlesType.Normal || subtitlesType == SubtitlesType.Both) && File.Exists(video.SubtitlesPathNewNormal))
                    {
                        try
                        {
                            File.Delete(video.SubtitlesPathNewNormal);
                        }
                        catch { }
                    }

                    if ((subtitlesType == SubtitlesType.Exclusive || subtitlesType == SubtitlesType.Both) && File.Exists(video.SubtitlesPathNewExlusive))
                    {
                        try
                        {
                            File.Delete(video.SubtitlesPathNewExlusive);
                        }
                        catch { }
                    }
                }
                else
                {
                    if (embed)
                    {
                        if ((subtitlesType == SubtitlesType.Normal || subtitlesType == SubtitlesType.Both) && File.Exists(video.SubtitlesPathNewNormal))
                        {
                            try
                            {
                                File.Delete(video.SubtitlesPathNewNormal);
                            }
                            catch { }
                        }


                        if ((subtitlesType == SubtitlesType.Exclusive || subtitlesType == SubtitlesType.Both) && File.Exists(video.SubtitlesPathNewExlusive))
                        {
                            try
                            {
                                File.Delete(video.SubtitlesPathNewExlusive);
                            }
                            catch { }
                        }
                    }

                    if (deleteOriginalFiles)
                    {
                        FileInfo fi = new FileInfo(video.Output);

                        if (fi.Length > 1024)
                        {
                            if (File.Exists(video.SubtitlesPathOriginal))
                            {
                                try
                                {
                                    File.Delete(video.SubtitlesPathOriginal);
                                }
                                catch { }
                            }

                            if (File.Exists(video.Input))
                            {
                                try
                                {
                                    File.Delete(video.Input);
                                }
                                catch { }
                            }
                        }
                    }
                }


                if (video.stream != null)
                {
                    Thread.Sleep(500);

                    for (int i = 0; i < video.stream.Channels.Count; i++)
                    {
                        string path = Path.ChangeExtension(video.Input, "") + video.stream.Channels[i] + ".wav";
                        if (File.Exists(path))
                        {
                            try
                            {
                                File.Delete(path);
                            }
                            catch { }
                        }

                        path = Path.ChangeExtension(video.Input, "") + video.stream.Channels[i] + "_.wav";
                        if (File.Exists(path))
                        {
                            try
                            {
                                File.Delete(path);
                            }
                            catch { }
                        }
                    }

                    try
                    {
                        if (File.Exists(Path.ChangeExtension(video.Output, "wav")))
                            File.Delete(Path.ChangeExtension(video.Output, "wav"));
                    }
                    catch { }
                }
            }
            catch { }

        }

        public static TimeSpan GetTotalDuration(Video video)
        {
            Tool.CreateFFMPEG();

            Process proc = new Process();
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.FileName = "ffmpeg";
            proc.StartInfo.Arguments = "-i \"" + video.Input + "\"";
            proc = Process.Start(proc.StartInfo);

            string[] output = proc.StandardError.ReadToEnd().ToLower().Split(new string[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);

            proc.WaitForExit();

            for (int i = 0; i < output.Length; i++)
            {
                string line = output[i].Trim();

                if (line.StartsWith("duration: "))
                {
                    line = line.Substring("duration: ".Length);
                    line = line.Substring(0, line.IndexOf(','));
                    return TimeSpan.Parse(line);
                }
            }

            return TimeSpan.FromMilliseconds(0);
        }
    }

    public enum SubtitlesType
    {
        Normal,
        Exclusive,
        Both
    }
}