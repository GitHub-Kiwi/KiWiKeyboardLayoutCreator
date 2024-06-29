using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KbdLayoutInstaller
{
    /// <summary>
    /// a form is necessary cause, loadkeyboardlayout makeActiveFlag only works if application or parent has keyboardfocus
    /// </summary>
    public partial class FocusForm : Form
    {
        readonly string dllPath;
        readonly string layoutLanguageCode;
        readonly string layoutDescription;
        public int resCode = 0;

        public FocusForm(string dllPath, string layoutLanguageCode, string layoutDescription)
        {
            InitializeComponent();
            this.dllPath = dllPath;
            this.layoutLanguageCode = layoutLanguageCode;
            this.layoutDescription = layoutDescription;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Focus();
            resCode = KbdLayoutInstaller.InstallDll(dllPath, layoutLanguageCode, layoutDescription);

            Close();
        }
    }
}
