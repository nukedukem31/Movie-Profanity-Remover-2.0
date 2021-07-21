using FFmpeg.NET;
using System;
using System.Collections.Generic;
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

            GetSurroundStream(video);

            if (muteVocalsOnly && video.stream != null)
            {
                //ExtractChannels(video);
                MuteFrontCenterChannel(video);
                //string AudioPath = @"D:\Torrents\Movies\Wrath.Of.Man.1080p.WEB-DL.DD5.1.H.264-EVO_SL.wav";
                //GenerateArgumentsAudio(video, embed, stream, AudioPath, subtitlesType);
            }
            else
                GenerateArgumentsNormal(video, embed, subtitlesType);

            DeleteIfExist(video.Output);
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
                proc.WaitForExit();

                string[] output = proc.StandardError.ReadToEnd().ToLower().Split(new string[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);

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
                proc.WaitForExit();

                output = proc.StandardOutput.ReadToEnd().ToLower().Split(new string[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < output.Length; i++)
                {
                    string line = output[i].Trim();

                    string[] split = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                    if (split[0] == channel)
                    {
                        video.stream = new Stream();
                        video.stream.Index = streamIndex;
                        video.stream.Channels = split[1].Split(new string[] { "+" }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.ToUpperInvariant()).ToList();

                        if (video.stream.Channels.Contains("FC"))
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
                    Arguments += " -map_channel " + video.stream.Index + "." + i + " \"" + Path.ChangeExtension(video.Input, "") + video.stream.Channels[i] + ".wav\"";

                Task taskExecute = engine.ExecuteAsync(Arguments, tokenSource.Token);
                taskExecute.Wait();
            }
            catch { }
        }

        private static void MuteFrontCenterChannel(Video video)
        {
            try
            {
                Info.CurrentEngineStatus = CurrentEngineStatus.MutingChannel;

                Tool.CreateFFMPEG();

                string Arguments = "-i \"" + Path.ChangeExtension(video.Input, "") + "FC.wav\"";

                Arguments += " -af \"";

                Arguments += GetIntervalArguments(video);

                Arguments += " \"" + Path.ChangeExtension(video.Input, "") + "FC_.wav\"";

                Task taskExecute = engine.ExecuteAsync(Arguments, tokenSource.Token);
                taskExecute.Wait();
            }
            catch { }
        }

        private static void GenerateArgumentsNormal(Video video, bool embed, SubtitlesType subtitlesType)
        {
            //if (video.Intervals.Count <= 130)
            video.Output = video.Input.Substring(0, video.Input.Length - new FileInfo(video.Input).Extension.Length) + Tool.Settings.CustomAffix + "." + Tool.Settings.OutputType;

            video.Arguments = "-i \"" + video.Input + "\"";

            if (embed)
            {
                if (subtitlesType == SubtitlesType.Normal)
                    video.Arguments += " -i \"" + video.SubtitlesPathNewNormal + "\" -map 0 -map -0:s -map 1 -c:s mov_text -metadata:s:s:0 title=\"Normal\"";
                else if (subtitlesType == SubtitlesType.Normal)
                    video.Arguments += " -i \"" + video.SubtitlesPathNewExlusive + "\" -map 0 -map -0:s -map 1 -c:s mov_text -metadata:s:s:0 title=\"Exclusive\"";
                else if (subtitlesType == SubtitlesType.Both)
                    video.Arguments += " -i \"" + video.SubtitlesPathNewNormal + "\" -i \"" + video.SubtitlesPathNewExlusive + "\" -map 0 -map -0:s -map 1 -map 2 -c:s mov_text -metadata:s:s:0 title=\"Normal\" -metadata:s:s:1 title=\"Exclusive\"";
            }

            video.Arguments += " -filter:a \"volume=1.0\" -vcodec copy -map_chapters -1";

            if (video.AspectRatio)
                video.Arguments += " -aspect " + video.AspectRatioWidth + ":" + video.AspectRatioHeight;

            video.Arguments += " -af \"";

            video.Arguments += GetIntervalArguments(video);

            video.Arguments += " \"" + video.Output + "\"";
        }

        private static void GenerateArgumentsAudio(Video video, bool embed, string stream, string AudioPath, SubtitlesType subtitlesType)
        {
            //Extract audio from 5.1 Movie
            //Run mute on wav file
            //Replace stream with file
            //Map subtitles, etc.


            //ffmpeg -i v.mp4 -i a.wav -c:v copy -map 0:v:0 -map 1:a:0 new.mp4

            //if (video.Intervals.Count <= 130)
            video.Output = video.Input.Substring(0, video.Input.Length - new FileInfo(video.Input).Extension.Length) + Tool.Settings.CustomAffix + "." + Tool.Settings.OutputType;

            video.Arguments = "-i \"" + video.Input + "\" -i \"" + AudioPath + "\" -map " + stream.Replace(".", ":") + ":0";

            //if (embed)
            //{
            //    if (subtitlesType == SubtitlesType.Normal)
            //        video.Arguments += " -i \"" + video.SubtitlesPathNewNormal + "\" -map 0 -map -0:s -map 1 -c:s mov_text -metadata:s:s:0 title=\"Normal\"";
            //    else if (subtitlesType == SubtitlesType.Normal)
            //        video.Arguments += " -i \"" + video.SubtitlesPathNewExlusive + "\" -map 0 -map -0:s -map 1 -c:s mov_text -metadata:s:s:0 title=\"Exclusive\"";
            //    else if (subtitlesType == SubtitlesType.Both)
            //        video.Arguments += " -i \"" + video.SubtitlesPathNewNormal + "\" -i \"" + video.SubtitlesPathNewExlusive + "\" -map 0 -map -0:s -map 1 -map 2 -c:s mov_text -metadata:s:s:0 title=\"Normal\" -metadata:s:s:1 title=\"Exclusive\"";
            //}

            video.Arguments += " -filter:a \"volume=1.0\" -vcodec copy -map_chapters -1";

            if (video.AspectRatio)
                video.Arguments += " -aspect " + video.AspectRatioWidth + ":" + video.AspectRatioHeight;

            //video.Arguments += " -af \"";

            //video.Arguments += GetIntervalArguments(video);

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
            Thread execute = new Thread(() =>
            {
                try
                {
                    Tool.CreateFFMPEG();

                    File.WriteAllText("Args.txt", video.Arguments);
                    Task taskExecute = engine.ExecuteAsync(video.Arguments, tokenSource.Token);
                    taskExecute.Wait();
                }
                catch { }
            });
            execute.Start();
            execute.Join();
        }

        private static void Cleanup(Video video, bool embed, SubtitlesType subtitlesType, bool deleteOriginalFiles)
        {
            try
            {
                if (tokenSource.Token.IsCancellationRequested)
                {
                    Tool.DelayDelete(video.Output, 500);

                    if ((subtitlesType == SubtitlesType.Normal || subtitlesType == SubtitlesType.Both) && File.Exists(video.SubtitlesPathNewNormal))
                        File.Delete(video.SubtitlesPathNewNormal);

                    if ((subtitlesType == SubtitlesType.Exclusive || subtitlesType == SubtitlesType.Both) && File.Exists(video.SubtitlesPathNewExlusive))
                        File.Delete(video.SubtitlesPathNewExlusive);
                }
                else
                {
                    if (embed)
                    {
                        if ((subtitlesType == SubtitlesType.Normal || subtitlesType == SubtitlesType.Both) && File.Exists(video.SubtitlesPathNewNormal))
                            File.Delete(video.SubtitlesPathNewNormal);

                        if ((subtitlesType == SubtitlesType.Exclusive || subtitlesType == SubtitlesType.Both) && File.Exists(video.SubtitlesPathNewExlusive))
                            File.Delete(video.SubtitlesPathNewExlusive);
                    }

                    if (deleteOriginalFiles)
                    {
                        FileInfo fi = new FileInfo(video.Output);

                        if (fi.Length > 1024)
                        {
                            if (File.Exists(video.SubtitlesPathOriginal))
                                File.Delete(video.SubtitlesPathOriginal);

                            if (File.Exists(video.Input))
                                File.Delete(video.Input);
                        }
                    }
                }
            }
            catch { }

        }

        public static TimeSpan GetTotalDuration(Video video)
        {
            TimeSpan totalDuration = new TimeSpan();

            Thread metadata = new Thread(() =>
            {
                try
                {
                    Task<MetaData> taskMetadata = engine.GetMetaDataAsync(new MediaFile(video.Input));
                    taskMetadata.Wait();
                    totalDuration = taskMetadata.Result.Duration;
                }
                catch { }
            });
            metadata.Start();
            metadata.Join();

            return totalDuration;
        }
    }

    public enum SubtitlesType
    {
        Normal,
        Exclusive,
        Both
    }
}
