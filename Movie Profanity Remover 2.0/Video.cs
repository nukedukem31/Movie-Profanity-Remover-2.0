using System;
using System.Collections.Generic;

namespace Movie_Profanity_Remover_2._0
{
    public class Video
    {
        public Stream stream;

        public string Input;
        public string Output;
        public string SubtitlesPathOriginal;
        public string SubtitlesPathNewNormal;
        public string SubtitlesPathNewExlusive;
        public string Arguments;

        public TimeSpan TotalDuration;

        public List<string> SubtitlesOriginal;
        public List<Subtitle> Subtitles;
        public List<Subtitle> SubtitlesNew;
        public List<string> SwearWordsFull;
        public List<string> SwearWordsSingle;

        public bool AspectRatio;
        public int AspectRatioWidth;
        public int AspectRatioHeight;

        public int SingleWordBefore;
        public int SingleWordAfter;
    }
}
