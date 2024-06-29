using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using System.DirectoryServices;
using System.Security.Cryptography;
using System.Windows.Forms;
using System;

namespace KiWi_Keyboard_Layout_Creator
{
    /// <summary>
    /// these enums are mostly from kbd.h
    /// </summary>

    //https://learn.microsoft.com/en-us/windows-hardware/manufacture/desktop/windows-language-pack-default-values?view=windows-11
    public enum LanguageCodes
    {
#pragma warning disable CA1069 // Enums values should not be duplicated warning disabled
        ADLaM                        = 0x0C00,
        Albanian                     = 0x041C,
        Arabic                       = 0x0401,
        Armenian                     = 0x042B,
        Assamese                     = 0x044D,
        Azerbaijani                  = 0x042C,
        AzerbaijaniCyrillic          = 0x082C,
        Bangla                       = 0x0445,
        Bashkir 	                 = 0x046D,
        Belarusian 	                 = 0x0423,
        BelgianPeriod                = 0x0813,
        BelgianFrench                = 0x080C,
        Bosnian                      = 0x201A,
        Buginese                     = 0x0C00,
        Bulgarian                    = 0x0402,
        CanadianFrench               = 0x1009,
        CanadianFrenchLegacy         = 0x0C0C,
        CentralAtlasTamazight        = 0x085F,
        CentralKurdish               = 0x0492,
        CherokeeNation               = 0x045C,
        ChineseSimplifiedUS          = 0x0804,
        ChineseSimplifiedSingaporeUS = 0x1004,
        ChineseTraditionalUS         = 0x1404,
        ChineseTraditionalHongKongUS = 0x0C04,
        Czech                        = 0x0405,
        Danish                       = 0x0406,
        DevanagariINSCRIPT           = 0x0439,
        DivehiPhonetic               = 0x0465,
        Dutch                        = 0x0413,
        Dzongkha                     = 0x0C51,
        EnglishIndia                 = 0x4009,
        Estonian                     = 0x0425,
        Faeroese                     = 0x0438,
        Finnish                      = 0x040B,
        FinnishWithSami              = 0x083B,
        French                       = 0x040C,
        Futhark                      = 0x0C00,
        Georgian                     = 0x0437,
        German                       = 0x0407,
        Gothic                       = 0x0C00,
        Greek                        = 0x0408,
        Greenlandic                  = 0x046F,
        Guarani                      = 0x0474,
        Gujarati                     = 0x0447,
        Hausa                        = 0x0468,
        Hawaiian                     = 0x0475,
        Hebrew                       = 0x040D,
        HindiTraditional             = 0x0439,
        Hungarian                    = 0x040E,
        Icelandic                    = 0x040F,
        Igbo                         = 0x0470,
        InuktitutLatin               = 0x085D,
        InuktitutNaqittaut           = 0x045D,
        Irish                        = 0x1809,
        Italian                      = 0x0410,
        Japanese                     = 0x0411,
        Javanese                     = 0x0C00,
        Kannada                      = 0x044B,
        Kazakh                       = 0x043F,
        Khmer                        = 0x0453,
        Korean                       = 0x0412,
        KyrgyzCyrillic               = 0x0440,
        Lao                          = 0x0454,
        LatinAmerican                = 0x080A,
        Latvian                      = 0x0426,
        Lisu                         = 0x0C00,
        Lithuanian                   = 0x0427,
        Luxembourgish                = 0x046E,
        Macedonian                   = 0x042F,
        Malayalam                    = 0x044C,
        Maltese                      = 0x043A,
        Maori                        = 0x0481,
        Marathi                      = 0x044E,
        MongolianCyrillic            = 0x0450,
        MongolianMongolianScript     = 0x0850,
        Myanmar                      = 0x0C00,
        NZAotearoa                   = 0x1409,
        Nepali                       = 0x0461,
        NewTaiLue                    = 0x0C00,
        Norwegian                    = 0x0414,
        NorwegianwithSami            = 0x043B,
        NKo                          = 0x0C00,
        Odia                         = 0x0448,
        Ogham                        = 0x0C00,
        OlChiki                      = 0x0C00,
        OldItalic                    = 0x0C00,
        Osage                        = 0x0C00,
        Osmanya                      = 0x0C00,
        PashtoAfghanistan            = 0x0463,
        Persian                      = 0x0429,
        Phagspa                      = 0x0C00,
        Polish                       = 0x0415,
        Portuguese                   = 0x0816,
        PortugueseBrazil             = 0x0416,
        Punjabi                      = 0x0446,
        Romanian                     = 0x0418,
        Russian                      = 0x0419,
        Sakha                        = 0x0485,
        SamiFinlandSweden            = 0x083B,
        SamiNorway                   = 0x043B,
        ScottishGaelic               = 0x1809,
        SerbianCyrillic              = 0x0C1A,
        SerbianLatin                 = 0x081A,
        SesothoSaLeboa               = 0x046C,
        Setswana                     = 0x0432,
        Sinhala                      = 0x045B,
        Slovak                       = 0x041B,
        Slovenian                    = 0x0424,
        Sora                         = 0x0C00,
        Sorbian                      = 0x042E,
        Spanish                      = 0x040A,
        Standard                     = 0x041A,
        Swedish                      = 0x041D,
        SwedishWithSami              = 0x083B,
        SwissFrench                  = 0x100C,
        SwissGerman                  = 0x0807,
        Syriac                       = 0x045A,
        TaiLe                        = 0x0C00,
        Tajik                        = 0x0428,
        Tamil                        = 0x0449,
        Tatar                        = 0x0444,
        Telugu                       = 0x044A,
        ThaiKedmanee                 = 0x041E,
        Tibetan                      = 0x0451,
        Tifinagh                     = 0x105F,
        Turkish                      = 0x041F,
        Turkmen                      = 0x0442,
        US                           = 0x0409,
        Ukrainian                    = 0x0422,
        UnitedKingdom                = 0x0809,
        UnitedKingdomExtended        = 0x0452,
        Urdu                         = 0x0420,
        Uyghur                       = 0x0480,
        UzbekCyrillic                = 0x0843,
        Vietnamese                   = 0x042A,
        Wolof                        = 0x0488,
        Yoruba                       = 0x046A,
#pragma warning restore CA1069 // Enums values should not be duplicated
    }

    public struct KeycapSizesUnits
    {
        public const double Normal = 1;
        public const double Ctrl = 1.25;
        public const double LCtrl = 1.25;
        public const double RCtrl = 1.25;
        public const double Win = 1.25;
        public const double LWin = 1.25;
        public const double RWin = 1.25;
        public const double Alt = 1.25;
        public const double LAlt = 1.25;
        public const double RAlt = 1.25;
        public const double RMenu = 1.25;
        public const double Tab = 1.5;
        public const double Capital = 1.75;
        public const double Backspace = 2;
        public const double LShift = 2.25;
        public const double Enter = 2.25;
        public const double RShift = 2.75;
        public const double Spacebar = 6.25;
        public const double NumpadEnterHeight = 2.0;

    }

    public enum KbdLayers
    {
        KBDBASE = 0x00,
        KBDSHIFT = 0x01,
        KBDCTRL = 0x02,
        KBDALT = 0x04,
        // three symbols KANA, ROYA, LOYA are for FE
        KBDKANA = 0x08,
        KBDROYA = 0x10,
        KBDLOYA = 0x20,
        KBDCUSTOM = 0x40,
        KBDGRPSELTAP = 0x80 //theory what this is: https://www.unicode.org/mail-arch/unicode-ml/y2016-m11/0045.html GRouP SELecTor APing?
    }

    internal static class LayersFunc
    {
        internal static List<KbdLayers> GetEnumsFromLayer(int Bitmask)
        {
            List<KbdLayers> mods = [];

            foreach (KbdLayers bit in Enum.GetValues<KbdLayers>())
            {
                if ((Bitmask & ((int)bit)) == (int)bit)
                {
                    mods.Add(bit);
                }
            }
            return mods;
        }
    }

    public enum LockFlags
    {
        CAPLOK = 0x01,
        SGCAPS = 0x02,
        CAPLOKALTGR = 0x04,
        KANALOK = 0x08,
        GRPSELTAP = 0x80
    }

    public enum VK
    {
#pragma warning disable CA1069 // Enums values should not be duplicated warning disabled
        /* * Virtual Keys, Standard Set  */
        //ZERO = 0x00, //added this myself, to maybe use this as a nlf-activator, but that did not work, windows doesnt seem to register this as a vk
        LBUTTON = 0x01,
        RBUTTON = 0x02,
        CANCEL = 0x03,
        MBUTTON = 0x04,    /* NOT contiguous with L & RBUTTON */
        XBUTTON1 = 0x05,    /* NOT contiguous with L & RBUTTON */
        XBUTTON2 = 0x06,    /* NOT contiguous with L & RBUTTON */
        /* * 0x07 : reserved */
        BACK = 0x08,
        TAB = 0x09,
        /* * 0x0A - 0x0B : reserved */
        CLEAR = 0x0C,
        RETURN = 0x0D,
        /* * 0x0E - 0x0F : unassigned */
        SHIFT = 0x10,
        CONTROL = 0x11,
        MENU = 0x12,
        PAUSE = 0x13,
        CAPITAL = 0x14,
        KANA = 0x15,
        HANGUL = 0x15,
        HANGEUL = 0x15,
        IME_ON = 0x16,
        JUNJA = 0x17,
        FINAL = 0x18,
        HANJA = 0x19,
        KANJI = 0x19,
        IME_OFF = 0x1A,
        ESCAPE = 0x1B,
        CONVERT = 0x1C,
        NONCONVERT = 0x1D,
        ACCEPT = 0x1E,
        MODECHANGE = 0x1F,
        SPACE = 0x20,
        PRIOR = 0x21,
        NEXT = 0x22,
        END = 0x23,
        HOME = 0x24,
        LEFT = 0x25,
        UP = 0x26,
        RIGHT = 0x27,
        DOWN = 0x28,
        SELECT = 0x29,
        PRINT = 0x2A,
        EXECUTE = 0x2B,
        SNAPSHOT = 0x2C,
        INSERT = 0x2D,
        DELETE = 0x2E,
        HELP = 0x2F,
        /* * 0 - 9 are the same as ASCII '0' - '9' (0x30 - 0x39)
        * 0x3A - 0x40 : unassigned
        * A - Z are the same as ASCII 'A' - 'Z' (0x41 - 0x5A) */
        NMBR_0 = '0',
        NMBR_1 = '1',
        NMBR_2 = '2',
        NMBR_3 = '3',
        NMBR_4 = '4',
        NMBR_5 = '5',
        NMBR_6 = '6',
        NMBR_7 = '7',
        NMBR_8 = '8',
        NMBR_9 = '9',
        A = 'A',
        B = 'B',
        C = 'C',
        D = 'D',
        E = 'E',
        F = 'F',
        G = 'G',
        H = 'H',
        I = 'I',
        J = 'J',
        K = 'K',
        L = 'L',
        M = 'M',
        N = 'N',
        O = 'O',
        P = 'P',
        Q = 'Q',
        R = 'R',
        S = 'S',
        T = 'T',
        U = 'U',
        V = 'V',
        W = 'W',
        X = 'X',
        Y = 'Y',
        Z = 'Z',

        LWIN = 0x5B,
        RWIN = 0x5C,
        APPS = 0x5D,
        /* * 0x5E : reserved */
        SLEEP = 0x5F,
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
        MULTIPLY = 0x6A,
        ADD = 0x6B,
        SEPARATOR = 0x6C,
        SUBTRACT = 0x6D,
        DECIMAL = 0x6E,
        DIVIDE = 0x6F,
        F1 = 0x70,
        F2 = 0x71,
        F3 = 0x72,
        F4 = 0x73,
        F5 = 0x74,
        F6 = 0x75,
        F7 = 0x76,
        F8 = 0x77,
        F9 = 0x78,
        F10 = 0x79,
        F11 = 0x7A,
        F12 = 0x7B,
        F13 = 0x7C,
        F14 = 0x7D,
        F15 = 0x7E,
        F16 = 0x7F,
        F17 = 0x80,
        F18 = 0x81,
        F19 = 0x82,
        F20 = 0x83,
        F21 = 0x84,
        F22 = 0x85,
        F23 = 0x86,
        F24 = 0x87,
        /* * 0x88 - 0x8F : UI navigation */
        NAVIGATION_VIEW = 0x88, // reserved
        NAVIGATION_MENU = 0x89, // reserved
        NAVIGATION_UP = 0x8A, // reserved
        NAVIGATION_DOWN = 0x8B, // reserved
        NAVIGATION_LEFT = 0x8C, // reserved
        NAVIGATION_RIGHT = 0x8D, // reserved
        NAVIGATION_ACCEPT = 0x8E, // reserved
        NAVIGATION_CANCEL = 0x8F, // reserved
        NUMLOCK = 0x90,
        SCROLL = 0x91,
        /* * NEC PC-9800 kbd definitions*/
        OEM_NEC_EQUAL = 0x92,   // '=' key on numpad
        /* * Fujitsu/OASYS kbd definitions*/
        OEM_FJ_JISHO = 0x92, // 'Dictionary' key
        OEM_FJ_MASSHOU = 0x93, // 'Unregister word' key
        OEM_FJ_TOUROKU = 0x94, // 'Register word' key
        OEM_FJ_LOYA = 0x95, // 'Left OYAYUBI' key
        OEM_FJ_ROYA = 0x96, // 'Right OYAYUBI' key
        /* * 0x97 - 0x9F : unassigned
         * there are more unassigned spots left, 
         * but it seems to be customary to leave a space of one bit and I think having 7 to play with is enough */
        UNASSIGNED1 = 0x98,
        UNASSIGNED2 = 0x99,
        UNASSIGNED3 = 0x9A,
        UNASSIGNED4 = 0x9B,
        UNASSIGNED5 = 0x9C,
        UNASSIGNED6 = 0x9D,
        CUSTOM = 0x9E,// since i want to support a custom_Bitmod and i need some VK to shift to that layer, i am claiming this one
        /* * L* & R* - left and right Alt, Ctrl and Shift virtual keys.
        * Used only as parameters to GetAsyncKeyState() and GetKeyState().
        * No other API or message will distinguish left and right keys in this way. */
        LSHIFT = 0xA0,
        RSHIFT = 0xA1,
        LCONTROL = 0xA2,
        RCONTROL = 0xA3,
        LMENU = 0xA4,
        RMENU = 0xA5,
        BROWSER_BACK = 0xA6,
        BROWSER_FORWARD = 0xA7,
        BROWSER_REFRESH = 0xA8,
        BROWSER_STOP = 0xA9,
        BROWSER_SEARCH = 0xAA,
        BROWSER_FAVORITES = 0xAB,
        BROWSER_HOME = 0xAC,
        VOLUME_MUTE = 0xAD,
        VOLUME_DOWN = 0xAE,
        VOLUME_UP = 0xAF,
        MEDIA_NEXT_TRACK = 0xB0,
        MEDIA_PREV_TRACK = 0xB1,
        MEDIA_STOP = 0xB2,
        MEDIA_PLAY_PAUSE = 0xB3,
        LAUNCH_MAIL = 0xB4,
        LAUNCH_MEDIA_SELECT = 0xB5,
        LAUNCH_APP1 = 0xB6,
        LAUNCH_APP2 = 0xB7,
        /* * 0xB8 - 0xB9 : reserved */
        OEM_1 = 0xBA,   // ';:' for US
        OEM_PLUS = 0xBB,   // '+' any country
        OEM_COMMA = 0xBC,   // ',' any country
        OEM_MINUS = 0xBD,   // '-' any country
        OEM_PERIOD = 0xBE,   // '.' any country
        OEM_2 = 0xBF,   // '/?' for US
        OEM_3 = 0xC0,   // '`~' for US
        /* * 0xC1 - 0xC2 : reserved */
        /* * 0xC3 - 0xDA : Gamepad input */
        GAMEPAD_A = 0xC3, // reserved
        GAMEPAD_B = 0xC4, // reserved
        GAMEPAD_X = 0xC5, // reserved
        GAMEPAD_Y = 0xC6, // reserved
        GAMEPAD_RIGHT_SHOULDER = 0xC7, // reserved
        GAMEPAD_LEFT_SHOULDER = 0xC8, // reserved
        GAMEPAD_LEFT_TRIGGER = 0xC9, // reserved
        GAMEPAD_RIGHT_TRIGGER = 0xCA, // reserved
        GAMEPAD_DPAD_UP = 0xCB, // reserved
        GAMEPAD_DPAD_DOWN = 0xCC, // reserved
        GAMEPAD_DPAD_LEFT = 0xCD, // reserved
        GAMEPAD_DPAD_RIGHT = 0xCE, // reserved
        GAMEPAD_MENU = 0xCF, // reserved
        GAMEPAD_VIEW = 0xD0, // reserved
        GAMEPAD_LEFT_THUMBSTICK_BUTTON = 0xD1, // reserved
        GAMEPAD_RIGHT_THUMBSTICK_BUTTON = 0xD2, // reserved
        GAMEPAD_LEFT_THUMBSTICK_UP = 0xD3, // reserved
        GAMEPAD_LEFT_THUMBSTICK_DOWN = 0xD4, // reserved
        GAMEPAD_LEFT_THUMBSTICK_RIGHT = 0xD5, // reserved
        GAMEPAD_LEFT_THUMBSTICK_LEFT = 0xD6, // reserved
        GAMEPAD_RIGHT_THUMBSTICK_UP = 0xD7, // reserved
        GAMEPAD_RIGHT_THUMBSTICK_DOWN = 0xD8, // reserved
        GAMEPAD_RIGHT_THUMBSTICK_RIGHT = 0xD9, // reserved
        GAMEPAD_RIGHT_THUMBSTICK_LEFT = 0xDA, // reserved
        OEM_4 = 0xDB, //  '[{' for US
        OEM_5 = 0xDC, //  '\|' for US
        OEM_6 = 0xDD, //  ']}' for US
        OEM_7 = 0xDE, //  ''"' for US
        OEM_8 = 0xDF,
        /* * 0xE0 : reserved */
        /* * Various extended or enhanced keyboards */
        OEM_AX = 0xE1,  //  'AX' key on Japanese AX kbd
        OEM_102 = 0xE2,  //  "<>" or "\|" on RT 102-key kbd.
        ICO_HELP = 0xE3,  //  Help key on ICO
        ICO_00 = 0xE4,  //  00 key on ICO
        PROCESSKEY = 0xE5,
        ICO_CLEAR = 0xE6,
        /* * 0xE8 : unassigned */
        /* * Nokia/Ericsson definitions */
        OEM_RESET = 0xE9,
        OEM_JUMP = 0xEA,
        OEM_PA1 = 0xEB,
        OEM_PA2 = 0xEC,
        OEM_PA3 = 0xED,
        OEM_WSCTRL = 0xEE,
        OEM_CUSEL = 0xEF,
        OEM_ATTN = 0xF0,
        OEM_FINISH = 0xF1,
        OEM_COPY = 0xF2,
        OEM_AUTO = 0xF3,
        OEM_ENLW = 0xF4,
        OEM_BACKTAB = 0xF5,
        ATTN = 0xF6,
        CRSEL = 0xF7,
        EXSEL = 0xF8,
        EREOF = 0xF9,
        PLAY = 0xFA,
        ZOOM = 0xFB,
        NONAME = 0xFC,
        PA1 = 0xFD,
        OEM_CLEAR = 0xFE,
        _NONE__ = 0xFF
        /* * 0xFF : reserved */
#pragma warning restore CA1069 // Enums values should not be duplicated
    }

    public static class VKFunc
    {
        internal static VK? StringToVK(string str)
        {
            VK vk;

            string strVk = str.ToUpper();
            if (strVk.Length == 1 && int.TryParse(strVk, out _))
            {
                if (Enum.TryParse("NMBR_" + strVk, out vk))
                { return vk; }
                else
                { return null; }
            }
            else if (Enum.TryParse(strVk, out vk))
            { return vk; }

            return null;
        }

        internal static VK? FixTxtToVK(TextBox txtBox)
        {
            VK vk;

            txtBox.Text = txtBox.Text.ToUpper();
            if (txtBox.Text.Length == 1 && int.TryParse(txtBox.Text, out _))
            {
                if (Enum.TryParse("NMBR_" + txtBox.Text, out vk))
                { return vk; }
            }
            else if (Enum.TryParse(txtBox.Text, out vk))
            { return vk; }

            txtBox.Text = "";
            return null;
        }

        internal static string VKToString(VK? Vk)
        {
            if (Vk == null)
            { return ""; }

            string res = ((VK)Vk).ToString();

            if (res.Length == 6)
            {
                if (res[..5] == "NMBR_")
                {
                    return res[^1].ToString();
                }
            }
            return res;
        }
    }

    public enum VKHandlingMask
    {
        KBDEXT = 0x0100,
        KBDMULTIVK = 0x0200,
        KBDSPECIAL = 0x0400,
        KBDNUMPAD = 0x0800
    }

    public enum KeyboardTypes
    {
        GENERIC_101 = 4,
        JAPAN = 7,
        KOREA = 8,
        UNKNOWN = 0x51
    }

    public enum KLLF
    {
        KLLF_ALTGR = 0x0001, // if checked right alt acts as AltGr (Alt+Gr)
        KLLF_SHIFTLOCK = 0x0002, // pressing shift turns capslock off
        KLLF_LRM_RLM = 0x0004 // if checked Shift+backspace toggles right-to-left mode
    }

    public enum NlsType
    {
        NULL = 0, // Invalid function
        NOEVENT = 1, // Drop keyevent
        SEND_BASE_VK = 2, // Send Base VK_xxx
        SEND_PARAM_VK = 3, // Send Parameter VK_xxx
        KANALOCK = 4, // VK_KANA (with hardware lock)
        ALPHANUM = 5, // VK_DBE_ALPHANUMERIC
        HIRAGANA = 6, // VK_DBE_HIRAGANA
        KATAKANA = 7, // VK_DBE_KATAKANA
        SBCSDBCS = 8, // VK_DBE_SBCSCHAR/VK_DBE_DBCSCHAR
        ROMAN = 9, // VK_DBE_ROMAN/VK_DBE_NOROMAN
        CODEINPUT = 10, // VK_DBE_CODEINPUT/VK_DBE_NOCODEINPUT
        HELP_OR_END = 11, // VK_HELP or VK_END [NEC PC-9800 Only]
        HOME_OR_CLEAR = 12, // VK_HOME or VK_CLEAR [NEC PC-9800 Only]
        NUMPAD = 13, // VK_NUMPAD? for Numpad key [NEC PC-9800 Only]
        KANAEVENT = 14, // VK_KANA [Fujitsu FMV oyayubi Only]
        CONV_OR_NONCONV = 15 // VK_CONVERT and VK_NONCONVERT [Fujitsu FMV oyayubi Only]
    }


    public enum KBDNLS_TYPE
    {
        NULL = 0,
        NORMAL = 1, // dont know what this does for normal keys the choice here doesnt seem to matter, maybe it does for Layer-Shifts like VK_Kana
        TOGGLE = 2, // this seems to be what I would call normal
    }

    /// <summary>
    /// dont know what these do
    /// </summary>
    public enum KBDNLS_INDEX
    {
        NORMAL = 1,
        ALT = 2,
    }
}
