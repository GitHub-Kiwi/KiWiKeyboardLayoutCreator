using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace KiWi_Keyboard_Layout_Creator
{
    /// <summary>
    /// Displays the keys.
    /// Handles the functionality of the user being able to build the keyboard layout (add/remove/move keys).
    /// General layer independent flags of the keys are changed directly with this form.
    /// Manages the other forms, by opening/closing them and telling them on what key they should work.
    /// Also handles opening/saving projects(layouts).
    /// </summary>
    public partial class FormKiwiLayoutCreator : Form
    {
        #region constvalues
        private const string strResFolderName = "\\_res\\";
        private const string strStandardLayoutName = strResFolderName + "basic layout.json";
        // standard cap sizes https://intercom.help/omnitype/en/articles/5121683-keycap-sizes
        
        private const double intSnappingGridStepsize = 0.25;
        const int intSnappingRange = 10;
        #endregion

        #region State enums
        internal enum UIMode
        {
            None,
            PhysicalChange,
            ScancodesChange,
            VirtualkeysChange,
            SetFlag,
            LayerChange,
        }

        internal enum PhysicalMode
        {
            None,
            AddNormalButton,
            MoveButton,
            AddCtrl,
            AddTab,
            AddCaps,
            AddBackspace,
            AddLongEnter,
            AddRightShift,
            AddSpace,
            AddNumpadEnter,
            RemoveButton
        }
        #endregion

        #region fields
        private readonly string strFormBaseText;
        private readonly FormScancodeAndVK frmScancode = new();
        private readonly FormLayerInput frmLayerInput = new();
        private readonly KeyboardManager kbdManager = new();
        private FormProperties frmProperties;
        private FormNumpad frmNumpad;
        #endregion

        #region properties
        internal UIMode UiMode = UIMode.None;
        internal PhysicalMode PhysicalMd = PhysicalMode.None;
        internal KeyboardKey? FocusedKey = null;
        #endregion

        #region constructor
        public FormKiwiLayoutCreator()
        {
            InitializeComponent();

            frmProperties = new();
            frmNumpad = new();

            strFormBaseText = this.Text;

            if (LoadProject(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location) + strStandardLayoutName))
            {
                AutoSize = true;
                OnResize(new EventArgs());
                var s = new System.Drawing.Size(this.Width, this.Height);
                this.Size = s;
                TsmEditVirtualKeys_Click(this, new EventArgs());
            }

            layerSetupToolStripMenuItem.DropDown.Closing += LayerSetupDropDown_Closing;
        }
        #endregion

        #region event handlers
        #region Button events
        private void RemoveKeyboardKeyEvents(KeyboardKey key)
        {
            key.MouseDown -= OnKbKeyMouseDown;
            key.MouseUp -= OnKbKeyMouseUp;
            key.MouseMove -= OnKbKeyMouseMove;
            key.MouseClick -= OnKbKeyMouseClick;
            key.KeyNameChange -= Key_ValueChange;
            key.LockmasksChange -= Key_ValueChange;
            key.ScancodeChange -= Key_ValueChange;
            key.E0Change -= Key_ValueChange;
            key.VkChange -= Key_ValueChange;
            key.VKHandlingMaskChange -= Key_ValueChange;
            key.LayerToWCHAREntryChanged -= Key_LyrBitmodToWCHAREntryChanged;
            key.LayerToNlsEntryChanged -= Key_LayerToNlsEntryChanged;
        }

        private void RegisterKeyboardKeyEvents(KeyboardKey key)
        {
            key.MouseDown += OnKbKeyMouseDown;
            key.MouseUp += OnKbKeyMouseUp;
            key.MouseMove += OnKbKeyMouseMove;
            key.MouseClick += OnKbKeyMouseClick;
            key.KeyNameChange += Key_ValueChange;
            key.LockmasksChange += Key_ValueChange;
            key.ScancodeChange += Key_ValueChange;
            key.E0Change += Key_ValueChange;
            key.VkChange += Key_ValueChange;
            key.VKHandlingMaskChange += Key_ValueChange;
            key.LayerToWCHAREntryChanged += Key_LyrBitmodToWCHAREntryChanged;
            key.LayerToNlsEntryChanged += Key_LayerToNlsEntryChanged;
        }

        private void Key_LayerToNlsEntryChanged(object? sender, EventDictionary<int, NLSPair>.EntryChangedEventArgs e)
        {
            UpdateKeyDisplay(sender);
        }

        private void Key_LyrBitmodToWCHAREntryChanged(object? sender, EventDictionary<int, char>.EntryChangedEventArgs e)
        {
            UpdateKeyDisplay(sender);
        }

        private void Key_ValueChange(object? sender, EventArgs e)
        {
            UpdateKeyDisplay(sender);
        }

        private void OnKbKeyMouseClick(object? sender, MouseEventArgs e)
        {
            if (sender == null || sender is not KeyboardKey)
            { return; }

            var key = sender as KeyboardKey;

            void SetKeyFlagOnClick()
            {
                if (extensionToolStripMenuItem.Checked)
                { key!.VKHandlingMask ^= (int)VKHandlingMask.KBDEXT; }
                else if (multiToolStripMenuItem.Checked)
                { key!.VKHandlingMask ^= (int)VKHandlingMask.KBDMULTIVK; }
                else if (specialToolStripMenuItem.Checked)
                { key!.VKHandlingMask ^= (int)VKHandlingMask.KBDSPECIAL; }
                else if (numpadToolStripMenuItem.Checked)
                { key!.VKHandlingMask ^= (int)VKHandlingMask.KBDNUMPAD; }
                else if (capslockToolStripMenuItem.Checked)
                {
                    key!.Lockmask ^= (int)LockFlags.CAPLOK;
                    // only either caplok or sgcaps allowed. so unset sgcaps if caplok is set
                    if ((key!.Lockmask & (int)LockFlags.CAPLOK) == (int)LockFlags.CAPLOK)
                    { key!.Lockmask = (key!.Lockmask & ~(int)LockFlags.SGCAPS); }
                }
                else if (sGCapsToolStripMenuItem.Checked)
                {
                    key!.Lockmask ^= (int)LockFlags.SGCAPS;
                    // only either caplok or sgcaps allowed. so unset caplok if sgcaps is set
                    if ((key!.Lockmask & (int)LockFlags.SGCAPS) == (int)LockFlags.SGCAPS)
                    { key!.Lockmask = (key!.Lockmask & ~(int)LockFlags.CAPLOK); }
                }
                else if (capsAltGrToolStripMenuItem.Checked)
                { key!.Lockmask ^= (int)LockFlags.CAPLOKALTGR; }
                else if (kanalockToolStripMenuItem.Checked)
                { key!.Lockmask ^= (int)LockFlags.KANALOK; }
                else if (groupselectorToolStripMenuItem.Checked)
                { key!.Lockmask ^= (int)LockFlags.GRPSELTAP; }
            }

            switch (UiMode)
            {
                case UIMode.None:
                    break;
                case UIMode.PhysicalChange:
                    if (PhysicalMd == PhysicalMode.RemoveButton)
                    {
                        kbdManager.RemoveKey(key!);
                        Controls.Remove(key!);
                    }
                    break;
                case UIMode.ScancodesChange:
                    frmScancode.Show(key!);
                    break;
                case UIMode.VirtualkeysChange:
                    frmScancode.Show(key!);
                    break;
                case UIMode.LayerChange:
                    frmLayerInput.Show(key!, SelectedLayerMask());
                    break;
                case UIMode.SetFlag:
                    SetKeyFlagOnClick();
                    break;
                default:
                    break;
            }
            UpdateKeyDisplay(key!);
        }

        private void OnKbKeyMouseDown(object? sender, MouseEventArgs e)
        {
            if (sender == null || sender is not KeyboardKey)
            { return; }

            var key = sender as KeyboardKey;

            if (UiMode == UIMode.PhysicalChange)
            {
                if (PhysicalMd == PhysicalMode.MoveButton)
                { FocusedKey = key; }
            }
        }

        private void OnKbKeyMouseUp(object? sender, MouseEventArgs e)
        {
            FocusedKey = null;
        }

        private void OnKbKeyMouseMove(object? sender, MouseEventArgs e)
        {
            if (sender == null || sender is not KeyboardKey)
            { return; }

            var key = sender as KeyboardKey;

            if (e.Button == MouseButtons.Left)
            { FormKiwiMoveKey(new Point(e.X + key!.Location.X, e.Y + key.Location.Y)); }
        }
        #endregion

        #region menu strip Event handlers
        #region menu strip File section Event handlers
        private void SaveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.saveProjektDialog.ShowDialog() == DialogResult.OK)
            { SaveProject(saveProjektDialog.FileName); }
        }

        private void LoadProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openProjektDialog.ShowDialog() == DialogResult.OK)
            { LoadProject(openProjektDialog.FileName); }
        }

        private void NewProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Shure?", "Shure", MessageBoxButtons.YesNo) == DialogResult.Yes)
            { ClearProject(); }
        }

        private void LayoutProToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProperties.Show();
        }

        private void NumpadSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNumpad.Show();
        }

        private void BuildKbdFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dllBuilder = new KbdLayoutBuilder(kbdManager.Keys, frmProperties, frmNumpad);
            dllBuilder.kbdBlueprintDestinationFolder = frmProperties.KbdLayoutName;
            if (dllBuilder.BuildKbd_Dll(out string driverPath))
            { StartInstallDllProcess(driverPath); }
            return;
        }

        private void BuildKbdFileWithCommentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dllBuilder = new KbdLayoutBuilder(kbdManager.Keys, frmProperties, frmNumpad);
            dllBuilder.kbdBlueprintDestinationFolder = frmProperties.KbdLayoutName;
            if (dllBuilder.BuildKbd_Dll(out string driverPath))
            { StartInstallDllProcess(driverPath); }
            return;
        }
        #endregion

        #region menu strip physical change Event handlers
        private void AddButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UiMode = UIMode.PhysicalChange;
            PhysicalMd = PhysicalMode.AddNormalButton;
            this.Text = strFormBaseText + " - Add Normal Button";
        }

        private void MoveButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UiMode = UIMode.PhysicalChange;
            PhysicalMd = PhysicalMode.MoveButton;
            this.Text = strFormBaseText + " - Move Button";
        }

        private void CtrlButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UiMode = UIMode.PhysicalChange;
            PhysicalMd = PhysicalMode.AddCtrl;
            this.Text = strFormBaseText + " - Add Ctrl Button";
        }

        private void TabButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UiMode = UIMode.PhysicalChange;
            PhysicalMd = PhysicalMode.AddTab;
            this.Text = strFormBaseText + " - Add Tab Button";
        }

        private void CapsButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UiMode = UIMode.PhysicalChange;
            PhysicalMd = PhysicalMode.AddCaps;
            this.Text = strFormBaseText + " - Add Caps Button";
        }

        private void ReturnButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UiMode = UIMode.PhysicalChange;
            PhysicalMd = PhysicalMode.AddBackspace;
            this.Text = strFormBaseText + " - Add Return Button";
        }

        private void LongEnterSizedButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UiMode = UIMode.PhysicalChange;
            PhysicalMd = PhysicalMode.AddLongEnter;
            this.Text = strFormBaseText + " - Add Long Enter Button";
        }

        private void RShiftSizedButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UiMode = UIMode.PhysicalChange;
            PhysicalMd = PhysicalMode.AddRightShift;
            this.Text = strFormBaseText + " - Add Shift Button";
        }

        private void SpacebarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UiMode = UIMode.PhysicalChange;
            PhysicalMd = PhysicalMode.AddSpace;
            this.Text = strFormBaseText + " - Add Spacebar Button";
        }

        private void NumpadEnterHighButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UiMode = UIMode.PhysicalChange;
            PhysicalMd = PhysicalMode.AddNumpadEnter;
            this.Text = strFormBaseText + " - Add Numpad Enter High Button";
        }

        private void RemoveButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UiMode = UIMode.PhysicalChange;
            PhysicalMd = PhysicalMode.RemoveButton;
            this.Text = strFormBaseText + " - Remove Button";
        }
        #endregion

        #region menu strip Virtual Key section Event handlers
        private void MenuStripVirtualKey_Click(ToolStripMenuItem sender, string textSuffix, UIMode mode)
        {
            this.UiMode = mode;
            UncheckVirtualKeySectionTSMs();
            UncheckLayerTSMs();
            sender.Checked = true;
            Text = strFormBaseText + textSuffix;
            UpdateKeysDisplays();
        }

        private void UncheckVirtualKeySectionTSMs()
        {
            tsmSetupScancode.Checked = false;
            tsmSetupVK.Checked = false;

            extensionToolStripMenuItem.Checked = false;
            multiToolStripMenuItem.Checked = false;
            specialToolStripMenuItem.Checked = false;
            numpadToolStripMenuItem.Checked = false;

            capslockToolStripMenuItem.Checked = false;
            sGCapsToolStripMenuItem.Checked = false;
            capsAltGrToolStripMenuItem.Checked = false;
            kanalockToolStripMenuItem.Checked = false;
            groupselectorToolStripMenuItem.Checked = false;
        }

        private void TsmSetupScancode_Click(object sender, EventArgs e)
        {
            MenuStripVirtualKey_Click(tsmSetupScancode, " - Edit Scancodes", UIMode.ScancodesChange);
        }

        private void TsmEditVirtualKeys_Click(object sender, EventArgs e)
        {
            MenuStripVirtualKey_Click(tsmSetupVK, " - Edit Virtual Keys", UIMode.VirtualkeysChange);
        }

        #region set Scancode flags
        private void ExtensionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuStripVirtualKey_Click(extensionToolStripMenuItem, " - Edit VK handling flag: extension", UIMode.SetFlag);
        }

        private void MultiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuStripVirtualKey_Click(multiToolStripMenuItem, " - Edit VK handling flag: multi", UIMode.SetFlag);
        }

        private void SpecialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuStripVirtualKey_Click(specialToolStripMenuItem, " - Edit VK handling flag: special", UIMode.SetFlag);
        }

        private void NumpadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuStripVirtualKey_Click(numpadToolStripMenuItem, " - Edit VK handling flag: numpad", UIMode.SetFlag);
        }
        #endregion

        #region set lock behaviour flags
        private void CapslockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuStripVirtualKey_Click(capslockToolStripMenuItem, " - Edit lockbehaviour flag: capslock", UIMode.SetFlag);
        }

        private void SGCapsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuStripVirtualKey_Click(sGCapsToolStripMenuItem, " - Edit lockbehaviour flag: sGCaps", UIMode.SetFlag);
        }

        private void CapsAltGrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuStripVirtualKey_Click(capsAltGrToolStripMenuItem, " - Edit lockbehaviour flag: capsAltGr", UIMode.SetFlag);
        }

        private void KanalockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuStripVirtualKey_Click(kanalockToolStripMenuItem, " - Edit lockbehaviour flag: kanalock", UIMode.SetFlag);
        }

        private void GroupselectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuStripVirtualKey_Click(groupselectorToolStripMenuItem, " - Edit lockbehaviour flag: groupselecotr", UIMode.SetFlag);
        }
        #endregion
        #endregion

        #region menu strip Edit Layers
        private void LayerSetupDropDown_Closing(object? sender, ToolStripDropDownClosingEventArgs e)
        {
            // cancel the dropdown closing, if the mouse is inside the dropdown and not inside the editVirtualKeys or edit Scancode item
            Point p = layerSetupToolStripMenuItem.DropDown.PointToClient(Control.MousePosition);

            if (layerSetupToolStripMenuItem.DropDown.ClientRectangle.Contains(p))
            {
                if (!numpadSetupToolStripMenuItem.Bounds.Contains(p))
                { e.Cancel = true; }
            }
        }

        private void UncheckLayerTSMs()
        {
            tsmCtrlPressed.Checked = false;
            tsmShiftPressed.Checked = false;
            tsmAltPressed.Checked = false;
            tsmKanaPressed.Checked = false;
            tsmRoyaPressed.Checked = false;
            tsmLoyaPressed.Checked = false;
            tsmCustomPressed.Checked = false;
            tsmGRouPSELecTorAPingPressed.Checked = false;
            tsmSetupLayers.Checked = false;
        }

        private void EditToolStripPress(object sender, EventArgs e)
        {
            this.UiMode = UIMode.LayerChange;
            tsmSetupLayers.Checked = true;
            UncheckVirtualKeySectionTSMs();
            Text = strFormBaseText + " - Edit Layer";
            UpdateKeysDisplays();
            frmLayerInput.LayerMask = SelectedLayerMask();
        }
        #endregion
        #endregion

        #region mouse events
        private void FormKiwiLayoutCreator_MouseDown(object sender, MouseEventArgs e)
        {
            #region physical change
            void AddButton(double widthSizeUnit, double heightSizeUnit = KeycapSizesUnits.Normal)
            {
                var size = KeyboardKey.SizeFromKeycapUnits(widthSizeUnit, heightSizeUnit);
                Point location = KeySnapping(e.Location, size);
                var key = new KeyboardKey(size, location);
                RegisterKeyboardKeyEvents(key);
                kbdManager.AddKey(key);
                this.Controls.Add(key);
                this.FocusedKey = key;
            }
            #endregion

            switch (UiMode)
            {
                case UIMode.None:
                    break;
                case UIMode.PhysicalChange:
                    switch (PhysicalMd)
                    {
                        case PhysicalMode.None:
                            break;
                        case PhysicalMode.AddNormalButton:
                            AddButton(KeycapSizesUnits.Normal); 
                            break;
                        case PhysicalMode.MoveButton:
                            break;
                        case PhysicalMode.AddCtrl:
                            AddButton(KeycapSizesUnits.Ctrl);
                            break;
                        case PhysicalMode.AddTab:
                            AddButton(KeycapSizesUnits.Tab);
                            break;
                        case PhysicalMode.AddCaps:
                            AddButton(KeycapSizesUnits.Capital);
                            break;
                        case PhysicalMode.AddBackspace:
                            AddButton(KeycapSizesUnits.Backspace);
                            break;
                        case PhysicalMode.AddLongEnter:
                            AddButton(KeycapSizesUnits.Enter);
                            break;
                        case PhysicalMode.AddRightShift:
                            AddButton(KeycapSizesUnits.RShift);
                            break;
                        case PhysicalMode.AddSpace:
                            AddButton(KeycapSizesUnits.Spacebar);
                            break;
                        case PhysicalMode.AddNumpadEnter:
                            AddButton(KeycapSizesUnits.Normal, KeycapSizesUnits.NumpadEnterHeight);
                            break;
                        default:
                            break;
                    }
                    break;
                case UIMode.ScancodesChange:
                    break;
                case UIMode.VirtualkeysChange:
                    break;
                default:
                    break;
            }
        }

        private void FormKiwiLayoutCreator_MouseUp(object sender, MouseEventArgs e)
        {
            FocusedKey = null;
        }

        internal void FormKiwiLayoutCreator_MouseMove(object sender, MouseEventArgs e)
        {
            if (UiMode == UIMode.PhysicalChange && FocusedKey != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    FormKiwiMoveKey(e.Location);
                }
            }
        }

        internal void FormKiwiMoveKey(Point location)
        {
            if (UiMode == UIMode.PhysicalChange && FocusedKey != null)
            {
                FocusedKey.Location = KeySnapping(location, FocusedKey.Size);
            }
        }
        #endregion
        #endregion

        #region update key display
        private void UpdateKeysDisplays()
        {
            foreach (var key in kbdManager.Keys)
            { UpdateKeyDisplay(key); }
        }

        private void UpdateKeyDisplay(object? oKey)
        {
            if (oKey is not KeyboardKey)
            { return; }
            var key = (oKey as KeyboardKey)!;
            UpdateKeyDisplay(key);
        }

        private void UpdateKeyDisplay(KeyboardKey key)
        {
            void SetFlagDisplayed()
            {
                if (extensionToolStripMenuItem.Checked)
                {
                    if ((key.VKHandlingMask & (int)VKHandlingMask.KBDEXT) != 0x00)
                    { key.BackColor = Color.MediumAquamarine; }
                }
                else if (multiToolStripMenuItem.Checked)
                {
                    if ((key.VKHandlingMask & (int)VKHandlingMask.KBDMULTIVK) != 0x00)
                    { key.BackColor = Color.MediumAquamarine; }
                }
                else if (specialToolStripMenuItem.Checked)
                {
                    if ((key.VKHandlingMask & (int)VKHandlingMask.KBDSPECIAL) != 0x00)
                    { key.BackColor = Color.MediumAquamarine; }
                }
                else if (numpadToolStripMenuItem.Checked)
                {
                    if ((key.VKHandlingMask & (int)VKHandlingMask.KBDNUMPAD) != 0x00)
                    { key.BackColor = Color.MediumAquamarine; }
                }
                else if (capslockToolStripMenuItem.Checked)
                {
                    if ((key.Lockmask & (int)LockFlags.CAPLOK) != 0x00)
                    { key.BackColor = Color.MediumAquamarine; }
                }
                else if (sGCapsToolStripMenuItem.Checked)
                {
                    if ((key.Lockmask & (int)LockFlags.SGCAPS) != 0x00)
                    { key.BackColor = Color.MediumAquamarine; }
                }
                else if (capsAltGrToolStripMenuItem.Checked)
                {
                    if ((key.Lockmask & (int)LockFlags.CAPLOKALTGR) != 0x00)
                    { key.BackColor = Color.MediumAquamarine; }
                }
                else if (kanalockToolStripMenuItem.Checked)
                {
                    if ((key.Lockmask & (int)LockFlags.KANALOK) != 0x00)
                    { key.BackColor = Color.MediumAquamarine; }
                }
                else if (groupselectorToolStripMenuItem.Checked)
                {
                    if ((key.Lockmask & (int)LockFlags.GRPSELTAP) != 0x00)
                    { key.BackColor = Color.MediumAquamarine; }
                }
            }

            key.BackColor = SystemColors.Control;
            key.UseVisualStyleBackColor = true;

            switch (UiMode)
            {
                case UIMode.ScancodesChange:
                    if (key.Scancode == null)
                    { key.Text = ""; }
                    else
                    { key.Text = (key.E0 ? "E0 " : "") + "0x" + ((int)key.Scancode).ToString("X2"); }
                    break;
                case UIMode.VirtualkeysChange:
                    key.Text = key.KeyName;
                    break;
                case UIMode.LayerChange:
                    bool hasKbdnls = false;
                    if (key.LayerToNls.TryGetValue(SelectedLayerMask(), out NLSPair? nlsPair))
                    {
                        if (nlsPair != null)
                        {
                            if (!(nlsPair.NlsType == NlsType.NULL || nlsPair.NlsType == NlsType.SEND_BASE_VK))
                            {
                                hasKbdnls = true;

                                if (nlsPair.NlsType == NlsType.SEND_PARAM_VK && nlsPair.NlsVk != null)
                                { key.Text = "VK_" + VKFunc.VKToString(nlsPair.NlsVk); }
                                else
                                { key.Text = nlsPair.NlsType.ToString(); }
                            }
                        }
                    }

                    if (!hasKbdnls)
                    {
                        if (key.LayerToWCHAR.TryGetValue(SelectedLayerMask(), out char lyrchar))
                        { key.Text = lyrchar.ToString(); }
                        else
                        { key.Text = ""; }
                    }
                    break;
                case UIMode.SetFlag:
                    SetFlagDisplayed();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region utility
        internal void StartInstallDllProcess(string dllPath)
        {

            var processInfo = new System.Diagnostics.ProcessStartInfo
            {
                UseShellExecute = true,
                Verb = "RunAs",
                LoadUserProfile = true,
                FileName = @"KbdLayoutInstaller.exe",
                RedirectStandardOutput = false,
                RedirectStandardInput = false,
                CreateNoWindow = true,
            };

            processInfo.ArgumentList.Add(dllPath);
            processInfo.ArgumentList.Add(frmProperties.KbdLayoutLanguageCode.ToString("X4"));
            processInfo.ArgumentList.Add(frmProperties.KbdLayoutDescriptionText);

            System.Diagnostics.Process? p;
            try
            { p = System.Diagnostics.Process.Start(processInfo); }
            catch (Exception)
            {
                MessageBox.Show("The layout installation process could not be started.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (p == null)
            { return; }

            while (!p.HasExited)
            { }
        }

        internal int SelectedLayerMask()
        {
            int mask = 0;
            mask ^= tsmCtrlPressed.Checked ? (int)KbdLayers.KBDCTRL : 0;
            mask ^= tsmShiftPressed.Checked ? (int)KbdLayers.KBDSHIFT : 0;
            mask ^= tsmAltPressed.Checked ? (int)KbdLayers.KBDALT : 0;
            mask ^= tsmKanaPressed.Checked ? (int)KbdLayers.KBDKANA : 0;
            mask ^= tsmLoyaPressed.Checked ? (int)KbdLayers.KBDLOYA : 0;
            mask ^= tsmRoyaPressed.Checked ? (int)KbdLayers.KBDROYA : 0;
            mask ^= tsmCustomPressed.Checked ? (int)KbdLayers.KBDCUSTOM : 0;
            mask ^= tsmGRouPSELecTorAPingPressed.Checked ? (int)KbdLayers.KBDGRPSELTAP : 0;
            return mask;
        }

        /// <summary>
        /// snaps a key to the other keys of this form
        /// </summary>
        /// <param name="location">mouse location</param>
        /// <param name="size">size of key</param>
        /// <returns>the location where the mouse should be / where the key should end up</returns>
        internal Point KeySnapping(Point location, Size size)
        {
            //CTRL for no snap
            if (ModifierKeys.HasFlag(System.Windows.Forms.Keys.Control))
            { return location; }

            // alt-snapping: to a grid, that has the stepsize of KeyboardKey.ButtonSizeUnit * intSnappingGridStepsize
            if (ModifierKeys.HasFlag(System.Windows.Forms.Keys.Alt))
            {
                location.X -= location.X % Convert.ToInt32(Math.Round(KeyboardKey.ButtonSizeUnit * (double)intSnappingGridStepsize));
                location.Y -= location.Y % Convert.ToInt32(Math.Round(KeyboardKey.ButtonSizeUnit * (double)intSnappingGridStepsize));
                return location;
            }

            // normal-snapping: vertical snapping (right/left edges) has priority, if no vertical snapping is found, look for for horizontal (top/bottom edges)
            foreach (KeyboardKey vKey in kbdManager.Keys)
            {
                // find possible vertical snap
                if (Math.Abs(vKey.Left - (location.X + size.Width)) < intSnappingRange || Math.Abs(vKey.Right - location.X) < intSnappingRange)
                {
                    // check if is close enough vertically
                    if (vKey.Bottom + intSnappingRange - location.Y > 0 && vKey.Top - intSnappingRange - location.Y < 0)
                    {
                        // TODO normal snapping not quite how i want it, but ok  for now
                        location.Y = vKey.Top;

                        if (Math.Abs(vKey.Left - (location.X + size.Width)) < intSnappingRange)
                        { location.X = vKey.Left - size.Width - Math.Max(KeyboardKey.Margin.Left, KeyboardKey.Margin.Right); }
                        else
                        { location.X = vKey.Right + Math.Max(KeyboardKey.Margin.Left, KeyboardKey.Margin.Right); }
                        return location;
                    }
                }
                // find possible horizontal snap
                if (Math.Abs(vKey.Top - (location.Y + size.Height)) < intSnappingRange || Math.Abs(vKey.Bottom - location.Y) < intSnappingRange)
                {
                    // check if is close enough horizontally
                    if (intSnappingRange - vKey.Left - size.Width < location.X && vKey.Right + intSnappingRange > location.X)
                    {
                        location.X = vKey.Left;

                        if (Math.Abs(vKey.Top - (location.Y + size.Height)) < intSnappingRange)
                        { location.Y = vKey.Top - size.Height - Math.Max(KeyboardKey.Margin.Top, KeyboardKey.Margin.Bottom); }
                        else
                        { location.Y = vKey.Bottom + Math.Max(KeyboardKey.Margin.Top, KeyboardKey.Margin.Bottom); }
                        return location;
                    }
                }
            }

            //no snapping found
            return location;
        }
        #endregion

        #region save/load/clear Project
        internal void ClearProject()
        {

            frmProperties = new();
            frmNumpad = new();

            foreach (var key in kbdManager.Keys)
            { RemoveKeyboardKeyEvents(key); }
            kbdManager.ClearKeys();

            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i] is KeyboardKey)
                {
                    Controls.RemoveAt(i);
                    i--;
                }
            }
        }

        #region Json load/save
        const string jsonKeyProperties = "Properties";
        const string jsonKeyNumpad = "Numpad";
        const string jsonKeyKeys = "Keys";
        internal bool LoadProject(string path)
        {
            try
            {
                string jsonString = File.ReadAllText(path);
                JsonObject jsonobj = JsonSerializer.Deserialize<JsonObject>(jsonString)!;

                ClearProject();

                if (jsonobj.TryGetPropertyValue(jsonKeyProperties, out var properties))
                { frmProperties.JsonImport(properties); }

                if (jsonobj.TryGetPropertyValue(jsonKeyNumpad, out var numpad))
                { frmNumpad.JsonImport(numpad); }

                if (jsonobj.TryGetPropertyValue(jsonKeyKeys, out var kbdKeys))
                {
                    kbdManager.JsonImport(kbdKeys);
                    Controls.AddRange(kbdManager.Keys.ToArray());

                    foreach (var key in kbdManager.Keys)
                    { RegisterKeyboardKeyEvents(key); }
                    UpdateKeysDisplays();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error while trying to load the project");
                ClearProject();
                return false;
            }
            return true;
        }

        private void SaveProject(string fileName)
        {
            try
            {
                JsonObject jsonobj = new()
                {
                    { jsonKeyProperties, frmProperties.JsonExport() },
                    { jsonKeyNumpad, frmNumpad.JsonExport() },
                    { jsonKeyKeys, kbdManager.JsonExport() }
                };

                File.WriteAllText(fileName, JsonSerializer.Serialize(jsonobj, new JsonSerializerOptions { WriteIndented = true }));
            }
            catch (Exception)
            {
                MessageBox.Show("Error while trying to save the project");
            }
        }
        #endregion
        #endregion
    }
}
