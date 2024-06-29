using System.Text.RegularExpressions;

namespace KiWi_Keyboard_Layout_Creator
{
    /// <summary>
    /// displays a layer of a key, and has change events for changes the user makes
    /// only displays, never changes keys directly only reports changes via events
    /// </summary>
    public partial class CtlLayerInput : UserControl
    {
        #region fields
        private bool fireChangeEvents = true;
        #endregion

        #region events
        public event EventHandler? ValueChange;
        public event EventHandler? LayerChange;
        public event EventHandler? RemoveClick;
        public event EventHandler? NlsEnsureKeyUpClick;
        private void InvokeChange()
        {
            if (fireChangeEvents)
            { ValueChange?.Invoke(this, EventArgs.Empty); }
        }
        #endregion

        #region properties
        public bool IsEmpty
        {
            get
            {
                if (string.IsNullOrEmpty(TxtWChar.Text)
                    && cbbSpecialNls.SelectedIndex < 1
                    && ctlLayerSelection.LayerMask == 0)
                { return true; }
                return false;
            }
        }

        public int LayerMask
        {
            get
            { return ctlLayerSelection.LayerMask; }
            set
            {
                ctlLayerSelection.LayerMask = value;
                UpdateSpecialNlsEnabled();
            }
        }

        public bool LayerChkEnabled
        {
            get
            { return ctlLayerSelection.Enabled; }
            set
            { ctlLayerSelection.Enabled = value; }
        }

        public bool DeleteLayerBtnVisible
        {
            get { return BtnDeleteLayer.Visible; }
            set
            { BtnDeleteLayer.Visible = value; }
        }

        public char? WCHAR
        {
            get
            {
                if (String.IsNullOrEmpty(TxtWChar.Text))
                { return null; }
                return TxtWChar.Text[0];
            }
            set
            {
                if (value == null)
                {
                    TxtWChar.Text = "";
                    txtWCharHex.Text = "";
                }
                else
                {
                    TxtWChar.Text = value.ToString();
                    txtWCharHex.Text = ((Int16)value).ToString("X4");
                }
            }
        }

        /// <summary>
        /// KBDNLS = 0 if no KBDNLS selected
        /// </summary>
        public NlsType Nls
        {
            //the index of the KBDNLS entries in the cbb are like their Enumvalues - 1. 
            get
            { return (NlsType)(cbbSpecialNls.SelectedIndex); }
            set
            {
                cbbSpecialNls.SelectedIndex = (int)value;
                TxtSpecialNls.Enabled = (cbbSpecialNls.SelectedIndex == (int)NlsType.SEND_PARAM_VK);

                if (!TxtSpecialNls.Enabled)
                { TxtSpecialNls.Text = ""; }
            }
        }

        public VK? NlsVk
        {
            get
            { return TxtSpecialNls.Vk; }
            set
            {
                if (TxtSpecialNls.Enabled)
                { TxtSpecialNls.Vk = value; }
                else
                { TxtSpecialNls.Vk = null; }
            }
        }

        public bool ToggleKeyUp
        {
            get
            { return chkToggleKeyUp.Checked; }
            set
            { chkToggleKeyUp.Checked = value; }
        }

        /// <summary>
        /// KBDNLS = 0 if no KBDNLS selected
        /// </summary>
        public NlsType NlsKeyUp
        {
            //the index of the KBDNLS entries in the cbb are like their Enumvalues - 1. 
            get
            { return (NlsType)(cbbNlsUp.SelectedIndex); }
            set
            {
                cbbNlsUp.SelectedIndex = (int)value;
                txtNlsUpVk.Enabled = (cbbNlsUp.SelectedIndex == (int)NlsType.SEND_PARAM_VK);

                if (!txtNlsUpVk.Enabled)
                { txtNlsUpVk.Text = ""; }
            }
        }

        public VK? NlsVkKeyUp
        {
            get
            { return txtNlsUpVk.Vk; }
            set
            {
                if (txtNlsUpVk.Enabled)
                { txtNlsUpVk.Vk = value; }
                else
                { txtNlsUpVk.Vk = null; }
            }
        }

        public bool NlsKeyUpLayerBit
        {
            get
            { return chkToggleKeyUp.Checked; }
            set
            { chkToggleKeyUp.Checked = value; }
        }
        #endregion

        #region constructor
        public CtlLayerInput()
        {
            InitializeComponent();
            TxtSpecialNls.VkChanged += TxtSpecialNls_VkChanged;
            txtNlsUpVk.VkChanged += TxtNlsUpVk_VkChanged;
        }
        #endregion

        #region event handlers
        private void CtlLayerSelection_LayerChange(object sender, ValueChangedEventArgs<int> e)
        {
            if (fireChangeEvents)
            { LayerChange?.Invoke(this, EventArgs.Empty); }
        }

        private void BtnDeleteLayer_Click(object sender, EventArgs e)
        {
            RemoveClick?.Invoke(this, EventArgs.Empty);
        }

        #region Normal character group events
        private void ParseTxtWCharToTxtWCharHex()
        {
            if (TxtWChar.Text.Length == 0)
            { txtWCharHex.Text = ""; }
            else
            { txtWCharHex.Text = "0x" + ((int)TxtWChar.Text.First()).ToString("X4"); }
        }

        [GeneratedRegex(@"^0x[0-9a-fA-F]{4}$")]
        private static partial Regex HexStringRegexPrefixed();
        [GeneratedRegex(@"^[0-9a-fA-F]{4}$")]
        private static partial Regex HexStringRegex();
        private bool ParseTxtWCharHexToTxtWChar()
        {
            var match1 = HexStringRegexPrefixed().Match(txtWCharHex.Text);
            var match2 = HexStringRegex().Match(txtWCharHex.Text);
            if (match1.Success)
            {
                TxtWChar.Text = ((char)Convert.ToInt32(txtWCharHex.Text, 16)).ToString();
                InvokeChange();
                return true;
            }
            else if (match2.Success)
            {
                TxtWChar.Text = ((char)int.Parse(txtWCharHex.Text, System.Globalization.NumberStyles.HexNumber)).ToString();
                InvokeChange();
                return true;
            }

            return false;
        }

        private void TxtWChar_TextChanged(object sender, EventArgs e)
        {
            ParseTxtWCharToTxtWCharHex();
            InvokeChange();
        }

        private void TxtWCharHex_Leave(object sender, EventArgs e)
        {
            if (txtWCharHex.Text.Length == 0)
            {
                TxtWChar.Text = "";
                InvokeChange();
            }
            else if (!ParseTxtWCharHexToTxtWChar())
            { ParseTxtWCharToTxtWCharHex(); }
        }

        private void TxtWCharHex_TextChanged(object sender, EventArgs e)
        {
            if (txtWCharHex.Text.Length == 0)
            {
                TxtWChar.Text = "";
                InvokeChange();
            }
            else
            { ParseTxtWCharHexToTxtWChar(); }
        }
        #endregion

        #region Nls group events
        private void CbbSpecialNls_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtSpecialNls.Enabled = (cbbSpecialNls.SelectedIndex == (int)NlsType.SEND_PARAM_VK);
            TxtSpecialNls.Text = TxtSpecialNls.Enabled ? TxtSpecialNls.Text : "";
            InvokeChange();
        }

        private void CbbNlsUp_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNlsUpVk.Enabled = (cbbNlsUp.SelectedIndex == (int)NlsType.SEND_PARAM_VK);
            txtNlsUpVk.Text = txtNlsUpVk.Enabled ? txtNlsUpVk.Text : "";
            InvokeChange();
        }

        private void TxtSpecialNls_VkChanged(object? sender, EventArgs e)
        {
            btnNlsEnsureKeyUp.Enabled = !String.IsNullOrEmpty(TxtSpecialNls.Text);
            InvokeChange();
        }

        private void TxtNlsUpVk_VkChanged(object? sender, EventArgs e)
        {
            InvokeChange();
        }

        private void ChkToggleKeyUp_CheckedChanged(object sender, EventArgs e)
        {
            InvokeChange();
        }

        private void BtnNlsEnsureKeyUp_Click(object sender, EventArgs e)
        {
            NlsEnsureKeyUpClick?.Invoke(this, EventArgs.Empty);
        }
        #endregion
        #endregion

        #region utility
        private void UpdateSpecialNlsEnabled()
        {
            // if none of the layerbits, that are not possible for special nls are set, enable
            bool enable = 0x00 == (LayerMask & ((int)KbdLayers.KBDKANA | (int)KbdLayers.KBDROYA | (int)KbdLayers.KBDLOYA | (int)KbdLayers.KBDCUSTOM | (int)KbdLayers.KBDGRPSELTAP));
            cbbSpecialNls.Enabled = enable;
            TxtSpecialNls.Enabled = enable;
            cbbNlsUp.Enabled = enable;
            TxtSpecialNls.Enabled = enable;
        }

        /// <summary>
        /// does not fire layerchange or change events while during these changes
        /// </summary>
        internal void DisplayKeylayer(KeyboardKey key, int layer)
        {
            fireChangeEvents = false;

            LayerMask = layer;

            if (key.LayerToWCHAR.TryGetValue(layer, out char value))
            { WCHAR = value; }
            else
            { WCHAR = null; }

            if (key.LayerToNls.TryGetValue(layer, out NLSPair? value2))
            {
                if (value2 != null)
                {
                    Nls = value2.NlsType;
                    NlsVk = value2.NlsVk;
                }
                else
                {
                    Nls = NlsType.NULL;
                    NlsVk = null;
                }
            }
            else
            {
                Nls = NlsType.NULL;
                NlsVk = null;
            }

            if (key.LayerToNlsKeyUp.TryGetValue(layer, out NLSPair? value3))
            {
                if (value3 != null)
                {
                    NlsKeyUp = value3.NlsType;
                    NlsVkKeyUp = value3.NlsVk;
                }
                else
                {
                    NlsKeyUp = NlsType.NULL;
                    NlsVkKeyUp = null;
                }
            }
            else
            {
                NlsKeyUp = NlsType.NULL;
                NlsVkKeyUp = null;
            }

            chkToggleKeyUp.Checked = key.GetNlsKeyUpMaskBit(layer);
            btnNlsEnsureKeyUp.Enabled = !String.IsNullOrEmpty(TxtSpecialNls.Text);
            fireChangeEvents = true;
        }
        #endregion
    }
}
