using KiWi_Keyboard_Layout_Creator.Classes;

namespace KiWi_Keyboard_Layout_Creator
{
    /// <summary>
    /// displays the Scancode and Virtual Key data of a Key. This form does change its assigned keys data, when the User make changes in the Gui.
    /// </summary>
    public partial class FormScancodeAndVK : Form
    {
        #region fields
        KeyboardKey? key = null;
        bool bolRecordNextKey = false;
        bool RecordNextKey
        {
            set
            {
                bolRecordNextKey = value;
                lblKeypress.Visible = bolRecordNextKey;
            }
            get
            {
                return bolRecordNextKey;
            }
        }
        bool ignoreChangeEvents = false;
        #endregion

        #region constructor
        public FormScancodeAndVK()
        {
            InitializeComponent();
            txtVk.VkChanged += TxtVk_VkChanged;
            txtVk.SetDisplayStyle(VKInput.DisplayStyle.VKHex);
        }
        #endregion

        #region override
        protected override void WndProc(ref Message m)
        {
            const int WM_KEYUP = 0x0101;
            const int WM_SYSKEYUP = 0x0105;
            const int WM_LBUTTONUP = 0x0202;//mouse buttons
            const int WM_RBUTTONUP = 0x0205;

            if (RecordNextKey)
            {
                //https://learn.microsoft.com/de-de/cpp/mfc/reference/handlers-for-wm-messages?view=msvc-170
                if (m.Msg == WM_KEYUP || m.Msg == WM_SYSKEYUP)
                {
                    RecordNextKey = false;

                    int intvk = Convert.ToInt32(m.WParam);
                    int scancode = Convert.ToInt32((m.LParam >> 16) & 0xFF);
                    int ext = Convert.ToInt32((m.LParam >> 24) & 0x01);
                    VK? Vk = null;

                    if (Enum.IsDefined(typeof(VK), intvk))
                    { Vk = (VK)intvk; }

                    UpdateMultipleKeyProperties(Vk, scancode, ext == 1);
                }
                else if (m.Msg == WM_LBUTTONUP || m.Msg == WM_RBUTTONUP)
                { RecordNextKey = false; }
            }

            base.WndProc(ref m);
        }
        #endregion

        #region event Handlers
        private void BtnScanInput_Click(object sender, EventArgs e)
        {
            RecordNextKey = true;
            this.ActiveControl = null;// remove focus, so no control handles the message before the form
        }

        private void BtnAltGr_Click(object sender, EventArgs e)
        {
            UpdateMultipleKeyProperties(VK.RMENU, 0x38, true);
        }

        private void BtnLWin_Click(object sender, EventArgs e)
        {
            UpdateMultipleKeyProperties(VK.LWIN, 0x5B, true);
        }

        private void BtnRWin_Click(object sender, EventArgs e)
        {
            UpdateMultipleKeyProperties(VK.RWIN, 0x5C, true);
        }

        private void UpdateMultipleKeyProperties(VK? vk, int scancode, bool e0)
        {
            if (key == null)
            { return; }

            KeyboardKey keyNew = new KeyboardKey().CopyProperties(key);
            keyNew.Vk = vk;
            txtVk.Vk = vk;
            keyNew.KeyName = VKFunc.VKToString(vk);
            TxtKeyName.Text = VKFunc.VKToString(vk);
            keyNew.Scancode = scancode;
            TxtScancode.Text = TxtScancode.Text = "0x" + scancode.ToString("X2");
            keyNew.E0 = e0;
            chkE0.Checked = e0;
            key.CopyProperties(keyNew);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            key = null;
            Hide();
        }

        private void FormScancode_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            BtnClose_Click(sender, e);
        }

        private void TxtScancode_Leave(object sender, EventArgs e)
        {
            try
            { UpdateKeyScancode(Convert.ToInt32(TxtScancode.Text, 16)); }
            catch (Exception)
            { TxtScancode.Text = ""; }
        }

        private void ChkExt_CheckedChanged(object sender, EventArgs e)
        {
            if (!ignoreChangeEvents)
            { UpdateE0(chkE0.Checked); }
        }

        private void TxtScancode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            { ActiveControl = null; }
        }

        private void TxtVk_VkChanged(object? sender, EventArgs e)
        {
            //if (!ignoreChangeEvents)
            //{ UpdateKeyVK(txtVk.Vk); }
        }

        private void TxtVk_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            { UpdateKeyVK(txtVk.Vk); }
        }

        private void TxtVk_Leave(object sender, EventArgs e)
        {
            UpdateKeyVK(txtVk.Vk);
        }

        private void FormScancode_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Tab:
                case Keys.Left:
                case Keys.Up:
                case Keys.Right:
                case Keys.Down:
                    // so forms does not automaically change the focused controll, instead of sending the keypress to the form
                    e.IsInputKey = true;
                    break;
            }
        }

        private void TxtKeyName_TextChanged(object sender, EventArgs e)
        {
            if (!ignoreChangeEvents)
            { UpdateKeyVkName(TxtKeyName.Text); }
        }
        #endregion

        #region utility
        internal void Show(KeyboardKey key)
        {
            if (chkKeypressStart.Checked)
            {
                RecordNextKey = true;
            }

            if (key == null)
            {
                chkE0.Checked = false;
                TxtScancode.Text = "";
                TxtKeyName.Text = "";
                txtVk.Text = "";
                Show();
                this.Activate();
                if (RecordNextKey)
                { this.ActiveControl = null; }
                return;
            }

            this.key = key;

            ignoreChangeEvents = true;

            chkE0.Checked = key.E0;

            if (key.Scancode == null)
            { TxtScancode.Text = ""; }
            else
            { TxtScancode.Text = "0x" + key.Scancode?.ToString("X2"); }

            if (key.Vk == null)
            { txtVk.Text = ""; }
            else
            { txtVk.Text = "0x" + ((int)key.Vk).ToString("X2"); }

            if (key.KeyName == null)
            { TxtKeyName.Text = ""; }
            else
            { TxtKeyName.Text = key.KeyName; }

            ignoreChangeEvents = false;

            Show();
            this.Activate();

            if (RecordNextKey)
            { this.ActiveControl = null; }

        }

        private void UpdateE0(bool e0)
        {
            if (key == null) { return; }

            chkE0.Checked = e0;
            key.E0 = chkE0.Checked;
        }

        private void UpdateKeyScancode(int? code)
        {
            if (key == null) { return; }

            if (code == null)
            { TxtScancode.Text = ""; }
            else
            { TxtScancode.Text = "0x" + code?.ToString("X2"); }

            key.Scancode = code;
        }

        private void UpdateKeyVK(VK? Vk)
        {
            if (key == null) { return; }

            txtVk.Vk = Vk;
            key.Vk = Vk;
        }

        private void UpdateKeyVkName(string? keyname)
        {
            if (key == null) { return; }

            if (keyname == null)
            {
                TxtKeyName.Text = "";
                key.KeyName = "";
            }
            else
            {
                key.KeyName = keyname;
                TxtKeyName.Text = key.KeyName;
            }
        }
        #endregion
    }
}
