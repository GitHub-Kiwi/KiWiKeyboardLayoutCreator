using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace KiWi_Keyboard_Layout_Creator
{
    public partial class FormProperties : Form
    {
        #region fields
        private readonly AutoCompleteStringCollection autoCompleteData = [];
        #endregion

        #region properties
        private string LanguageCodeNameToCbbString(string value)
        {
            if (Enum.TryParse<LanguageCodes>(value, out LanguageCodes code))
            { return (value + " " + ((int)code).ToString("X4")); }
            return "";
        }

        public int KbdLayoutLanguageCode
        {
            get
            {
                if (CbbLanguageCode.SelectedIndex == -1)
                { return -1; }

                var arr = CbbLanguageCode.Items[CbbLanguageCode.SelectedIndex]?.ToString()?.Split(' ');

                if (arr == null || arr?.Length == 0)
                { return -1; }

                if (Enum.TryParse<LanguageCodes>(arr?[0], out var res))
                { return (int)res; }

                return -1;
            }
            set
            {
                if (Enum.IsDefined<LanguageCodes>((LanguageCodes)value))
                { CbbLanguageCode.SelectedIndex = CbbLanguageCode.FindStringExact(LanguageCodeNameToCbbString(((LanguageCodes)value).ToString())); }
            }
        }

        public string KbdLayoutName
        {
            get
            { return "kbd" + TxtLayoutName.Text.ToLower(); }
            internal set
            {
                if (value.StartsWith("kbd"))
                { TxtLayoutName.Text = value[3..]; }
                else
                { TxtLayoutName.Text = value; }
            }
        }

        public string KbdLayoutVersion
        {
            get
            { return TxtLayoutVersion.Text.TrimEnd('.'); }
            internal set
            { TxtLayoutVersion.Text = value; }
        }

        public string KbdLayoutDescriptionText
        {
            get
            { return TxtLayoutDescription.Text.Replace("\"", "").Replace("\'", ""); }
            internal set
            { TxtLayoutDescription.Text = value; }
        }

        public bool KLLF_AltGr
        {
            get
            { return ChkKllfAltgr.Checked; }
            set
            { ChkKllfAltgr.Checked = value; }
        }

        public bool KLLF_Shiftlock
        {
            get
            { return ChkKllfShiftlock.Checked; }
            set
            { ChkKllfShiftlock.Checked = value; }
        }

        public bool KLLF_LrmRml
        {
            get
            { return ChkKllfLrmRlm.Checked; }
            set
            { ChkKllfLrmRlm.Checked = value; }
        }

        public int GetKLLFMask()
        {
            int msk = 0x0000;
            if (KLLF_AltGr)
            { msk ^= (int)KLLF.KLLF_ALTGR; }
            if (KLLF_Shiftlock)
            { msk ^= (int)KLLF.KLLF_SHIFTLOCK; }
            if (KLLF_LrmRml)
            { msk ^= (int)KLLF.KLLF_LRM_RLM; }
            return msk;
        }

        [GeneratedRegex(@"^\d+")]
        private static partial Regex digitAtStartRegex();
        public int KbdLayoutType
        {
            set
            {
                CbbKeyboardType.SelectedIndex = CbbKeyboardType.FindString(value.ToString());
            }
            get
            {
                if (String.IsNullOrEmpty(CbbKeyboardType.Text))
                { return 4; }
                else
                {
                    try
                    {
                        var match = digitAtStartRegex().Match(CbbKeyboardType.Text);
                        if (match.Success)
                        { return Convert.ToInt32(match.Value); }
                        return 4;
                    }
                    catch (Exception)
                    { return 4; }
                }
            }
        }

        public Dictionary<KbdLayers, VK> GetBitmodVKMapping()
        {
            Dictionary<KbdLayers, VK> map = [];

            if (Enum.TryParse<VK>(TxtBitmodShift.Text, true, out VK vkres))
            { map.Add(KbdLayers.KBDSHIFT, vkres); }
            else
            { map.Add(KbdLayers.KBDSHIFT, VK.SHIFT); }

            if (Enum.TryParse<VK>(TxtBitmodCtrl.Text, true, out vkres))
            { map.Add(KbdLayers.KBDCTRL, vkres); }
            else
            { map.Add(KbdLayers.KBDCTRL, VK.CONTROL); }

            if (Enum.TryParse<VK>(TxtBitmodAlt.Text, true, out vkres))
            { map.Add(KbdLayers.KBDALT, vkres); }
            else
            { map.Add(KbdLayers.KBDALT, VK.MENU); }

            if (Enum.TryParse<VK>(TxtBitmodKana.Text, true, out vkres))
            { map.Add(KbdLayers.KBDKANA, vkres); }
            else
            { map.Add(KbdLayers.KBDKANA, VK.KANA); }


            if (Enum.TryParse<VK>(TxtBitmodRoya.Text, true, out vkres))
            { map.Add(KbdLayers.KBDROYA, vkres); }
            else
            { map.Add(KbdLayers.KBDROYA, VK.OEM_FJ_ROYA); }

            if (Enum.TryParse<VK>(TxtBitmodLoya.Text, true, out vkres))
            { map.Add(KbdLayers.KBDLOYA, vkres); }
            else
            { map.Add(KbdLayers.KBDLOYA, VK.OEM_FJ_LOYA); }

            if (Enum.TryParse<VK>(TxtBitmodCustom.Text, true, out vkres))
            { map.Add(KbdLayers.KBDCUSTOM, vkres); }
            else
            { map.Add(KbdLayers.KBDCUSTOM, VK.CUSTOM); }

            if (Enum.TryParse<VK>(TxtBitmodGrpseltab.Text, true, out vkres))
            { map.Add(KbdLayers.KBDGRPSELTAP, vkres); }
            else
            { map.Add(KbdLayers.KBDGRPSELTAP, VK._NONE__); }

            return map;
        }

        public void SetBitmodVKMapping(Dictionary<KbdLayers, VK> map)
        {
            if (map == null)
            { return; }

            foreach (var key in map.Keys)
            { SetBitmodVKMapping(key, map[key]); }
        }

        public void SetBitmodVKMapping(KbdLayers bm, VK vk)
        {
            switch (bm)
            {
                case KbdLayers.KBDSHIFT:
                    TxtBitmodShift.Text = Enum.GetName<VK>(vk);
                    break;
                case KbdLayers.KBDCTRL:
                    TxtBitmodCtrl.Text = Enum.GetName<VK>(vk);
                    break;
                case KbdLayers.KBDALT:
                    TxtBitmodAlt.Text = Enum.GetName<VK>(vk);
                    break;
                case KbdLayers.KBDKANA:
                    TxtBitmodKana.Text = Enum.GetName<VK>(vk);
                    break;
                case KbdLayers.KBDROYA:
                    TxtBitmodRoya.Text = Enum.GetName<VK>(vk);
                    break;
                case KbdLayers.KBDLOYA:
                    TxtBitmodLoya.Text = Enum.GetName<VK>(vk);
                    break;
                case KbdLayers.KBDCUSTOM:
                    TxtBitmodCustom.Text = Enum.GetName<VK>(vk);
                    break;
                case KbdLayers.KBDGRPSELTAP:
                    TxtBitmodGrpseltab.Text = Enum.GetName<VK>(vk);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region constructor
        public FormProperties()
        {
            InitializeComponent();
            CbbKeyboardType.Select(1, 1);

            foreach (string item in Enum.GetNames(typeof(VK)))
            { autoCompleteData.Add(item); }

            void setAutoComplete(TextBox txt)
            {
                txt.AutoCompleteMode = AutoCompleteMode.Append;
                txt.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txt.AutoCompleteCustomSource = autoCompleteData;
            }
            setAutoComplete(TxtBitmodShift);
            setAutoComplete(TxtBitmodCtrl);
            setAutoComplete(TxtBitmodAlt);
            setAutoComplete(TxtBitmodKana);
            setAutoComplete(TxtBitmodRoya);
            setAutoComplete(TxtBitmodLoya);
            setAutoComplete(TxtBitmodCustom);
            setAutoComplete(TxtBitmodGrpseltab);

            foreach (var languageCode in Enum.GetNames<LanguageCodes>().Order())
            { CbbLanguageCode.Items.Add(LanguageCodeNameToCbbString(languageCode)); }
        }
        #endregion

        #region event handlers
        private void FormProperties_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void TxtLayoutVersion_TextChanged(object sender, EventArgs e)
        {
            if (TxtLayoutVersion.Text.Length == 0)
            { return; }

            switch (TxtLayoutVersion.Text.Last())
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                case '.':
                    break;
                default:
                    TxtLayoutVersion.Text = TxtLayoutVersion.Text.TrimEnd(TxtLayoutVersion.Text.Last());
                    TxtLayoutVersion.SelectionStart = TxtLayoutVersion.Text.Length;
                    break;
            }
        }

        private void TxtLayoutDescription_TextChanged(object sender, EventArgs e)
        {
            TxtLayoutDescription.Text = TxtLayoutDescription.Text.Replace("\"", "").Replace("\'", "");
        }

        private void TxtLayoutVersion_Leave(object sender, EventArgs e)
        {
            TxtLayoutVersion.Text = TxtLayoutVersion.Text.TrimEnd('.');
        }
        #endregion

        #region Json Import Export
        const string exKeyboardLayout = "KeyboardLayout";
        const string exBitModMapping = "BitModMapping";
        const string exKllfAltGr = "KLLF_AltGr";
        const string exKllfShiftlock = "KLLF_Shiftlock";
        const string exKllfLrmRml = "KLLF_LrmRml";
        const string exLayoutFileDescription = "LayoutFileDescription";
        const string exLayoutVersion = "LayoutVersion";
        const string exLayoutName = "LayoutName";
        const string exLayoutLanguageCode = "LayoutLanguageCode";

        internal JsonObject JsonExport()
        {
            var exp = new JsonObject
            {
                { exKeyboardLayout, JsonSerializer.SerializeToNode(KbdLayoutType) },
                { exBitModMapping, JsonSerializer.SerializeToNode(GetBitmodVKMapping())},
                { exKllfAltGr, JsonSerializer.SerializeToNode(KLLF_AltGr)},
                { exKllfShiftlock, JsonSerializer.SerializeToNode(KLLF_Shiftlock)},
                { exKllfLrmRml, JsonSerializer.SerializeToNode(KLLF_LrmRml)},
                { exLayoutFileDescription, JsonSerializer.SerializeToNode(KbdLayoutDescriptionText)},
                { exLayoutVersion, JsonSerializer.SerializeToNode(KbdLayoutVersion)},
                { exLayoutName, JsonSerializer.SerializeToNode(KbdLayoutName)},
                { exLayoutLanguageCode, JsonSerializer.SerializeToNode(
                    CbbLanguageCode.SelectedIndex > 0 ? CbbLanguageCode.Items[CbbLanguageCode.SelectedIndex]?.ToString() : ""
                    )}
            };
            return exp;
        }

        internal void JsonImport(JsonNode? data)
        {
            if (data == null)
            { return; }
            if (data.GetValueKind() != JsonValueKind.Object)
            { return; }

            var dataobj = data!.AsObject();

            foreach (var val in dataobj)
            {
                switch (val.Key)
                {
                    case exKeyboardLayout:
                        KbdLayoutType = val.Value.Deserialize<int>();
                        break;
                    case exBitModMapping:
                        var dic = val.Value.Deserialize<Dictionary<KbdLayers, VK>>();
                        if (dic != null)
                        { SetBitmodVKMapping(dic); }
                        break;
                    case exKllfAltGr:
                        KLLF_AltGr = val.Value.Deserialize<bool>();
                        break;
                    case exKllfShiftlock:
                        KLLF_Shiftlock = val.Value.Deserialize<bool>();
                        break;
                    case exKllfLrmRml:
                        KLLF_LrmRml = val.Value.Deserialize<bool>();
                        break;
                    case exLayoutFileDescription:
                        KbdLayoutDescriptionText = val.Value.Deserialize<string>()!;
                        break;
                    case exLayoutVersion:
                        KbdLayoutVersion = val.Value.Deserialize<string>()!;
                        break;
                    case exLayoutName:
                        KbdLayoutName = val.Value.Deserialize<string>()!;
                        break;
                    case exLayoutLanguageCode:
                        CbbLanguageCode.SelectedIndex = CbbLanguageCode.FindStringExact(val.Value.Deserialize<string>()!);
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
    }
}
