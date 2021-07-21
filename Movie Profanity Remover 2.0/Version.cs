using System;
using System.Net;
using System.Threading;

namespace Movie_Profanity_Remover_2._0
{
    public static class Version
    {
        public static void Check(CfrmMain main)
        {
            Thread thread = new Thread(() =>
            {
                try
                {
                    using (WebClient client = new WebClient())
                    {
                        string[] htmlCode = client.DownloadString("https://pastebin.com/raw/McQ75mpw").Split(new string[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);

                        for (int i = 0; i < htmlCode.Length; i++)
                        {
                            if (htmlCode[i].Contains("[MPR2]"))
                            {
                                string version = htmlCode[i].Substring("[MPR2]".Length);
                                version = version.Substring(0, version.Length - "[/MPR2]".Length);

                                if (Info.Version != version)
                                    main.ShowUpdate(version);
                            }
                        }

                    }
                }
                catch { }
            });
            thread.Start();
        }
    }
}
