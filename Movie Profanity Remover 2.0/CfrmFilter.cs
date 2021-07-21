using System;
using System.Linq;
using System.Windows.Forms;

namespace Movie_Profanity_Remover_2._0
{
    public partial class CfrmFilter : Form
    {
        public CfrmFilter()
        {
            InitializeComponent();

            Prepare();
        }

        void Prepare()
        {
            chkWordFullAss.Checked = Tool.Settings.WordFullAss;
            chkWordFullAsshole.Checked = Tool.Settings.WordFullAsshole;
            chkWordFullBastard.Checked = Tool.Settings.WordFullBastard;
            chkWordFullBitch.Checked = Tool.Settings.WordFullBitch;
            chkWordFullBullshit.Checked = Tool.Settings.WordFullBullshit;
            chkWordFullChrist.Checked = Tool.Settings.WordFullChrist;
            chkWordFullCock.Checked = Tool.Settings.WordFullCock;
            chkWordFullCunt.Checked = Tool.Settings.WordFullCunt;
            chkWordFullDamn.Checked = Tool.Settings.WordFullDamn;
            chkWordFullDick.Checked = Tool.Settings.WordFullDick;
            chkWordFullDickhead.Checked = Tool.Settings.WordFullDickhead;
            chkWordFullFuck.Checked = Tool.Settings.WordFullFuck;
            chkWordFullGod.Checked = Tool.Settings.WordFullGod;
            chkWordFullGoddamn.Checked = Tool.Settings.WordFullGoddamn;
            chkWordFullJesus.Checked = Tool.Settings.WordFullJesus;
            chkWordFullMotherfucker.Checked = Tool.Settings.WordFullMotherfucker;
            chkWordFullPussy.Checked = Tool.Settings.WordFullPussy;
            chkWordFullShit.Checked = Tool.Settings.WordFullShit;
            txtAdditionalFull.Lines = Tool.Settings.WordFullCustom.ToArray();

            chkWordSingleAss.Checked = Tool.Settings.WordSingleAss;
            chkWordSingleAsshole.Checked = Tool.Settings.WordSingleAsshole;
            chkWordSingleBastard.Checked = Tool.Settings.WordSingleBastard;
            chkWordSingleBitch.Checked = Tool.Settings.WordSingleBitch;
            chkWordSingleBullshit.Checked = Tool.Settings.WordSingleBullshit;
            chkWordSingleChrist.Checked = Tool.Settings.WordSingleChrist;
            chkWordSingleCock.Checked = Tool.Settings.WordSingleCock;
            chkWordSingleCunt.Checked = Tool.Settings.WordSingleCunt;
            chkWordSingleDamn.Checked = Tool.Settings.WordSingleDamn;
            chkWordSingleDick.Checked = Tool.Settings.WordSingleDick;
            chkWordSingleDickhead.Checked = Tool.Settings.WordSingleDickhead;
            chkWordSingleFuck.Checked = Tool.Settings.WordSingleFuck;
            chkWordSingleGod.Checked = Tool.Settings.WordSingleGod;
            chkWordSingleGoddamn.Checked = Tool.Settings.WordSingleGoddamn;
            chkWordSingleJesus.Checked = Tool.Settings.WordSingleJesus;
            chkWordSingleMotherfucker.Checked = Tool.Settings.WordSingleMotherfucker;
            chkWordSinglePussy.Checked = Tool.Settings.WordSinglePussy;
            chkWordSingleShit.Checked = Tool.Settings.WordSingleShit;
            txtAdditionalSingle.Lines = Tool.Settings.WordSingleCustom.ToArray();
        }

        private void SelectNoneAll(GroupBox groupBox)
        {
            bool selectAll = EnabledCheckbox(groupBox);

            foreach (Control c in groupBox.Controls)
                if ((c is CheckBox))
                    ((CheckBox)c).Checked = selectAll;
        }

        private bool EnabledCheckbox(GroupBox groupBox)
        {
            foreach (Control c in groupBox.Controls)
                if ((c is CheckBox) && !((CheckBox)c).Checked)
                    return true;

            return false;
        }

        void Done()
        {
            Tool.Settings.WordFullAss = chkWordFullAss.Checked;
            Tool.Settings.WordFullAsshole = chkWordFullAsshole.Checked;
            Tool.Settings.WordFullBastard = chkWordFullBastard.Checked;
            Tool.Settings.WordFullBitch = chkWordFullBitch.Checked;
            Tool.Settings.WordFullBullshit = chkWordFullBullshit.Checked;
            Tool.Settings.WordFullChrist = chkWordFullChrist.Checked;
            Tool.Settings.WordFullCock = chkWordFullCock.Checked;
            Tool.Settings.WordFullCunt = chkWordFullCunt.Checked;
            Tool.Settings.WordFullDamn = chkWordFullDamn.Checked;
            Tool.Settings.WordFullDick = chkWordFullDick.Checked;
            Tool.Settings.WordFullDickhead = chkWordFullDickhead.Checked;
            Tool.Settings.WordFullFuck = chkWordFullFuck.Checked;
            Tool.Settings.WordFullGod = chkWordFullGod.Checked;
            Tool.Settings.WordFullGoddamn = chkWordFullGoddamn.Checked;
            Tool.Settings.WordFullJesus = chkWordFullJesus.Checked;
            Tool.Settings.WordFullMotherfucker = chkWordFullMotherfucker.Checked;
            Tool.Settings.WordFullPussy = chkWordFullPussy.Checked;
            Tool.Settings.WordFullShit = chkWordFullShit.Checked;
            Tool.Settings.WordFullCustom = txtAdditionalFull.Lines.ToList();

            Tool.Settings.WordSingleAss = chkWordSingleAss.Checked;
            Tool.Settings.WordSingleAsshole = chkWordSingleAsshole.Checked;
            Tool.Settings.WordSingleBastard = chkWordSingleBastard.Checked;
            Tool.Settings.WordSingleBitch = chkWordSingleBitch.Checked;
            Tool.Settings.WordSingleBullshit = chkWordSingleBullshit.Checked;
            Tool.Settings.WordSingleChrist = chkWordSingleChrist.Checked;
            Tool.Settings.WordSingleCock = chkWordSingleCock.Checked;
            Tool.Settings.WordSingleCunt = chkWordSingleCunt.Checked;
            Tool.Settings.WordSingleDamn = chkWordSingleDamn.Checked;
            Tool.Settings.WordSingleDick = chkWordSingleDick.Checked;
            Tool.Settings.WordSingleDickhead = chkWordSingleDickhead.Checked;
            Tool.Settings.WordSingleFuck = chkWordSingleFuck.Checked;
            Tool.Settings.WordSingleGod = chkWordSingleGod.Checked;
            Tool.Settings.WordSingleGoddamn = chkWordSingleGoddamn.Checked;
            Tool.Settings.WordSingleJesus = chkWordSingleJesus.Checked;
            Tool.Settings.WordSingleMotherfucker = chkWordSingleMotherfucker.Checked;
            Tool.Settings.WordSinglePussy = chkWordSinglePussy.Checked;
            Tool.Settings.WordSingleShit = chkWordSingleShit.Checked;
            Tool.Settings.WordSingleCustom = txtAdditionalSingle.Lines.ToList();

            Tool.SaveSettings();

            Close();
        }

        private void BtnSelectAllNoneFull_Click(object sender, EventArgs e)
        {
            SelectNoneAll(grpFilterFull);
        }

        private void BtnSelectAllNoneSingle_Click(object sender, EventArgs e)
        {
            SelectNoneAll(grpFilterSingle);
        }

        private void BtnDone_Click(object sender, EventArgs e)
        {
            Done();
        }
    }
}
