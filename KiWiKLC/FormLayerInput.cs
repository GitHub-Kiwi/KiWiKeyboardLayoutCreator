using System;

namespace KiWi_Keyboard_Layout_Creator
{
    /// <summary>
    /// displays a Layer of a Key. This form does change its assigned keys data, when the User make changes in the Gui.
    /// Changes are not reported by this form, but by its key.
    /// </summary>
    public partial class FormLayerInput : Form
    {
        #region fields
        private KeyboardKey? key = null;
        #endregion

        #region properties
        public int LayerMask
        {
            get
            { return CtlLayerInput.LayerMask; }
            set
            {
                if (key != null)
                { CtlLayerInput.DisplayKeylayer(key, value); }
                else
                { CtlLayerInput.LayerMask = value; }
            }
        }
        #endregion

        #region constructor
        public FormLayerInput()
        {
            InitializeComponent();
        }
        #endregion

        #region event handlers
        #region keyChange event handlers
        private void RemoveKeyEventHandlers(KeyboardKey key)
        {
            key.LayerToWCHAREntryChanged -= Key_LyrBitmodToWCHAREntryChanged;
            key.LayerToNlsEntryChanged -= Key_LyrBitmodToKBDNLSEntryChanged;
        }

        private void AddKeyEventHandlers(KeyboardKey key)
        {
            key.LayerToWCHAREntryChanged += Key_LyrBitmodToWCHAREntryChanged;
            key.LayerToNlsEntryChanged += Key_LyrBitmodToKBDNLSEntryChanged;
        }

        private void Key_LyrBitmodToKBDNLSEntryChanged(object? sender, EventDictionary<int, NLSPair>.EntryChangedEventArgs e)
        {
            CtlLayerInput.DisplayKeylayer(key!, CtlLayerInput.LayerMask);
        }

        private void Key_LyrBitmodToWCHAREntryChanged(object? sender, EventDictionary<int, char>.EntryChangedEventArgs e)
        {
            CtlLayerInput.DisplayKeylayer(key!, CtlLayerInput.LayerMask);
        }
        #endregion

        private void CtlLayerInput_LayerChange(object sender, EventArgs e)
        {
            if (key != null)
            { CtlLayerInput.DisplayKeylayer(key, CtlLayerInput.LayerMask); }
        }

        private void CtlLayerInput_Change(object sender, EventArgs e)
        {
            if (key == null)
            { return; }

            // values in the mask changed. Apply those to a copyKey and then apply all the changes to the real key, to ensure all events are fired after all changes
            int layerMask = CtlLayerInput.LayerMask;
            char? wchar = CtlLayerInput.WCHAR;
            NlsType NlsLayer = CtlLayerInput.Nls;
            VK? NlsLayerVk = CtlLayerInput.NlsVk;
            NlsType NlsUpLayer = CtlLayerInput.NlsKeyUp;
            VK? NlsUpLayerVk = CtlLayerInput.NlsVkKeyUp;
            bool NlsKeyUpLayerBit = CtlLayerInput.NlsKeyUpLayerBit;

            var keyNew = new KeyboardKey().CopyProperties(key);

            if (wchar == null)
            { keyNew.LayerToWCHAR.Remove(layerMask); }
            else if (!keyNew.LayerToWCHAR.TryAdd(layerMask, (char)wchar))
            { keyNew.LayerToWCHAR[layerMask] = (char)wchar; }

            if (NlsLayer == NlsType.NULL)
            { keyNew.LayerToNls.Remove(layerMask); }
            else
            { keyNew.LayerToNls[layerMask] = new NLSPair(NlsLayer, NlsLayerVk); }

            if (NlsUpLayer == NlsType.NULL)
            { keyNew.LayerToNlsKeyUp.Remove(layerMask); }
            else
            { keyNew.LayerToNlsKeyUp[layerMask] = new NLSPair(NlsUpLayer, NlsUpLayerVk); }

            keyNew.SetNlsKeyUpMaskBit(layerMask, NlsKeyUpLayerBit);

            key.CopyProperties(keyNew);
        }

        private void ctlLayerInput_NlsEnsureKeyUpClick(object sender, EventArgs e)
        {
            if (key == null || CtlLayerInput.NlsVk == null)
            { return; }

            int layerMask = CtlLayerInput.LayerMask;

            var keyNew = new KeyboardKey().CopyProperties(key);

            keyNew.SetNlsKeyUpMaskBit(layerMask, true);

            for (int iLayer = 0; iLayer <= (int)(KbdLayers.KBDSHIFT | KbdLayers.KBDALT | KbdLayers.KBDCTRL); iLayer++)
            { keyNew.LayerToNlsKeyUp[iLayer] = new NLSPair(NlsType.SEND_PARAM_VK, CtlLayerInput.NlsVk); }
            
            key.CopyProperties(keyNew);
            CtlLayerInput.DisplayKeylayer(key, layerMask);
        }

        private void FormLayerInput_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            key = null;
            Hide();
        }
        #endregion

        #region utility
        internal void Show(KeyboardKey key, int layermask)
        {
            var previousKey = this.key;

            this.key = key;

            if (key == null)
            { return; }

            if (previousKey != null)
            { RemoveKeyEventHandlers(previousKey); }

            AddKeyEventHandlers(key);

            CtlLayerInput.DisplayKeylayer(key, layermask);

            Show();
            this.Activate();//become focused window

            this.ActiveControl = CtlLayerInput;
        }
        #endregion
    }
}
