using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace KiWi_Keyboard_Layout_Creator
{
    /// <summary>
    /// holds and changes the WChars that are assigned to the Virtual Keycodes of the numpad
    /// </summary>
    public partial class FormNumpad : Form
    {
        #region fields
        private readonly Dictionary<NumpadVK, KeyboardKey> dicVkKeys = [];
        /// <summary>
        /// dont listen to textbox changes when false
        /// </summary>
        private bool listenToTxtChange = true;
        #endregion

        #region enum
        public enum NumpadVK
        {
            NUMPAD0 = 0x60,
            NUMPAD1 = 0x61,
            NUMPAD2 = 0x62,
            NUMPAD3 = 0x63,
            NUMPAD4 = 0x64,
            NUMPAD5 = 0x65,
            NUMPAD6 = 0x66,
            NUMPAD7 = 0x67,
            NUMPAD8 = 0x68,
            NUMPAD9 = 0x69,
            DECIMAL = 0x6E,
        }
        #endregion

        #region Properties
        internal bool TryGetWChar(NumpadVK Vk, int layer, out char? WChar)
        {
            WChar = null;
            if (dicVkKeys.TryGetValue(Vk, out var key))
            {
                if (key.LayerToWCHAR.TryGetValue(layer, out char WCharOut))
                {
                    WChar = WCharOut;
                    return true;
                }
            }
            return false;
        }

        internal HashSet<int> GetUsedLayers()
        {
            HashSet<int> res = [];

            foreach (NumpadVK numpadVk in Enum.GetValues<NumpadVK>())
            {
                foreach (var VKsLayer in dicVkKeys[numpadVk].LayerToWCHAR.Keys)
                { res.Add(VKsLayer); }
            }

            return res;
        }

        internal List<KeyboardKey> GetKeyboardKeys()
        {
            return [.. dicVkKeys.Values];
        }

        internal KeyboardKey GetKeyboardKey(NumpadVK numpadVk)
        {
            return dicVkKeys[numpadVk];
        }
        #endregion

        #region constructor
        public FormNumpad()
        {
            InitializeComponent();
            ctlLayerSelection.LayerChange += CtlLayerSelection_LayerChange;

            foreach (NumpadVK numpadVk in Enum.GetValues<NumpadVK>())
            {
                dicVkKeys[numpadVk] = new KeyboardKey
                {
                    Vk = (VK)((int)numpadVk)
                };
                string? name = Enum.GetName<VK>((VK)((int)numpadVk));
                if (name != null)
                { dicVkKeys[numpadVk].KeyName = name; }
            }
        }
        #endregion

        #region event handlers
        private void FormNumpad_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void CtlLayerSelection_LayerChange(object? sender, ValueChangedEventArgs<int> e)
        {
            UpdateTxtDisplayedValues();
        }

        #region hex and WChar textboxes
        [GeneratedRegex(@"^[0-9a-fA-F]{4}$")]
        private static partial Regex HexStringRegex();
        [GeneratedRegex(@"^0x[0-9a-fA-F]{4}$")]
        private static partial Regex HexStringRegexPrefixed();
        private bool TxtNumHex_ParseHex(TextBox hexTextBox, TextBox wCharTextBox)
        {
            if (hexTextBox.Text.Length == 0)
            { wCharTextBox.Text = ""; }
            else
            {
                var match1 = HexStringRegexPrefixed().Match(hexTextBox.Text);
                var match2 = HexStringRegex().Match(hexTextBox.Text);

                if (match1.Success)
                { wCharTextBox.Text = ((char)Convert.ToInt32(hexTextBox.Text, 16)).ToString(); }
                else if (match2.Success)
                { wCharTextBox.Text = ((char)int.Parse(hexTextBox.Text, System.Globalization.NumberStyles.HexNumber)).ToString(); }
            }

            return false;
        }

        private void TxtNumHex_TextChanged(object sender, EventArgs e)
        {
            if (sender == null || sender is not TextBox || !listenToTxtChange)
            { return; }

            TextBox hexTextBox = (TextBox)sender;
            TextBox? wCharTextBox = MapHexToWCharTextBox(hexTextBox);

            if (wCharTextBox == null)
            { return; }

            if (TxtNumHex_ParseHex(hexTextBox, wCharTextBox))
            { UpdateDicValues(); }
        }

        private void TxtNumHex_Leave(object sender, EventArgs e)
        {
            if (sender == null || sender is not TextBox)
            { return; }

            TextBox hexTextBox = (TextBox)sender;
            TextBox? wCharTextBox = MapHexToWCharTextBox(hexTextBox);

            if (wCharTextBox == null)
            { return; }

            if (!TxtNumHex_ParseHex(hexTextBox, wCharTextBox))
            { TxtNumChar_ToHexTextBox(wCharTextBox, hexTextBox); }
            UpdateDicValues();
        }



        private void TxtNumChar_ToHexTextBox(TextBox wCharTextBox, TextBox hexTextBox)
        {
            if (wCharTextBox.Text.Length == 0)
            { hexTextBox.Text = ""; }
            else
            {
                hexTextBox.Text = "0x" + ((int)wCharTextBox.Text.First()).ToString("X4");
            }
        }

        private void TxtNumChar_TextChanged(object sender, EventArgs e)
        {
            if (sender == null || sender is not TextBox || !listenToTxtChange)
            { return; }

            TextBox wCharTextBox = (TextBox)sender;
            TextBox? hexTextBox = MapWCharToHexTextBox(wCharTextBox);

            if (hexTextBox == null)
            { return; }

            TxtNumChar_ToHexTextBox(wCharTextBox, hexTextBox);
            UpdateDicValues();
        }
        #endregion
        #endregion

        #region utility
        /// <summary>
        /// call after changes to dictionary or selected layer, to ensure the textboxes display the dictionary content for the current layer
        /// </summary>
        private void UpdateTxtDisplayedValues()
        {
            listenToTxtChange = false;
            //load configuration for the selected Layer

            foreach (NumpadVK numpadVk in Enum.GetValues<NumpadVK>())
            {
                var txts = MapNumpadVkToTextBoxes(numpadVk);

                if (txts == null)
                { continue; }

                if (TryGetWChar(numpadVk, ctlLayerSelection.LayerMask, out char? WChar))
                {
                    txts!.Value.txtWchar.Text = (WChar!).ToString();
                    txts!.Value.txtHex.Text = "0x" + ((Int16)WChar).ToString("X4");
                }
                else
                {
                    txts!.Value.txtWchar.Text = "";
                    txts!.Value.txtHex.Text = "";
                }
            }
            listenToTxtChange = true;
        }

        /// <summary>
        /// call after changes to the WChar values
        /// </summary>
        private void UpdateDicValues()
        {
            int layer = ctlLayerSelection.LayerMask;

            foreach (NumpadVK numpadVk in Enum.GetValues<NumpadVK>())
            {
                if (dicVkKeys.TryGetValue(numpadVk, out var key))
                {
                    var txts = MapNumpadVkToTextBoxes(numpadVk);

                    if (txts!.Value.txtWchar.Text == "")
                    { key.LayerToWCHAR.Remove(layer); }
                    else
                    { key.LayerToWCHAR[layer] = txts.Value.txtWchar.Text.First(); }
                }
            }
        }

        private TextBox? MapHexToWCharTextBox(TextBox hexTextBox)
        {
            if (hexTextBox == txtNum0Hex)
            {
                return txtNum0Char;
            }
            else if (hexTextBox == txtNum1Hex)
            {
                return txtNum1Char;
            }
            else if (hexTextBox == txtNum2Hex)
            {
                return txtNum2Char;
            }
            else if (hexTextBox == txtNum3Hex)
            {
                return txtNum3Char;
            }
            else if (hexTextBox == txtNum4Hex)
            {
                return txtNum4Char;
            }
            else if (hexTextBox == txtNum5Hex)
            {
                return txtNum5Char;
            }
            else if (hexTextBox == txtNum6Hex)
            {
                return txtNum6Char;
            }
            else if (hexTextBox == txtNum7Hex)
            {
                return txtNum7Char;
            }
            else if (hexTextBox == txtNum8Hex)
            {
                return txtNum8Char;
            }
            else if (hexTextBox == txtNum9Hex)
            {
                return txtNum9Char;
            }
            else if (hexTextBox == txtNumDecimalHex)
            {
                return txtNumDecimalChar;
            }
            return null;
        }

        private (TextBox txtWchar, TextBox txtHex)? MapNumpadVkToTextBoxes(NumpadVK vK)
        {
            switch (vK)
            {
                case NumpadVK.NUMPAD0:
                    return (txtNum0Char, txtNum0Hex);
                case NumpadVK.NUMPAD1:
                    return (txtNum1Char, txtNum1Hex);
                case NumpadVK.NUMPAD2:
                    return (txtNum2Char, txtNum2Hex);
                case NumpadVK.NUMPAD3:
                    return (txtNum3Char, txtNum3Hex);
                case NumpadVK.NUMPAD4:
                    return (txtNum4Char, txtNum4Hex);
                case NumpadVK.NUMPAD5:
                    return (txtNum5Char, txtNum5Hex);
                case NumpadVK.NUMPAD6:
                    return (txtNum6Char, txtNum6Hex);
                case NumpadVK.NUMPAD7:
                    return (txtNum7Char, txtNum7Hex);
                case NumpadVK.NUMPAD8:
                    return (txtNum8Char, txtNum8Hex);
                case NumpadVK.NUMPAD9:
                    return (txtNum9Char, txtNum9Hex);
                case NumpadVK.DECIMAL:
                    return (txtNumDecimalChar, txtNumDecimalHex);
                default:
                    break;
            }
            return null;
        }

        private TextBox? MapWCharToHexTextBox(TextBox wCharTextBox)
        {
            if (wCharTextBox == txtNum0Char)
            {
                return txtNum0Hex;
            }
            else if (wCharTextBox == txtNum1Char)
            {
                return txtNum1Hex;
            }
            else if (wCharTextBox == txtNum2Char)
            {
                return txtNum2Hex;
            }
            else if (wCharTextBox == txtNum3Char)
            {
                return txtNum3Hex;
            }
            else if (wCharTextBox == txtNum4Char)
            {
                return txtNum4Hex;
            }
            else if (wCharTextBox == txtNum5Char)
            {
                return txtNum5Hex;
            }
            else if (wCharTextBox == txtNum6Char)
            {
                return txtNum6Hex;
            }
            else if (wCharTextBox == txtNum7Char)
            {
                return txtNum7Hex;
            }
            else if (wCharTextBox == txtNum8Char)
            {
                return txtNum8Hex;
            }
            else if (wCharTextBox == txtNum9Char)
            {
                return txtNum9Hex;
            }
            else if (wCharTextBox == txtNumDecimalChar)
            {
                return txtNumDecimalHex;
            }
            return null;
        }
        #endregion

        #region Json Import Export
        internal JsonObject JsonExport()
        {
            var exp = new JsonObject();
            foreach (var numpadVk in Enum.GetValues<NumpadVK>())
            {
                exp.Add(Enum.GetName(typeof(NumpadVK), numpadVk)!, dicVkKeys[numpadVk].JsonExport());
            }
            return exp;
        }

        internal void JsonImport(JsonNode? data)
        {
            if (data == null)
            { return; }
            if (data.GetValueKind() != JsonValueKind.Object)
            { return; }

            var dataobj = data!.AsObject();

            foreach (var pairKeyVal in dataobj)
            {
                if (Enum.TryParse<NumpadVK>(pairKeyVal.Key, out NumpadVK numpadVk) && pairKeyVal.Value != null)
                { dicVkKeys[numpadVk].JsonImport(pairKeyVal.Value.AsObject()); }
            }
            UpdateTxtDisplayedValues();
        }
        #endregion
    }
}
