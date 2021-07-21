using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace Movie_Profanity_Remover_2
{
    public static class Analytics
    {
        private static readonly string endpoint = "https://www.google-analytics.com/collect";
        private static readonly string googleVersion = "1";
        private static readonly string googleTrackingId = "UA-131495165-5";
        private static string googleClientId;
        public static async Task<HttpResponseMessage> TrackEvent(string label)
        {
            googleClientId = UUID();
            using (var httpClient = new HttpClient())
            {
                var postData = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("v", googleVersion),
                    new KeyValuePair<string, string>("tid", googleTrackingId),
                    new KeyValuePair<string, string>("cid", googleClientId),
                    new KeyValuePair<string, string>("t", "event"),
                    new KeyValuePair<string, string>("ec", label),
                    new KeyValuePair<string, string>("ea", googleClientId)
                };
                if (label != null)
                    postData.Add(new KeyValuePair<string, string>("el", googleClientId + " - " + label));
                return await httpClient.PostAsync(endpoint, new FormUrlEncodedContent(postData)).ConfigureAwait(false);
            }
        }
        private static string UUID()
        {
            try
            {
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.FileName = "CMD.exe";
                startInfo.Arguments = "/C wmic csproduct get UUID";
                process.StartInfo = startInfo;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();
                process.WaitForExit();
                string output = (process.StandardOutput.ReadToEnd());
                output = output.Replace(" ", "").Replace("\r", "").Replace("\n", "").Replace("UUID", "");
                return output;
            }
            catch
            {
                return new Random().Next().ToString();
            }
        }
    }
}