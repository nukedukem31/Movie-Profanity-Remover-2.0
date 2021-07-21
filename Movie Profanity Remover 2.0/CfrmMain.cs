using FFmpeg.NET;
using FFmpeg.NET.Events;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Movie_Profanity_Remover_2._0
{
    public partial class CfrmMain : Form
    {
        #region Imports
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        #endregion

        List<Video> videos = new List<Video>();

        ContextMenuStrip cms = new ContextMenuStrip();

        bool ForceClear = false;

        public CfrmMain()
        {
            InitializeComponent();

            _ = Analytics.TrackEvent(Text);

            Prepare();
        }

        #region Methods
        void Prepare()
        {
            if (!Environment.Is64BitOperatingSystem)
            {
                MessageBox.Show(this, "64-Bit Operating system required.", "Movie Profanity Remover 2.0", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }

            Version.Check(this);

            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            LoadPreset();

            PrepareContextMenuStrip();

            grpVideoSettings.ForeColor = Color.White;
            grpFilterOptions.ForeColor = Color.White;
            grpSubtitlesSettings.ForeColor = Color.White;
            grpOtherSettings.ForeColor = Color.White;

            //FFMPEG
            Tool.CreateFFMPEG();
            FFMPEG.engine = new Engine(AppDomain.CurrentDomain.BaseDirectory + "ffmpeg.exe");

            FFMPEG.engine.Progress += Engine_Progress;
            FFMPEG.engine.Complete += Engine_Complete;
        }

        public void ShowUpdate(string version)
        {
            try
            {
                Invoke((Action)delegate
                {
                    if (MessageBox.Show(this, "A new update was released (v" + version + ").\nDownload now?", "Movie Profanity Remover 2.0", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        Process.Start("https://csharpsoftwareza.blogspot.com/2020/11/movie-profanity-remover-20.html");
                });
            }
            catch { }
        }

        void PrepareContextMenuStrip()
        {
            cms.Items.Add("Remove", null, cms_Remove);
            cms.Items.Add("Remove All", null, cms_RemoveAll);
        }

        void MessageBoxInformation(string message)
        {
            try
            {
                Invoke((Action)delegate
                {
                    MessageBox.Show(this, message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                });
            }
            catch { }
            
        }

        void MessageBoxError(string message)
        {
            Invoke((Action)delegate
            {
                MessageBox.Show(this, message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            });
        }


        void Reset()
        {
            txtVideo.Text = txtSubtitles.Text = string.Empty;

            Info.CurrentEngineStatus = CurrentEngineStatus.Default;
            Info.CurrentPercentage = 0;
            Info.CurrentVideo = null;
            Info.OverallDuration = TimeSpan.FromMilliseconds(0);
            Info.OverallPercentage = 0;
            Info.OverallElapsedDuration = TimeSpan.FromMilliseconds(0);
        }



        void SelectVideo()
        {
            dlgOpenVideo.Title = "Select video file";
            if (dlgOpenVideo.ShowDialog() == DialogResult.OK)
            {
                txtVideo.Text = dlgOpenVideo.FileName;
                dlgOpenVideo.FileName = "";
            }
        }

        void SelectSubtitles()
        {
            dlgOpenSubtitle.Title = "Select subtitles file";
            if (dlgOpenSubtitle.ShowDialog() == DialogResult.OK)
            {
                txtSubtitles.Text = dlgOpenSubtitle.FileName;
                dlgOpenSubtitle.FileName = "";
            }
        }

        void LoadPreset()
        {
            try
            {
                Tool.LoadSettings();

                //Remove event handlers
                numSingleWordBeforeMS.ValueChanged -= NumSingleWordMS_ValueChanged;
                numSingleWordAfterMS.ValueChanged -= NumSingleWordMS_ValueChanged;

                numWidth.ValueChanged -= NumAspectRatio_ValueChanged;
                numHeight.ValueChanged -= NumAspectRatio_ValueChanged;

                cmbOutputType.SelectedIndexChanged -= CmbOutputType_SelectedIndexChanged;

                //Load settings
                chkAspectRatio.Checked = Tool.Settings.AspectRatio;
                numWidth.Value = Tool.Settings.AspectRatioWidth;
                numHeight.Value = Tool.Settings.AspectRatioHeight;

                cmbOutputType.SelectedItem = "." + Tool.Settings.OutputType;

                //Required before setting embedsubtitles
                chkCreateSubtitles.Checked = Tool.Settings.CreateSubtitles;

                chkEmbedSubtitles.Enabled = chkCreateSubtitles.Checked;
                radNormal.Enabled = radExclusive.Enabled = radBoth.Enabled = chkCreateSubtitles.Checked;
                //End

                chkEmbedSubtitles.Checked = Tool.Settings.EmbedSubtitles;

                if (cmbOutputType.SelectedItem == null)
                    cmbOutputType.SelectedItem = ".mp4";

                if (cmbOutputType.SelectedItem.ToString() == ".mkv")
                {
                    chkEmbedSubtitles.Checked = false;
                    chkEmbedSubtitles.Enabled = false;
                }

                if (!string.IsNullOrEmpty(Tool.Settings.CustomAffix))
                    txtCustomAffix.Text = Tool.Settings.CustomAffix;
                else
                    Tool.Settings.CustomAffix = txtCustomAffix.Text = "_SL";

                numSingleWordBeforeMS.Value = Tool.Settings.SingleWordBefore;
                numSingleWordAfterMS.Value = Tool.Settings.SingleWordAfter;

                chkDeleteOriginalFiles.Checked = Tool.Settings.DeleteOriginalFiles;
                chkMuteVocalsOnly.Checked = Tool.Settings.MuteVocalsOnly;

                radNormal.Checked = Tool.Settings.NormalSubtitles;
                radExclusive.Checked = Tool.Settings.ExclusiveSubtitles;
                radBoth.Checked = Tool.Settings.BothSubtitles;

                if (!radNormal.Checked && !radExclusive.Checked && !radBoth.Checked)
                    radNormal.Checked = true;

                //Add event handlers
                numSingleWordBeforeMS.ValueChanged += NumSingleWordMS_ValueChanged;
                numSingleWordAfterMS.ValueChanged += NumSingleWordMS_ValueChanged;

                numWidth.ValueChanged += NumAspectRatio_ValueChanged;
                numHeight.ValueChanged += NumAspectRatio_ValueChanged;

                cmbOutputType.SelectedIndexChanged += CmbOutputType_SelectedIndexChanged;

                if (string.IsNullOrEmpty(Tool.Settings.CustomAffix))
                    Tool.SaveSettings();
            }
            catch { }
        }


        void ToggleUI(bool @enabled)
        {
            try
            {
                Invoke((Action)delegate
                {
                    foreach (Control control in Controls)
                    {
                        if (control.Name != "lblMinimize" && control.Name != "lblClose" && control.Name != "pnlTitle" && control.Name != "lblTitle" && control.Name != "lvVideos" && control.Name != "btnGo" && control.Name != "prgCurrent" && control.Name != "prgOverall")
                            control.Enabled = enabled;
                    }

                    if (!enabled)
                    {
                        btnAddToQueue.Visible = false;

                        prgCurrent.Value = prgOverall.Value = 0;
                        prgCurrent.Visible = prgOverall.Visible = true;

                        btnGo.Text = "Stop";
                    }
                    else
                    {
                        btnGo.Text = "Go";

                        prgCurrent.Visible = prgOverall.Visible = false;

                        if (cmbOutputType.SelectedItem.ToString() == ".mp4")
                            chkEmbedSubtitles.Visible = true;

                        btnAddToQueue.Visible = true;
                    }
                });
            }
            catch { }
            
        }


        KeyValuePair<List<string>, List<string>> GetSwearWords()
        {
            KeyValuePair<List<string>, List<string>> SwearWords = new KeyValuePair<List<string>, List<string>>(new List<string>(), new List<string>());

            if (Tool.Settings.WordFullJesus)
                SwearWords.Key.Add("jesus");
            if (Tool.Settings.WordSingleJesus)
                SwearWords.Value.Add("jesus");


            if (Tool.Settings.WordFullChrist)
            {
                SwearWords.Key.Add("christ");
                SwearWords.Key.Add("christs");
                SwearWords.Key.Add("christ's");
            }
            if (Tool.Settings.WordSingleChrist)
            {
                SwearWords.Value.Add("christ");
                SwearWords.Value.Add("christs");
                SwearWords.Value.Add("christ's");
            }


            if (Tool.Settings.WordFullGod)
                SwearWords.Key.Add("god");
            if (Tool.Settings.WordSingleGod)
                SwearWords.Value.Add("god");


            if (Tool.Settings.WordFullAss)
            {
                SwearWords.Key.Add("ass");
                SwearWords.Key.Add("asses");
                SwearWords.Key.Add("arse");
                SwearWords.Key.Add("arses");
            }
            if (Tool.Settings.WordSingleAss)
            {
                SwearWords.Value.Add("ass");
                SwearWords.Value.Add("asses");
                SwearWords.Value.Add("arse");
                SwearWords.Value.Add("arses");
            }


            if (Tool.Settings.WordFullAsshole)
            {
                SwearWords.Key.Add("asshole");
                SwearWords.Key.Add("assholes");
            }
            if (Tool.Settings.WordSingleAsshole)
            {
                SwearWords.Value.Add("asshole");
                SwearWords.Value.Add("assholes");
            }


            if (Tool.Settings.WordFullBastard)
            {
                SwearWords.Key.Add("bastard");
                SwearWords.Key.Add("bastards");
            }
            if (Tool.Settings.WordSingleBastard)
            {
                SwearWords.Value.Add("bastard");
                SwearWords.Value.Add("bastards");
            }


            if (Tool.Settings.WordFullBitch)
            {
                SwearWords.Key.Add("bitch");
                SwearWords.Key.Add("bitches");
            }
            if (Tool.Settings.WordSingleBitch)
            {
                SwearWords.Value.Add("bitch");
                SwearWords.Value.Add("bitches");
            }


            if (Tool.Settings.WordFullBullshit)
                SwearWords.Key.Add("bullshit");
            if (Tool.Settings.WordSingleBullshit)
                SwearWords.Value.Add("bullshit");


            if (Tool.Settings.WordFullCock)
            {
                SwearWords.Key.Add("cock");
                SwearWords.Key.Add("cocks");
            }
            if (Tool.Settings.WordSingleCock)
            {
                SwearWords.Value.Add("cock");
                SwearWords.Value.Add("cocks");
            }


            if (Tool.Settings.WordFullCunt)
            {
                SwearWords.Key.Add("cunt");
                SwearWords.Key.Add("cunts");
            }
            if (Tool.Settings.WordSingleCunt)
            {
                SwearWords.Value.Add("cunt");
                SwearWords.Value.Add("cunts");
            }


            if (Tool.Settings.WordFullDamn)
                SwearWords.Key.Add("damn");
            if (Tool.Settings.WordSingleDamn)
                SwearWords.Value.Add("damn");


            if (Tool.Settings.WordFullDick)
            {
                SwearWords.Key.Add("dick");
                SwearWords.Key.Add("dicks");
            }
            if (Tool.Settings.WordSingleDick)
            {
                SwearWords.Value.Add("dick");
                SwearWords.Value.Add("dicks");
            }


            if (Tool.Settings.WordFullDickhead)
            {
                SwearWords.Key.Add("dickhead");
                SwearWords.Key.Add("dickheads");
            }
            if (Tool.Settings.WordSingleDickhead)
            {
                SwearWords.Value.Add("dickhead");
                SwearWords.Value.Add("dickheads");
            }


            if (Tool.Settings.WordFullFuck)
            {
                SwearWords.Key.Add("fuck");
                SwearWords.Key.Add("fucks");
                SwearWords.Key.Add("fuckin");
                SwearWords.Key.Add("fucking");
                SwearWords.Key.Add("fucked");
                SwearWords.Key.Add("fucker");
                SwearWords.Key.Add("fuckers");
            }
            if (Tool.Settings.WordSingleFuck)
            {
                SwearWords.Value.Add("fuck");
                SwearWords.Value.Add("fucks");
                SwearWords.Value.Add("fuckin");
                SwearWords.Value.Add("fucking");
                SwearWords.Value.Add("fucked");
                SwearWords.Value.Add("fucker");
                SwearWords.Value.Add("fuckers");
            }


            if (Tool.Settings.WordFullGoddamn)
                SwearWords.Key.Add("goddamn");
            if (Tool.Settings.WordSingleGoddamn)
                SwearWords.Value.Add("goddamn");


            if (Tool.Settings.WordFullMotherfucker)
            {
                SwearWords.Key.Add("motherfucker");
                SwearWords.Key.Add("motherfuckers");
            }
            if (Tool.Settings.WordSingleMotherfucker)
            {
                SwearWords.Value.Add("motherfucker");
                SwearWords.Value.Add("motherfuckers");
            }


            if (Tool.Settings.WordFullPussy)
            {
                SwearWords.Key.Add("pussy");
                SwearWords.Key.Add("pussies");
            }
            if (Tool.Settings.WordSinglePussy)
            {
                SwearWords.Value.Add("pussy");
                SwearWords.Value.Add("pussies");
            }


            if (Tool.Settings.WordFullShit)
            {
                SwearWords.Key.Add("shit");
                SwearWords.Key.Add("shits");
            }
            if (Tool.Settings.WordSingleShit)
            {
                SwearWords.Value.Add("shit");
                SwearWords.Value.Add("shits");
            }

            foreach (string line in Tool.Settings.WordFullCustom)
            {
                if (!string.IsNullOrWhiteSpace(line))
                    SwearWords.Key.Add(line.ToLower());
            }

            foreach (string line in Tool.Settings.WordSingleCustom)
            {
                if (!string.IsNullOrWhiteSpace(line))
                    SwearWords.Value.Add(line.ToLower());
            }


            //Check for Keys in values and remove
            List<string> Key = SwearWords.Key.Distinct().ToList();
            List<string> Value = SwearWords.Value.Distinct().ToList();

            SwearWords.Key.Clear();
            SwearWords.Value.Clear();

            SwearWords.Key.AddRange(Key);
            SwearWords.Value.AddRange(Value);

            for (int i = 0; i < SwearWords.Value.Count; i++)
            {
                if (SwearWords.Key.Contains(SwearWords.Value[i]))
                    SwearWords.Value.RemoveAt(i);
            }

            return SwearWords;
        }


        void Peek()
        {
            if (txtSubtitles.Text != string.Empty)
            {
                KeyValuePair<List<string>, List<string>> SelectedSwearWords = GetSwearWords();

                if (SelectedSwearWords.Key.Count > 0 || SelectedSwearWords.Value.Count > 0)
                {
                    List<string> _Subtitles = Subtitles.Read(txtSubtitles.Text);
                    List<string> SwearWords = new List<string>();

                    for (int subtitle = 0; subtitle < _Subtitles.Count; subtitle++)
                    {
                        for (int swear = 0; swear < SelectedSwearWords.Key.Count; swear++)
                        {
                            KeyValuePair<int, int> Indexes = Subtitles.SwearWord(_Subtitles, subtitle, SelectedSwearWords.Key[swear]);

                            if (Indexes.Key != -1 && Indexes.Value != -1)
                                SwearWords.Add(SelectedSwearWords.Key[swear]);
                        }

                        for (int swear = 0; swear < SelectedSwearWords.Value.Count; swear++)
                        {
                            KeyValuePair<int, int> Indexes = Subtitles.SwearWord(_Subtitles, subtitle, SelectedSwearWords.Value[swear]);

                            if (Indexes.Key != -1 && Indexes.Value != -1)
                                SwearWords.Add(SelectedSwearWords.Value[swear]);
                        }
                    }

                    PeekCount(SwearWords);
                }
            }
            else
                MessageBoxError("Provide a subtitle file first");
        }

        void PeekCount(List<string> lstSwearWords)
        {
            string message = "";

            var q = from x in lstSwearWords
                    group x by x into g
                    let count = g.Count()
                    orderby count descending
                    select new { Value = g.Key, Count = count };

            foreach (var x in q)
                message += x.Value.ToUpper() + " (" + x.Count + ")" + Environment.NewLine;

            MessageBox.Show(message + "\n" + "Total: " + lstSwearWords.Count, Text, MessageBoxButtons.OK);
        }


        bool UnsavedChanges()
        {
            if (txtVideo.Text != string.Empty || txtSubtitles.Text != string.Empty)
                if (MessageBox.Show("You have unsaved changes. Do you wish to continue anyway?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    return false;
                else
                    return true;

            return false;
        }

        void Remove()
        {
            if (btnAddToQueue.Enabled)
            {
                List<int> SelectedIndices = new List<int>();

                for (int i = 0; i < lvVideos.SelectedIndices.Count; i++)
                {
                    SelectedIndices.Add(lvVideos.SelectedIndices[i]);
                }

                SelectedIndices = SelectedIndices.OrderByDescending(x => x).ToList();

                for (int j = 0; j < SelectedIndices.Count; j++)
                {
                    int Index = SelectedIndices[j];
                    videos.RemoveAt(Index);
                    lvVideos.Items.RemoveAt(Index);
                }
            }
        }

        void RemoveAll()
        {
            Invoke((Action)delegate
            {
                videos.Clear();
                lvVideos.Items.Clear();
            });
        }


        void AddToQueue()
        {
            if (txtVideo.TextLength > 0)
            {
                if (txtSubtitles.TextLength > 0)
                {
                    Video video = new Video();
                    video.Input = txtVideo.Text;
                    video.SubtitlesPathOriginal = txtSubtitles.Text;

                    videos.Add(video);
                    lvVideos.Items.Add(new ListViewItem(new string[] { "", new FileInfo(txtVideo.Text).Name }));

                    Reset();
                }
                else
                    MessageBoxError("No subtitle file specified");
            }
            else
                MessageBoxError("No video file specified");
        }


        void Go()
        {
            if (videos.Count > 0)
            {
                KeyValuePair<List<string>, List<string>> SwearWords = GetSwearWords();

                if (SwearWords.Key.Count > 0 || SwearWords.Value.Count > 0)
                {
                    if (UnsavedChanges())
                        return;

                    ToggleUI(false);

                    try
                    {
                        FFMPEG.tokenSource = new CancellationTokenSource();

                        Thread remove = new Thread(() =>
                        {
                            Start();
                            ToggleUI(true);


                            if (ForceClear)
                                RemoveAll();

                            ForceClear = false;
                        });
                        remove.Start();
                    }
                    catch (Exception ex)
                    {
                        ToggleUI(true);
                        SetForeground();
                        MessageBoxError("Error occured. Please restart the program and try again.\nDetails: " + ex.Message + " " + ex.TargetSite);
                    }
                }
                else
                {
                    MessageBoxError("No filter applied");
                }
            }
            else
            {
                MessageBoxError("No video(s) queued");
            }
        }

        void Stop()
        {
            if (MessageBox.Show("Are you sure you want to abort the process?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                FFMPEG.tokenSource.Cancel();
        }

        void Start()
        {
            int success = 0;
            int total = 0;

            Info.OverallDuration = new TimeSpan();
            Info.OverallElapsedDuration = new TimeSpan();
            Info.CurrentPercentage = 0;
            Info.OverallPercentage = 0;

            for (int i = 0; i < videos.Count; i++)
                Info.OverallDuration += FFMPEG.GetTotalDuration(videos[i]);

            for (int i = 0; i < videos.Count; i++)
            {
                if (FFMPEG.tokenSource != null && FFMPEG.tokenSource.IsCancellationRequested)
                    break;

                Tool.RemoveReadOnlyProperty(videos[i].Input);
                Tool.RemoveReadOnlyProperty(videos[i].SubtitlesPathOriginal);

                videos[i].SubtitlesOriginal = Subtitles.Read(videos[i].SubtitlesPathOriginal);

                KeyValuePair<List<string>, List<string>> SwearWords = GetSwearWords();
                videos[i].SwearWordsFull = SwearWords.Key;
                videos[i].SwearWordsSingle = SwearWords.Value;
                videos[i].SingleWordBefore = (int)numSingleWordBeforeMS.Value;
                videos[i].SingleWordAfter = (int)numSingleWordAfterMS.Value;

                videos[i].Subtitles = Subtitles.GetIntervals(videos[i]);

                int Intervals = videos[i].Subtitles.Where(s => s.RemoveFlag == true).Count();
                //if (Intervals > 0 )
                //{
                    //try
                    //{
                    Execute(videos[i]);
                    if (!FFMPEG.tokenSource.IsCancellationRequested)
                        success++;
                    //}
                    //catch { }
                //}

                Info.OverallElapsedDuration += FFMPEG.GetTotalDuration(videos[i]);
                total++;
            }

            ToggleUI(true);
            SetForeground();
            Reset();

            MessageBoxInformation(success + "/" + total + "  videos successfully created");
        }

        void Execute(Video video)
        {
            bool _CreateSubtitles = false;
            bool EmbedSubtitles = false;
            bool DeleteOriginalFiles = false;
            bool MuteVocalsOnly = false;

            SubtitlesType subtitlesType = SubtitlesType.Normal;

            Invoke((Action)delegate
            {
                _CreateSubtitles = chkCreateSubtitles.Checked;
                EmbedSubtitles = chkEmbedSubtitles.Checked;
                DeleteOriginalFiles = chkDeleteOriginalFiles.Checked;
                MuteVocalsOnly = chkMuteVocalsOnly.Checked;

                if (radExclusive.Checked)
                    subtitlesType = SubtitlesType.Exclusive;
                else if (radBoth.Checked)
                    subtitlesType = SubtitlesType.Both;
            });

            if (_CreateSubtitles && !FFMPEG.tokenSource.IsCancellationRequested)
                CreateSubtitles(video, EmbedSubtitles, subtitlesType);

            if (_CreateSubtitles)
                CreateVideo(video, EmbedSubtitles, subtitlesType, DeleteOriginalFiles, MuteVocalsOnly);
            else
                CreateVideo(video, false, subtitlesType, DeleteOriginalFiles, MuteVocalsOnly);
        }


        void SetForeground()
        {
            try
            {
                Invoke((Action)delegate
                {
                    TopMost = true;
                    TopMost = false;
                });
            }
            catch { }
        }


        void CreateVideo(Video video, bool embed, SubtitlesType subtitlesType, bool deleteOriginalFiles, bool muteVocalsOnly)
        {
            FFMPEG.Mute(video, embed, subtitlesType, deleteOriginalFiles, muteVocalsOnly);
        }

        void CreateSubtitles(Video video, bool embed, SubtitlesType subtitlesType)
        {
            Subtitles.Create(video, embed, subtitlesType);
        }
        #endregion

        #region Event Handlers
        //Form
        private async void CfrmMain_Shown(object sender, EventArgs e)
        {
            await Task.Delay(200);
            Opacity = 1;
        }

        private void CfrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btnGo.Text == "Stop" && MessageBox.Show("Are you sure you want to cancel the operation and terminate the application?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            if (FFMPEG.tokenSource != null)
                FFMPEG.tokenSource.Cancel();
        }


        //Title
        private void PnlTitle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void LblMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void LblClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Context Menu Strip
        private void cms_Remove(object sender, EventArgs e)
        {
            Remove();
        }

        private void cms_RemoveAll(object sender, EventArgs e)
        {
            if (lvVideos.Items.Count > 0 && MessageBox.Show("Are you sure you want to remove all videos?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                RemoveAll();
        }

        //Buttons
        private void btnSelectVideo_Click(object sender, EventArgs e)
        {
            SelectVideo();
        }

        private void btnSelectSubtitle_Click(object sender, EventArgs e)
        {
            SelectSubtitles();
        }

        private void btnPeekSwearCount_Click(object sender, EventArgs e)
        {
            Peek();
        }

        private void BtnFilter_Click(object sender, EventArgs e)
        {
            CfrmFilter filter = new CfrmFilter();
            filter.ShowDialog();
        }

        private void btnAddToQueue_Click(object sender, EventArgs e)
        {
            AddToQueue();
        }

        private void BtnGo_MouseUp(object sender, MouseEventArgs e)
        {
            if (btnGo.ClientRectangle.Contains(btnGo.PointToClient(MousePosition)))
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (btnAddToQueue.Enabled)
                        Go();
                    else
                        Stop();
                }
            }
        }

        //Textboxes
        private void txt_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void txtVideo_DragDrop(object sender, DragEventArgs e)
        {
            List<string> dropped = (e.Data.GetData(DataFormats.FileDrop) as string[]).ToList();

            bool subdirectoriesQuestionAsked = false;
            bool subdirectories = false;

            for (int i = 0; i < dropped.Count; i++)
            {
                if (Directory.Exists(dropped[i]) && Directory.GetDirectories(dropped[i]).Length > 0)
                {
                    if (!subdirectoriesQuestionAsked)
                    {
                        subdirectoriesQuestionAsked = true;

                        if (MessageBox.Show("Include subdirectories?", "VirusTotalMultiUploader", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            subdirectories = true;
                    }
                }

                if (Directory.Exists(dropped[i]))
                    dropped = dropped.Concat(Tool.SearchDirectory(dropped[i], subdirectories)).ToList();
            }

            for (int i = 0; i < dropped.Count; i++)
            {
                string file = dropped[i].ToLower();

                if (file.EndsWith(".mkv") || file.EndsWith(".mp4") || file.EndsWith(".avi"))
                {
                    string subtitle = Path.ChangeExtension(dropped[i], ".srt");
                    if (File.Exists(subtitle))
                    {
                        txtVideo.Text = dropped[i];
                        txtSubtitles.Text = subtitle;
                        AddToQueue();
                    }
                    else
                    {
                        txtVideo.Text = dropped[i];
                    }
                }
            }
        }

        private void txtSubtitle_DragDrop(object sender, DragEventArgs e)
        {
            string dropped = (e.Data.GetData(DataFormats.FileDrop) as string[])[0];

            if (dropped.ToLower().EndsWith(".srt"))
                txtSubtitles.Text = dropped;
        }

        private void TxtCustomAffix_TextChanged(object sender, EventArgs e)
        {
            string CustomAffix = txtCustomAffix.Text;

            if (!string.IsNullOrEmpty(CustomAffix))
                Tool.Settings.CustomAffix = txtCustomAffix.Text;
            else
                Tool.Settings.CustomAffix = "_SL";

            Tool.SaveSettings();
        }

        //Listview
        private void LvVideos_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.NewWidth = lvVideos.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }

        private void LvVideos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                Remove();
            }
            else if (e.Control && e.KeyCode == Keys.A)
            {
                for (int i = 0; i < lvVideos.Items.Count; i++)
                    lvVideos.Items[i].Selected = true;
            }
        }

        private void LvVideos_MouseUp(object sender, MouseEventArgs e)
        {
            cms.Items[0].Enabled = lvVideos.SelectedIndices.Count > 0;
            cms.Items[1].Enabled = lvVideos.Items.Count > 0;

            if (e.Button == MouseButtons.Right && btnAddToQueue.Enabled)
                cms.Show(Cursor.Position);
        }

        //Links
        private void lnkSubscene_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://subscene.com/");
        }

        private void lnkOpensubtitles_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.opensubtitles.org/en/search/subs");
        }

        private void lnkMoviesubtitles_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.moviesubtitles.org/");
        }

        //Checkboxes
        private void ChkAspectRatio_CheckedChanged(object sender, EventArgs e)
        {
            numWidth.Enabled = lblX.Enabled = numHeight.Enabled = chkAspectRatio.Checked;

            Tool.Settings.AspectRatio = chkAspectRatio.Checked;
            Tool.SaveSettings();
        }

        private void ChkCreateSubtitles_CheckedChanged(object sender, EventArgs e)
        {
            chkEmbedSubtitles.Enabled = chkCreateSubtitles.Checked;
            radNormal.Enabled = radExclusive.Enabled = radBoth.Enabled = chkCreateSubtitles.Checked;

            if (cmbOutputType.SelectedItem.ToString() == ".mkv")
                chkEmbedSubtitles.Enabled = false;

            Tool.Settings.CreateSubtitles = chkCreateSubtitles.Checked;
            Tool.SaveSettings();
        }

        private void ChkEmbedSubtitles_CheckedChanged(object sender, EventArgs e)
        {
            Tool.Settings.EmbedSubtitles = chkEmbedSubtitles.Checked;
            Tool.SaveSettings();
        }

        private void ChkDeleteOriginalFiles_CheckedChanged(object sender, EventArgs e)
        {
            Tool.Settings.DeleteOriginalFiles = chkDeleteOriginalFiles.Checked;
            Tool.SaveSettings();
        }

        private void ChkMuteVocalsOnly_CheckedChanged(object sender, EventArgs e)
        {
            Tool.Settings.MuteVocalsOnly = chkMuteVocalsOnly.Checked;
            Tool.SaveSettings();
        }

        //Radioboxes
        private void RadNormal_CheckedChanged(object sender, EventArgs e)
        {
            Tool.Settings.NormalSubtitles = radNormal.Checked;
            Tool.SaveSettings();
        }

        private void RadExclusive_CheckedChanged(object sender, EventArgs e)
        {
            Tool.Settings.ExclusiveSubtitles = radExclusive.Checked;
            Tool.SaveSettings();
        }

        private void RadBoth_CheckedChanged(object sender, EventArgs e)
        {
            Tool.Settings.BothSubtitles = radBoth.Checked;
            Tool.SaveSettings();
        }


        //Comboboxes
        private void CmbOutputType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tool.Settings.OutputType = cmbOutputType.SelectedItem.ToString().Substring(1);

            if (Tool.Settings.OutputType == "mp4")
            {
                if (chkCreateSubtitles.Checked)
                    chkEmbedSubtitles.Enabled = true;
            }
            else
                chkEmbedSubtitles.Enabled = chkEmbedSubtitles.Checked = false;

            Tool.SaveSettings();
        }

        //Numeric up-downs
        private void NumSingleWordMS_ValueChanged(object sender, EventArgs e)
        {
            Tool.Settings.SingleWordBefore = (int)numSingleWordBeforeMS.Value;
            Tool.Settings.SingleWordAfter = (int)numSingleWordAfterMS.Value;
            Tool.SaveSettings();
        }

        private void NumAspectRatio_ValueChanged(object sender, EventArgs e)
        {
            Tool.Settings.AspectRatioWidth = (int)numWidth.Value;
            Tool.Settings.AspectRatioHeight = (int)numHeight.Value;
            Tool.SaveSettings();
        }

        //FFMPEG
        private void Engine_Progress(object sender, ConversionProgressEventArgs e)
        {
            try
            {
                if (Info.CurrentEngineStatus == CurrentEngineStatus.Default)
                {
                    //Current
                    TimeSpan CurrentDuration = e.ProcessedDuration;
                    TimeSpan TotalDuration = FFMPEG.currentVideo.TotalDuration;

                    double CurrentMilliseconds = CurrentDuration.TotalMilliseconds;
                    double TotalMilliseconds = TotalDuration.TotalMilliseconds;

                    int Current = (int)(CurrentMilliseconds / TotalMilliseconds * 1000);

                    //Overall
                    double OverallCurrentMilliseconds = Info.OverallElapsedDuration.TotalMilliseconds + CurrentMilliseconds;
                    double OverallTotalMilliseconds = Info.OverallDuration.TotalMilliseconds;

                    int OverallCurrent = (int)(OverallCurrentMilliseconds / OverallTotalMilliseconds * 1000);

                    Info.CurrentPercentage = Current / 10;
                    Info.OverallPercentage = OverallCurrent / 10;

                    string CurrentVideoName = new FileInfo(FFMPEG.currentVideo.Input).Name;

                    Invoke((Action)delegate
                    {
                        try
                        {
                            prgCurrent.Value = Current;
                            prgOverall.Value = OverallCurrent;

                            tTip.SetToolTip(prgCurrent, "Current Media: " + CurrentVideoName + "  |  (" + Info.CurrentPercentage + "%)");
                        }
                        catch { }

                        try
                        {
                            int current = 0;

                            for (int i = 0; i < lvVideos.Items.Count; i++)
                            {
                                if (lvVideos.Items[i].SubItems[1].Text == CurrentVideoName)
                                {
                                    current = i + 1;
                                    break;
                                }
                            }

                            tTip.SetToolTip(prgOverall, "Overall Progress: " + current.ToString() + "/" + videos.Count.ToString() + "  |  (" + Info.OverallPercentage + "%)");
                        }
                        catch { }
                    });
                }
                else
                {
                    //Current
                    TimeSpan CurrentDuration = e.ProcessedDuration;
                    TimeSpan TotalDuration = FFMPEG.currentVideo.TotalDuration;

                    int Current = (int)(CurrentDuration.TotalMilliseconds / TotalDuration.TotalMilliseconds * 1000);

                    Invoke((Action)delegate
                    {
                        try
                        {
                            prgCurrent.Value = Current;

                            if (Info.CurrentEngineStatus == CurrentEngineStatus.ExtractingChannels)
                                tTip.SetToolTip(prgCurrent, "Current Media: " + new FileInfo(FFMPEG.currentVideo.Input).Name + "  |  Extracting Audio Channels...");
                            else if (Info.CurrentEngineStatus == CurrentEngineStatus.MutingChannels)
                                tTip.SetToolTip(prgCurrent, "Current Media: " + new FileInfo(FFMPEG.currentVideo.Input).Name + "  |  Muting Channel(s)...");
                            else if (Info.CurrentEngineStatus == CurrentEngineStatus.MergingChannels)
                                tTip.SetToolTip(prgCurrent, "Current Media: " + new FileInfo(FFMPEG.currentVideo.Input).Name + "  |  Merging Channels...");
                        }
                        catch { }
                    });
                }
                
            }
            catch { }
        }

        private void Engine_Complete(object sender, ConversionCompleteEventArgs e)
        {
            try
            {
                Invoke((Action)delegate
                {
                    prgCurrent.Value = 0;
                });
            }
            catch { }
        }
        #endregion
    }
}