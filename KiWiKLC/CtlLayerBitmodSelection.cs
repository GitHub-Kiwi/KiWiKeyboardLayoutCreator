namespace KiWi_Keyboard_Layout_Creator
{
    public partial class CtlLayerBitmodSelection : UserControl
    {

        #region fields
        private int previousLayerMask = 0;
        private bool listenToChange = true;
        #endregion

        #region events
        public event EventHandler<ValueChangedEventArgs<int>>? LayerChange;
        #endregion

        #region properties
        public new string Text
        {
            get
            { return GrpLayerSelection.Text; }
            set
            { GrpLayerSelection.Text = value; }
        }

        public new bool Enabled
        {
            get
            { return ChkShift.Enabled; }
            set
            {
                ChkShift.Enabled = value;
                ChkAlt.Enabled = value;
                ChkCTRL.Enabled = value;
                ChkKana.Enabled = value;
                ChkLoya.Enabled = value;
                ChkRoya.Enabled = value;
                ChkCustom.Enabled = value;
                ChkGrpSelTap.Enabled = value;
            }
        }

        /// <summary>
        /// does not trigger LayerChange event
        /// </summary>
        public int LayerMask
        {
            get
            {
                return 0x00
                    | (ChkShift.Checked ? ((int)KbdLayers.KBDSHIFT) : 0x00)
                    | (ChkCTRL.Checked ? ((int)KbdLayers.KBDCTRL) : 0x00)
                    | (ChkAlt.Checked ? ((int)KbdLayers.KBDALT) : 0x00)
                    | (ChkKana.Checked ? ((int)KbdLayers.KBDKANA) : 0x00)
                    | (ChkRoya.Checked ? ((int)KbdLayers.KBDROYA) : 0x00)
                    | (ChkLoya.Checked ? ((int)KbdLayers.KBDLOYA) : 0x00)
                    | (ChkCustom.Checked ? ((int)KbdLayers.KBDCUSTOM) : 0x00)
                    | (ChkGrpSelTap.Checked ? ((int)KbdLayers.KBDGRPSELTAP) : 0x00)
                    ;
            }
            set
            {
                listenToChange = false;
                ChkShift.Checked = (value & (int)KbdLayers.KBDSHIFT) == (int)KbdLayers.KBDSHIFT;
                ChkCTRL.Checked = (value & (int)KbdLayers.KBDCTRL) == (int)KbdLayers.KBDCTRL;
                ChkAlt.Checked = (value & (int)KbdLayers.KBDALT) == (int)KbdLayers.KBDALT;
                ChkKana.Checked = (value & (int)KbdLayers.KBDKANA) == (int)KbdLayers.KBDKANA;
                ChkRoya.Checked = (value & (int)KbdLayers.KBDROYA) == (int)KbdLayers.KBDROYA;
                ChkLoya.Checked = (value & (int)KbdLayers.KBDLOYA) == (int)KbdLayers.KBDLOYA;
                ChkCustom.Checked = (value & (int)KbdLayers.KBDCUSTOM) == (int)KbdLayers.KBDCUSTOM;
                ChkGrpSelTap.Checked = (value & (int)KbdLayers.KBDGRPSELTAP) == (int)KbdLayers.KBDGRPSELTAP;

                if (previousLayerMask != LayerMask)
                {
                    var eArgs = new ValueChangedEventArgs<int>(previousLayerMask, LayerMask);
                    LayerChange?.Invoke(this, eArgs);
                }
                previousLayerMask = LayerMask;
                listenToChange = true;
            }
        }
        #endregion

        #region constructor
        public CtlLayerBitmodSelection()
        {
            InitializeComponent();
        }
        #endregion

        #region event handlers
        private void Chk_CheckedChanged(object sender, EventArgs e)
        {
            if (!listenToChange)
            { return; }

            //if (sender == ChkAlt && ChkAlt.Checked)
            //{ ChkCTRL.Checked = true; }// because alt should never be a layer on its own

            var eArgs = new ValueChangedEventArgs<int>(previousLayerMask, LayerMask);
            previousLayerMask = LayerMask;
            LayerChange?.Invoke(this, eArgs);
        }
        #endregion
    }
}
