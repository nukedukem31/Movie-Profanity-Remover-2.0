using System;

namespace Movie_Profanity_Remover_2._0
{
    public static class Info
    {
        public const string Version = "2021.07.21.1";
        public static string Settings = "Settings.dat";

        public static TimeSpan OverallDuration;
        public static TimeSpan OverallElapsedDuration;

        public static double CurrentPercentage = 0;
        public static double OverallPercentage = 0;

        public static Video CurrentVideo;

        public static CurrentEngineStatus CurrentEngineStatus = CurrentEngineStatus.Default;
    }

    public enum CurrentEngineStatus
    {
        Default,
        ExtractingChannels,
        MutingChannels,
        MergingChannels
    }
}
