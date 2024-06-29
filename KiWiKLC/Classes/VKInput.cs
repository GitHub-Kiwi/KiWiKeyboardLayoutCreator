using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KiWi_Keyboard_Layout_Creator.Classes
{
    public partial class VKInput : UserControl
    {
        #region fields
        private readonly AutoCompleteStringCollection autoCompleteData = [];
        private VK? currentVk;
        DisplayStyle displayStyle = DisplayStyle.VKName;
        #endregion

        #region enums
        internal enum DisplayStyle
        {
            VKName,
            VKHex
        }
        #endregion

        #region events
        public event EventHandler? VkChanged;
        #endregion

        #region properties
        /// <summary>
        /// sets the prefered display style and updates the style of the current displayed Text
        /// </summary>
        internal void SetDisplayStyle(DisplayStyle dsp)
        {
            displayStyle = dsp;
            DisplayVK(Vk);
        }

        internal VK? Vk
        {
            get
            { return currentVk; }
            set
            {
                if (currentVk != value)
                { DisplayVK(value); }
                currentVk = value;
            }
        }

        internal new string Text 
        {
            get { return TxtVk.Text; }
            set { TxtVk.Text = value; }
        }
        #endregion

        #region constructor
        public VKInput()
        {
            InitializeComponent();

            TxtVk.AutoCompleteCustomSource = autoCompleteData;

            TxtVk.KeyUp += TxtVk_OnKeyUp;

            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                foreach (string item in Enum.GetNames(typeof(VK)))
                { autoCompleteData.Add(item); }

                TxtVk.AutoCompleteMode = AutoCompleteMode.Suggest;
                TxtVk.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }   
        }
        #endregion

        #region eventhandlers
        private void TxtVk_OnKeyUp(object? sender, KeyEventArgs e)
        { base.OnKeyUp(e); }

        private void TxtVk_TextChanged(object sender, EventArgs e)
        {
            if (TryStringToVK(TxtVk.Text, out VK? res))
            {
                if (currentVk != res)
                {
                    currentVk = res;
                    VkChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        #endregion

        #region utility
        [GeneratedRegex(@"^[0-9a-fA-F]{4}$")]
        private static partial Regex HexStringRegex();
        [GeneratedRegex(@"^0x[0-9a-fA-F]{4}$")]
        private static partial Regex HexStringRegexPrefixed();

        private bool TryStringToVK(string strVk, out VK? result)
        {
            result = null;
            if (String.IsNullOrEmpty(strVk))
            { return true; }

            // see if it is hex value
            var match1 = HexStringRegexPrefixed().Match(strVk);
            var match2 = HexStringRegex().Match(strVk);
            int hexValue = -1;

            if (match1.Success)
            { hexValue = Convert.ToInt32(strVk, 16); }
            else if (match2.Success)
            { hexValue = int.Parse(strVk, System.Globalization.NumberStyles.HexNumber); }

            if (hexValue > -1)
            {
                if (Enum.IsDefined(typeof(VK), hexValue))
                {
                    result = (VK)hexValue;
                    return true;
                }
                else
                { return false; }
            }

            result = VKFunc.StringToVK(strVk);

            if (result == null)
            { return false; }
            else
            { return true; }

        }

        private void DisplayVK(VK? vk)
        {
            if (vk == null)
            {
                Text = "";
                return;
            }

            if (displayStyle == DisplayStyle.VKName)
            { Text = VKFunc.VKToString(vk); }
            else if (displayStyle == DisplayStyle.VKHex)
            { Text = "0x" + ((int)vk!).ToString("X2"); }
        }
        #endregion
    }
}
