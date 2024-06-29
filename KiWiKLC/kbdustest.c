//---------------------------------------------------------------------------
// basic pattern of a kbd*.c file
// kommented parts show one example, followed by a comment, that will be replaced by the data from the Layout Creator
// Made by Ki.Wi.
//---------------------------------------------------------------------------

// #define KBD_TYPE 4
#define KBD_TYPE 4

#include <windows.h>
#include <kbd.h>

#if defined(_M_IA64)
#pragma section(".data")
#define ALLOC_SECTION_LDATA __declspec(allocate(".data"))
#else
#pragma data_seg(".data")
#define ALLOC_SECTION_LDATA
#endif



//---------------------------------------------------------------------------
// Scan codes to key names
//---------------------------------------------------------------------------

static ALLOC_SECTION_LDATA VSC_LPWSTR key_names[] = {
    //{scancode, keyname}, 
    {0x01, L"ESCAPE"}, //VK:1B
    {0x02, L"1"}, //VK:31
    {0x03, L"2"}, //VK:32
    {0x04, L"3"}, //VK:33
    {0x05, L"4"}, //VK:34
    {0x06, L"5"}, //VK:35
    {0x07, L"6"}, //VK:36
    {0x08, L"7"}, //VK:37
    {0x09, L"8"}, //VK:38
    {0x0A, L"9"}, //VK:39
    {0x0B, L"0"}, //VK:30
    {0x0C, L"OEM_MINUS"}, //VK:BD
    {0x0D, L"OEM_PLUS"}, //VK:BB
    {0x0E, L"BACK"}, //VK:08
    {0x0F, L"TAB"}, //VK:09
    {0x10, L"Q"}, //VK:51
    {0x11, L"W"}, //VK:57
    {0x12, L"E"}, //VK:45
    {0x13, L"R"}, //VK:52
    {0x14, L"T"}, //VK:54
    {0x15, L"Y"}, //VK:59
    {0x16, L"U"}, //VK:55
    {0x17, L"I"}, //VK:49
    {0x18, L"O"}, //VK:4F
    {0x19, L"P"}, //VK:50
    {0x1A, L"OEM_4"}, //VK:DB
    {0x1B, L"OEM_6"}, //VK:DD
    {0x1C, L"RETURN"}, //VK:0D
    {0x1D, L"LCONTROL"}, //VK:A2
    {0x1E, L"A"}, //VK:41
    {0x1F, L"S"}, //VK:53
    {0x20, L"D"}, //VK:44
    {0x21, L"F"}, //VK:46
    {0x22, L"G"}, //VK:47
    {0x23, L"H"}, //VK:48
    {0x24, L"J"}, //VK:4A
    {0x25, L"K"}, //VK:4B
    {0x26, L"L"}, //VK:4C
    {0x27, L"OEM_1"}, //VK:BA
    {0x28, L"OEM_7"}, //VK:DE
    {0x29, L"OEM_3"}, //VK:C0
    {0x2A, L"LSHIFT"}, //VK:A0
    {0x2B, L"OEM_5"}, //VK:DC
    {0x2C, L"Z"}, //VK:5A
    {0x2D, L"X"}, //VK:58
    {0x2E, L"C"}, //VK:43
    {0x2F, L"V"}, //VK:56
    {0x30, L"B"}, //VK:42
    {0x31, L"N"}, //VK:4E
    {0x32, L"M"}, //VK:4D
    {0x33, L"OEM_COMMA"}, //VK:BC
    {0x34, L"OEM_PERIOD"}, //VK:BE
    {0x35, L"OEM_2"}, //VK:BF
    {0x36, L"RSHIFT"}, //VK:A1
    {0x37, L"MULTIPLY"}, //VK:6A
    {0x38, L"LMENU"}, //VK:A4
    {0x39, L"SPACE"}, //VK:20
    {0x3A, L"MenuEx"}, //VK:12
    {0x3B, L"F1"}, //VK:70
    {0x3C, L"F2"}, //VK:71
    {0x3D, L"F3"}, //VK:72
    {0x3E, L"F4"}, //VK:73
    {0x3F, L"F5"}, //VK:74
    {0x40, L"F6"}, //VK:75
    {0x41, L"F7"}, //VK:76
    {0x42, L"F8"}, //VK:77
    {0x43, L"F9"}, //VK:78
    {0x44, L"F10"}, //VK:79
    {0x45, L"NUMLOCK"}, //VK:90
    {0x46, L"SCROLL"}, //VK:91
    {0x47, L"HOME"}, //VK:24
    {0x48, L"UP"}, //VK:26
    {0x49, L"PRIOR"}, //VK:21
    {0x4A, L"SUBTRACT"}, //VK:6D
    {0x4B, L"LEFT"}, //VK:25
    {0x4C, L"CLEAR"}, //VK:0C
    {0x4D, L"RIGHT"}, //VK:27
    {0x4E, L"ADD"}, //VK:6B
    {0x4F, L"END"}, //VK:23
    {0x50, L"DOWN"}, //VK:28
    {0x51, L"NEXT"}, //VK:22
    {0x52, L"INSERT"}, //VK:2D
    {0x53, L"DELETE"}, //VK:2E
    {0x57, L"F11"}, //VK:7A
    {0x58, L"F12"}, //VK:7B
    {0x00, NULL}
};

//---------------------------------------------------------------------------
// Scan codes to key names (extended keypad)
//---------------------------------------------------------------------------

static ALLOC_SECTION_LDATA VSC_LPWSTR key_names_ext[] = {
    //{scancode, keyname}, 
    {0x1C, L"RETURN"}, //VK:0D
    {0x1D, L"RCONTROL"}, //VK:A3
    {0x35, L"DIVIDE"}, //VK:6F
    {0x37, L"SNAPSHOT"}, //VK:2C
    {0x38, L"AltGrCustom"}, //VK:9E
    {0x47, L"HOME"}, //VK:24
    {0x48, L"UP"}, //VK:26
    {0x49, L"PRIOR"}, //VK:21
    {0x4B, L"LEFT"}, //VK:25
    {0x4D, L"RIGHT"}, //VK:27
    {0x4F, L"END"}, //VK:23
    {0x50, L"DOWN"}, //VK:28
    {0x51, L"NEXT"}, //VK:22
    {0x52, L"INSERT"}, //VK:2D
    {0x53, L"DELETE"}, //VK:2E
    {0x5B, L"LWIN"}, //VK:5B
    {0x5C, L"RWIN"}, //VK:5C
    {0x5D, L"APPS"}, //VK:5D
    {0x00, NULL}
};

//---------------------------------------------------------------------------
// Names of dead keys
//---------------------------------------------------------------------------

static ALLOC_SECTION_LDATA DEADKEY_LPWSTR key_names_dead[] = {
    /*L"\x00b4" L"ACUTE ACCENT",*/
    /*key_names_dead*/
    NULL
};

//---------------------------------------------------------------------------
// Scan code to virtual key conversion table
//---------------------------------------------------------------------------

static ALLOC_SECTION_LDATA USHORT scancode_to_vk[] =
{
    ///* 00 */ VK__none_,
    ///* 01 */ VK_ESCAPE,
    ///* 02 */ '1',
    /*
     * Right-hand Shift key must have KBDEXT bit set.
     */
    ///* 36 */ VK_RSHIFT | KBDEXT,
    ///* 37 */ VK_MULTIPLY | KBDMULTIVK,               // numpad_* + Shift/Alt -> SnapShot
    /*
     * NumLock Key:
     *     KBDEXT     - VK_NUMLOCK is an Extended key
     *     KBDMULTIVK - VK_NUMLOCK or VK_PAUSE (without or with CTRL)
     */
    ///* 45 */ VK_NUMLOCK | KBDEXT | KBDMULTIVK,
    ///* 46 */ VK_SCROLL | KBDMULTIVK
    /*
     * Number Pad keys:
     *     KBDNUMPAD  - digits 0-9 and decimal point.
     *     KBDSPECIAL - require special processing by Windows
     */
    ///* 45 */ VK_NUMLOCK | KBDEXT | KBDMULTIVK,
    ///* 46 */ VK_SCROLL | KBDMULTIVK,
    ///* 47 */ VK_HOME | KBDSPECIAL | KBDNUMPAD,
    ///* 48 */ VK_UP | KBDSPECIAL | KBDNUMPAD,
    ///* 49 */ VK_PRIOR | KBDSPECIAL | KBDNUMPAD,
    
    VK__none_,
    0x1B,//Scancode:1 Keyname:ESCAPE
    0x31,//Scancode:2 Keyname:1
    0x32,//Scancode:3 Keyname:2
    0x33,//Scancode:4 Keyname:3
    0x34,//Scancode:5 Keyname:4
    0x35,//Scancode:6 Keyname:5
    0x36,//Scancode:7 Keyname:6
    0x37,//Scancode:8 Keyname:7
    0x38,//Scancode:9 Keyname:8
    0x39,//Scancode:10 Keyname:9
    0x30,//Scancode:11 Keyname:0
    0xBD,//Scancode:12 Keyname:OEM_MINUS
    0xBB,//Scancode:13 Keyname:OEM_PLUS
    0x08,//Scancode:14 Keyname:BACK
    0x09,//Scancode:15 Keyname:TAB
    0x51,//Scancode:16 Keyname:Q
    0x57,//Scancode:17 Keyname:W
    0x45,//Scancode:18 Keyname:E
    0x52,//Scancode:19 Keyname:R
    0x54,//Scancode:20 Keyname:T
    0x59 | 0x400,//Scancode:21 Keyname:Y
    0x55 | 0x400,//Scancode:22 Keyname:U
    0x49 | 0x400,//Scancode:23 Keyname:I
    0x4F | 0x400,//Scancode:24 Keyname:O
    0x50 | 0x400,//Scancode:25 Keyname:P
    0xDB,//Scancode:26 Keyname:OEM_4
    0xDD,//Scancode:27 Keyname:OEM_6
    0x0D,//Scancode:28 Keyname:RETURN
    0xA2,//Scancode:29 Keyname:LCONTROL
    0x41,//Scancode:30 Keyname:A
    0x53,//Scancode:31 Keyname:S
    0x44 | 0x400,//Scancode:32 Keyname:D
    0x46 | 0x400,//Scancode:33 Keyname:F
    0x47,//Scancode:34 Keyname:G
    0x48 | 0x400,//Scancode:35 Keyname:H
    0x4A | 0x400,//Scancode:36 Keyname:J
    0x4B | 0x400,//Scancode:37 Keyname:K
    0x4C | 0x400,//Scancode:38 Keyname:L
    0xBA | 0x400,//Scancode:39 Keyname:OEM_1
    0xDE,//Scancode:40 Keyname:OEM_7
    0xC0 | 0x400,//Scancode:41 Keyname:OEM_3
    0xA0,//Scancode:42 Keyname:LSHIFT
    0xDC,//Scancode:43 Keyname:OEM_5
    0x5A,//Scancode:44 Keyname:Z
    0x58,//Scancode:45 Keyname:X
    0x43,//Scancode:46 Keyname:C
    0x56,//Scancode:47 Keyname:V
    0x42,//Scancode:48 Keyname:B
    0x4E,//Scancode:49 Keyname:N
    0x4D,//Scancode:50 Keyname:M
    0xBC,//Scancode:51 Keyname:OEM_COMMA
    0xBE,//Scancode:52 Keyname:OEM_PERIOD
    0xBF,//Scancode:53 Keyname:OEM_2
    0xA1 | 0x100,//Scancode:54 Keyname:RSHIFT
    0x6A | 0x200,//Scancode:55 Keyname:MULTIPLY
    0xA4,//Scancode:56 Keyname:LMENU
    0x20,//Scancode:57 Keyname:SPACE
    0x12 | 0x400,//Scancode:58 Keyname:MenuEx
    0x70,//Scancode:59 Keyname:F1
    0x71,//Scancode:60 Keyname:F2
    0x72,//Scancode:61 Keyname:F3
    0x73,//Scancode:62 Keyname:F4
    0x74,//Scancode:63 Keyname:F5
    0x75,//Scancode:64 Keyname:F6
    0x76,//Scancode:65 Keyname:F7
    0x77,//Scancode:66 Keyname:F8
    0x78,//Scancode:67 Keyname:F9
    0x79,//Scancode:68 Keyname:F10
    0x90 | 0x300,//Scancode:69 Keyname:NUMLOCK
    0x91 | 0x200,//Scancode:70 Keyname:SCROLL
    0x24 | 0xC00,//Scancode:71 Keyname:HOME
    0x26 | 0xC00,//Scancode:72 Keyname:UP
    0x21 | 0xC00,//Scancode:73 Keyname:PRIOR
    0x6D,//Scancode:74 Keyname:SUBTRACT
    0x25 | 0xC00,//Scancode:75 Keyname:LEFT
    0x0C | 0xC00,//Scancode:76 Keyname:CLEAR
    0x27 | 0xC00,//Scancode:77 Keyname:RIGHT
    0x6B,//Scancode:78 Keyname:ADD
    0x23 | 0xC00,//Scancode:79 Keyname:END
    0x28 | 0xC00,//Scancode:80 Keyname:DOWN
    0x22 | 0xC00,//Scancode:81 Keyname:NEXT
    0x2D | 0xC00,//Scancode:82 Keyname:INSERT
    0x2E | 0xC00,//Scancode:83 Keyname:DELETE
    VK__none_,//Scancode:84
    VK__none_,//Scancode:85
    VK__none_,//Scancode:86
    0x7A,//Scancode:87 Keyname:F11
    0x7B,//Scancode:88 Keyname:F12
};

//---------------------------------------------------------------------------
// Scan code to virtual key conversion table (scancodes with E0 prefix)
//---------------------------------------------------------------------------

static ALLOC_SECTION_LDATA VSC_VK scancode_to_vk_e0[] = {
    /*{0x10, VK_MEDIA_PREV_TRACK | KBDEXT},*/
    { 0x1C, 0x0D | 0x100},//Scancode:28 Keyname:RETURN
    { 0x1D, 0xA3 | 0x100},//Scancode:29 Keyname:RCONTROL
    { 0x35, 0x6F | 0x100},//Scancode:53 Keyname:DIVIDE
    { 0x37, 0x2C | 0x100},//Scancode:55 Keyname:SNAPSHOT
    { 0x38, 0x9E | 0x100},//Scancode:56 Keyname:AltGrCustom
    { 0x47, 0x24 | 0x100},//Scancode:71 Keyname:HOME
    { 0x48, 0x26 | 0x100},//Scancode:72 Keyname:UP
    { 0x49, 0x21 | 0x100},//Scancode:73 Keyname:PRIOR
    { 0x4B, 0x25 | 0x100},//Scancode:75 Keyname:LEFT
    { 0x4D, 0x27 | 0x100},//Scancode:77 Keyname:RIGHT
    { 0x4F, 0x23 | 0x100},//Scancode:79 Keyname:END
    { 0x50, 0x28 | 0x100},//Scancode:80 Keyname:DOWN
    { 0x51, 0x22 | 0x100},//Scancode:81 Keyname:NEXT
    { 0x52, 0x2D | 0x100},//Scancode:82 Keyname:INSERT
    { 0x53, 0x2E | 0x100},//Scancode:83 Keyname:DELETE
    { 0x5B, 0x5B | 0x100},//Scancode:91 Keyname:LWIN
    { 0x5C, 0x5C | 0x100},//Scancode:92 Keyname:RWIN
    { 0x5D, 0x5D | 0x100},//Scancode:93 Keyname:APPS
    {0x00, 0x0000}
};

//---------------------------------------------------------------------------
// Scan code to virtual key conversion table (scancodes with E1 prefix)
//---------------------------------------------------------------------------

static ALLOC_SECTION_LDATA VSC_VK scancode_to_vk_e1[] = {
    /*{0x1D, VK_PAUSE},*/
    {0x1D, VK_PAUSE},
     // i think this is a concept that was not followed further. Seems to always just be {0x1D, VK_PAUSE} only
    {0x00, 0x0000}
};

//---------------------------------------------------------------------------
// Associate a virtual key with a modifier bitmask
//---------------------------------------------------------------------------

static ALLOC_SECTION_LDATA VK_TO_BIT vk_to_bits[] = {
    /*{VK_SHIFT,   KBDSHIFT},*/
    {0x10, 0x01},//VK:SHIFT BitModifier:KBDSHIFT Bit:0001
    {0x11, 0x02},//VK:CONTROL BitModifier:KBDCTRL Bit:0010
    {0x12, 0x04},//VK:MENU BitModifier:KBDALT Bit:0100
    {0x9E, 0x40},//VK:CUSTOM BitModifier:KBDCUSTOM Bit:1000000
    {0, 0}
};

//---------------------------------------------------------------------------
// Map character modifier bits to modification number
//---------------------------------------------------------------------------

static ALLOC_SECTION_LDATA MODIFIERS char_modifiers = {
    .pVkToBit = vk_to_bits,
    .wMaxModBits = 65,
    .ModNumber = {
        0, // 0000 = no bitmod 
        1, // 0001
        6, // 0010
        SHFT_INVALID, // 0011
        7, // 0100
        SHFT_INVALID, // 0101
        3, // 0110
        5, // 0111
        SHFT_INVALID, // 1000
        SHFT_INVALID, // 1001
        SHFT_INVALID, // 1010
        SHFT_INVALID, // 1011
        SHFT_INVALID, // 1100
        SHFT_INVALID, // 1101
        SHFT_INVALID, // 1110
        SHFT_INVALID, // 1111
        SHFT_INVALID, // 10000
        SHFT_INVALID, // 10001
        SHFT_INVALID, // 10010
        SHFT_INVALID, // 10011
        SHFT_INVALID, // 10100
        SHFT_INVALID, // 10101
        SHFT_INVALID, // 10110
        SHFT_INVALID, // 10111
        SHFT_INVALID, // 11000
        SHFT_INVALID, // 11001
        SHFT_INVALID, // 11010
        SHFT_INVALID, // 11011
        SHFT_INVALID, // 11100
        SHFT_INVALID, // 11101
        SHFT_INVALID, // 11110
        SHFT_INVALID, // 11111
        SHFT_INVALID, // 100000
        SHFT_INVALID, // 100001
        SHFT_INVALID, // 100010
        SHFT_INVALID, // 100011
        SHFT_INVALID, // 100100
        SHFT_INVALID, // 100101
        SHFT_INVALID, // 100110
        SHFT_INVALID, // 100111
        SHFT_INVALID, // 101000
        SHFT_INVALID, // 101001
        SHFT_INVALID, // 101010
        SHFT_INVALID, // 101011
        SHFT_INVALID, // 101100
        SHFT_INVALID, // 101101
        SHFT_INVALID, // 101110
        SHFT_INVALID, // 101111
        SHFT_INVALID, // 110000
        SHFT_INVALID, // 110001
        SHFT_INVALID, // 110010
        SHFT_INVALID, // 110011
        SHFT_INVALID, // 110100
        SHFT_INVALID, // 110101
        SHFT_INVALID, // 110110
        SHFT_INVALID, // 110111
        SHFT_INVALID, // 111000
        SHFT_INVALID, // 111001
        SHFT_INVALID, // 111010
        SHFT_INVALID, // 111011
        SHFT_INVALID, // 111100
        SHFT_INVALID, // 111101
        SHFT_INVALID, // 111110
        SHFT_INVALID, // 111111
        2, // 1000000
        4, // 1000001
    }
};
//---------------------------------------------------------------------------
// Virtual Key to WCHAR translations for 5 shift states
//---------------------------------------------------------------------------

//static VK_TO_WCHARS5 vk_to_wchar5[] = {
//    //                                        Shift           Ctrl      AltGr                  Shift/AltGr
//    //                                        -----           ----      -----                  -----------
//    {'A',         CAPSLOCK,       L'a',      L'A',           WCH_NONE, UC_LOWER_A_RING_ABOVE, UC_UPPER_A_RING_ABOVE},
//    {VK_OEM_6,    CAPLOKALTGR,    WCH_DEAD,  WCH_DEAD,       WCH_NONE, UC_LOWER_O_CIRCUMFLEX, UC_UPPER_O_CIRCUMFLEX},
//    {VK__none_,   0x00,            L'^',     UC_DIAERESIS,   WCH_NONE, WCH_NONE,              WCH_NONE },
//    {0,             0,      0,           0,             0,        0,                     0}
//};

static ALLOC_SECTION_LDATA VK_TO_WCHARS1 vk_to_wchar1[] = { 
    {0x0D,	0x00,	0x000D},// KeyName: RETURN
    {0x20,	0x00,	0x0020},// KeyName: SPACE
    {0x6A,	0x00,	0x002A},// KeyName: MULTIPLY
    {0x6B,	0x00,	0x002B},// KeyName: ADD
    {0x6D,	0x00,	0x002D},// KeyName: SUBTRACT
    {0x6F,	0x00,	0x002F},// KeyName: DIVIDE
    {0,	0	,0}
};

static ALLOC_SECTION_LDATA VK_TO_WCHARS2 vk_to_wchar2[] = { 
    {0x42,	0x01,	0x0062,	0x0042},// KeyName: B
    {0x46,	0x01,	0x0066,	0x0046},// KeyName: F
    {0x47,	0x01,	0x0067,	0x0047},// KeyName: G
    {0x48,	0x01,	0x0068,	0x0048},// KeyName: H
    {0x4A,	0x01,	0x006A,	0x004A},// KeyName: J
    {0x4B,	0x01,	0x006B,	0x004B},// KeyName: K
    {0x56,	0x01,	0x0076,	0x0056},// KeyName: V
    {0x58,	0x01,	0x0078,	0x0058},// KeyName: X
    {0xBE,	0x00,	0x002E,	0x003E},// KeyName: OEM_PERIOD
    {0xC0,	0x00,	0x0060,	0x007E},// KeyName: OEM_3
    {0,	0	,0	,0}
};

static ALLOC_SECTION_LDATA VK_TO_WCHARS4 vk_to_wchar4[] = { 
    {0x30,	0x00,	0x0030,	0x0029,	0x2019,	0x2019},// KeyName: 0
    {0x32,	0x00,	0x0032,	0x0040,	0x00B2,	0x00B2},// KeyName: 2
    {0x33,	0x00,	0x0033,	0x0023,	0x00B3,	0x00B3},// KeyName: 3
    {0x35,	0x00,	0x0035,	0x0025,	0x20AC,	0x20AC},// KeyName: 5
    {0x36,	0x00,	0x0036,	0x005E,	0x00BC,	0x00BC},// KeyName: 6
    {0x37,	0x00,	0x0037,	0x0026,	0x00BD,	0x00BD},// KeyName: 7
    {0x38,	0x00,	0x0038,	0x002A,	0x00BE,	0x00BE},// KeyName: 8
    {0x39,	0x00,	0x0039,	0x0028,	0x2018,	0x2018},// KeyName: 9
    {0x4D,	0x01,	0x006D,	0x004D,	0x00B5,	0x00B5},// KeyName: M
    {0x52,	0x01,	0x0072,	0x0052,	0x00AE,	0x00AE},// KeyName: R
    {0xBD,	0x00,	0x002D,	0x005F,	0x00A5,	0x00A5},// KeyName: OEM_MINUS
    {0xBF,	0x00,	0x002F,	0x003F,	0x00BF,	0x00BF},// KeyName: OEM_2
    {0,	0	,0	,0	,0	,0}
};

static ALLOC_SECTION_LDATA VK_TO_WCHARS6 vk_to_wchar6[] = { 
    {0x31,	0x00,	0x0031,	0x0021,	0x00A1,	0x00A1,	0x00B9,	0x00B9},// KeyName: 1
    {0x34,	0x00,	0x0034,	0x0024,	0x00A4,	0x00A4,	0x00A3,	0x00A3},// KeyName: 4
    {0x41,	0x01,	0x0061,	0x0041,	0x00E1,	0x00E1,	0x00C1,	0x00C1},// KeyName: A
    {0x43,	0x01,	0x0063,	0x0043,	0x00A9,	0x00A9,	0x00A2,	0x00A2},// KeyName: C
    {0x44,	0x01,	0x0064,	0x0044,	0x00F0,	0x00F0,	0x00D0,	0x00D0},// KeyName: D
    {0x45,	0x01,	0x0065,	0x0045,	0x00E9,	0x00E9,	0x00C9,	0x00C9},// KeyName: E
    {0x49,	0x01,	0x0069,	0x0049,	0x00ED,	0x00ED,	0x00CD,	0x00CD},// KeyName: I
    {0x4C,	0x01,	0x006C,	0x004C,	0x00F8,	0x00F8,	0x00D8,	0x00D8},// KeyName: L
    {0x4E,	0x01,	0x006E,	0x004E,	0x00F1,	0x00F1,	0x00D1,	0x00D1},// KeyName: N
    {0x4F,	0x01,	0x006F,	0x004F,	0x00F3,	0x00F3,	0x00D3,	0x00D3},// KeyName: O
    {0x50,	0x01,	0x0070,	0x0050,	0x00F6,	0x00F6,	0x00D6,	0x00D6},// KeyName: P
    {0x51,	0x01,	0x0071,	0x0051,	0x00E4,	0x00E4,	0x00C4,	0x00C4},// KeyName: Q
    {0x53,	0x01,	0x0073,	0x0053,	0x00DF,	0x00DF,	0x00A7,	0x00A7},// KeyName: S
    {0x54,	0x01,	0x0074,	0x0054,	0x00FE,	0x00FE,	0x00DE,	0x00DE},// KeyName: T
    {0x55,	0x01,	0x0075,	0x0055,	0x00FA,	0x00FA,	0x00DA,	0x00DA},// KeyName: U
    {0x57,	0x01,	0x0077,	0x0057,	0x00E5,	0x00E5,	0x00C5,	0x00C5},// KeyName: W
    {0x59,	0x01,	0x0079,	0x0059,	0x00FC,	0x00FC,	0x00DC,	0x00DC},// KeyName: Y
    {0x5A,	0x01,	0x007A,	0x005A,	0x00E6,	0x00E6,	0x00C6,	0x00C6},// KeyName: Z
    {0xBA,	0x00,	0x003B,	0x003A,	0x00B6,	0x00B6,	0x00B0,	0x00B0},// KeyName: OEM_1
    {0xBB,	0x00,	0x003D,	0x002B,	0x00D7,	0x00D7,	0x00F7,	0x00F7},// KeyName: OEM_PLUS
    {0xBC,	0x00,	0x002C,	0x003C,	0x00E7,	0x00E7,	0x00C7,	0x00C7},// KeyName: OEM_COMMA
    {0xDB,	0x00,	0x005B,	0x007B,	0x00AB,	0x00AB,	0x2190,	0x2190},// KeyName: OEM_4
    {0xDC,	0x00,	0x005C,	0x007C,	0x00AC,	0x00AC,	0x00A6,	0x00A6},// KeyName: OEM_5
    {0xDD,	0x00,	0x005D,	0x007D,	0x00BB,	0x00BB,	0x2192,	0x2192},// KeyName: OEM_6
    {0xDE,	0x00,	0x0027,	0x0022,	0x00B4,	0x00B4,	0x00A8,	0x00A8},// KeyName: OEM_7
    {0,	0	,0	,0	,0	,0	,0	,0}
};

static ALLOC_SECTION_LDATA VK_TO_WCHARS7 vk_to_wchar7[] = { 
    {0x08,	0x00,	0x0008,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	0x007F},// KeyName: BACK
    {0x09,	0x00,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	0x0009},// KeyName: TAB
    {0,	0	,0	,0	,0	,0	,0	,0	,0}
};

static ALLOC_SECTION_LDATA VK_TO_WCHARS8 vk_to_wcharNumpad[] = { 
    {0x60,	0x00,	0x0030,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	0x0030},// KeyName: NUMPAD0
    {0x61,	0x00,	0x0031,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	0x0031},// KeyName: NUMPAD1
    {0x62,	0x00,	0x0032,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	0x0032},// KeyName: NUMPAD2
    {0x63,	0x00,	0x0033,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	0x0033},// KeyName: NUMPAD3
    {0x64,	0x00,	0x0034,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	0x0034},// KeyName: NUMPAD4
    {0x65,	0x00,	0x0035,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	0x0035},// KeyName: NUMPAD5
    {0x66,	0x00,	0x0036,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	0x0036},// KeyName: NUMPAD6
    {0x67,	0x00,	0x0037,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	0x0037},// KeyName: NUMPAD7
    {0x68,	0x00,	0x0038,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	0x0038},// KeyName: NUMPAD8
    {0x69,	0x00,	0x0039,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	0x0039},// KeyName: NUMPAD9
    {0x6E,	0x00,	0x002E,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	WCH_NONE,	0x002E},// KeyName: DECIMAL
    {0,	0	,0	,0	,0	,0	,0	,0	,0	,0}
};



//---------------------------------------------------------------------------
// Virtual Key to WCHAR translations with shift states
//---------------------------------------------------------------------------

static ALLOC_SECTION_LDATA VK_TO_WCHAR_TABLE vk_to_wchar[] = {
    /*{(PVK_TO_WCHARS1)vk_to_wchar5, 5, sizeof(vk_to_wchar5[0])},*/
    {(PVK_TO_WCHARS1)vk_to_wchar1, 1, sizeof(vk_to_wchar1[0])},
    {(PVK_TO_WCHARS1)vk_to_wchar2, 2, sizeof(vk_to_wchar2[0])},
    {(PVK_TO_WCHARS1)vk_to_wchar4, 4, sizeof(vk_to_wchar4[0])},
    {(PVK_TO_WCHARS1)vk_to_wchar6, 6, sizeof(vk_to_wchar6[0])},
    {(PVK_TO_WCHARS1)vk_to_wchar7, 7, sizeof(vk_to_wchar7[0])},
    {(PVK_TO_WCHARS1)vk_to_wcharNumpad, 8, sizeof(vk_to_wcharNumpad[0])},
    {NULL,                         0, 0}
};

//---------------------------------------------------------------------------
// Dead keys sequences translations
//---------------------------------------------------------------------------

static ALLOC_SECTION_LDATA DEADKEY dead_keys[] = {
    //              Accent        Composed               Flags
    //              ------        --------               -----
    //DEADTRANS(L'e', UC_ACUTE,     UC_LOWER_E_ACUTE,      0x0000),
    /*dead_keys*/
    {0, 0, 0}
};

//---------------------------------------------------------------------------
// Main keyboard layout structure, point to all tables
//---------------------------------------------------------------------------

static ALLOC_SECTION_LDATA KBDTABLES kbd_tables = {
    .pCharModifiers = &char_modifiers,
    .pVkToWcharTable = vk_to_wchar,
    .pDeadKey = dead_keys,
    .pKeyNames = key_names,
    .pKeyNamesExt = key_names_ext,
    .pKeyNamesDead = key_names_dead,
    .pusVSCtoVK = scancode_to_vk,
    .bMaxVSCtoVK = ARRAYSIZE(scancode_to_vk),
    .pVSCtoVK_E0 = scancode_to_vk_e0,
    .pVSCtoVK_E1 = scancode_to_vk_e1,
    /*.fLocaleFlags = MAKELONG(KLLF_ALTGR, KBD_VERSION),*/
    .fLocaleFlags = MAKELONG(0x0003, KBD_VERSION), // KLLF_ALTGR:True, KLLF_SHIFTLOCK:True, KLLF_LRM_RLM:False

    
    .nLgMax = 0,// Ligature support is on the todo
    .cbLgEntry = 0,// Ligature support is on the todo
    .pLigature = NULL,// Ligature support is on the todo
    .dwType = KBD_TYPE,//4,// should be KBD_TYPE
    .dwSubType = 0,// not shure right now what this was, smth. like a keyboard subtype maybe for FarEast?
};

//---------------------------------------------------------------------------
// Keyboard layout entry point
//---------------------------------------------------------------------------

__declspec(dllexport) PKBDTABLES KbdLayerDescriptor(void)
{
    return &kbd_tables;
}


/***********************************************************************\
* VkToFuncTable_101[]
*
\***********************************************************************/

// example
//static VK_F VkToFuncTable_101[] = {
//    {
//        VK_CAPITAL,                 // Base Vk
//        KBDNLS_TYPE_TOGGLE,         // NLSFEProcType
//        KBDNLS_INDEX_NORMAL,        // NLSFEProcCurrent
//        0x08, /* 00001000 */        // NLSFEProcSwitch
//        {                           // NLSFEProc
//            {KBDNLS_SEND_BASE_VK,0},        // Base
//            {KBDNLS_ALPHANUM,0},            // Shift
//            {KBDNLS_HIRAGANA,0},            // Control
//            {KBDNLS_SEND_PARAM_VK,VK_KANA}, // Shift+Control
//            {KBDNLS_KATAKANA,0},            // Alt
//            {KBDNLS_SEND_BASE_VK,0},        // Shift+Alt
//            {KBDNLS_SEND_BASE_VK,0},        // Control+Alt
//            {KBDNLS_SEND_BASE_VK,0}         // Shift+Control+Alt
//        },
//        {                           // NLSFEProcAlt
//            {KBDNLS_SEND_PARAM_VK,VK_KANA}, // Base
//            {KBDNLS_SEND_PARAM_VK,VK_KANA}, // Shift
//            {KBDNLS_SEND_PARAM_VK,VK_KANA}, // Control
//            {KBDNLS_SEND_PARAM_VK,VK_KANA}, // Shift+Control
//            {KBDNLS_SEND_BASE_VK,0},        // Alt
//            {KBDNLS_SEND_BASE_VK,0},        // Shift+Alt
//            {KBDNLS_SEND_BASE_VK,0},        // Control+Alt
//            {KBDNLS_SEND_BASE_VK,0}         // Shift+Control+Alt
//        }
//    },
//    {
//        VK_OEM_3,            // Base Vk
//        KBDNLS_TYPE_NORMAL,  // NLSFEProcType
//        KBDNLS_INDEX_NORMAL, // NLSFEProcCurrent
//        0,                   // NLSFEProcSwitch
//        {                    // NLSFEProc
//            {KBDNLS_SEND_BASE_VK,0},         // Base
//            {KBDNLS_SEND_BASE_VK,0},         // Shift
//            {KBDNLS_SBCSDBCS,0},             // Control
//            {KBDNLS_SEND_BASE_VK,0},         // Shift+Control
//            {KBDNLS_SEND_PARAM_VK,VK_KANJI}, // Alt
//            {KBDNLS_SEND_BASE_VK,0},         // Shift+Alt
//            {KBDNLS_SEND_BASE_VK,0},         // Control+Alt
//            {KBDNLS_SEND_BASE_VK,0}          // Shift+Control+Alt
//        },
//        {                    // NLSFEProcIndexAlt
//            {KBDNLS_NULL,0},                 // Base
//            {KBDNLS_NULL,0},                 // Shift
//            {KBDNLS_NULL,0},                 // Control
//            {KBDNLS_NULL,0},                 // Shift+Control
//            {KBDNLS_NULL,0},                 // Alt
//            {KBDNLS_NULL,0},                 // Shift+Alt
//            {KBDNLS_NULL,0},                 // Control+Alt
//            {KBDNLS_NULL,0}                  // Shift+Control+Alt
//        }
//    }
//};

static ALLOC_SECTION_LDATA VK_F VkToFuncTable[] = {
    {
        0xC0,
        KBDNLS_TYPE_TOGGLE,
        KBDNLS_INDEX_NORMAL,
        0x10,
        {
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {0x03, 0x14},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0}
        },
        {
            {0x03, 0x14},
            {0x03, 0x14},
            {0x03, 0x14},
            {0x03, 0x14},
            {0x03, 0x14},
            {0x03, 0x14},
            {0x03, 0x14},
            {0x03, 0x14}
        },
    },
    {
        0x59,
        KBDNLS_TYPE_TOGGLE,
        KBDNLS_INDEX_NORMAL,
        0x10,
        {
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {0x03, 0x21},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0}
        },
        {
            {0x03, 0x21},
            {0x03, 0x21},
            {0x03, 0x21},
            {0x03, 0x21},
            {0x03, 0x21},
            {0x03, 0x21},
            {0x03, 0x21},
            {0x03, 0x21}
        },
    },
    {
        0x55,
        KBDNLS_TYPE_TOGGLE,
        KBDNLS_INDEX_NORMAL,
        0x10,
        {
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {0x03, 0x24},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0}
        },
        {
            {0x03, 0x24},
            {0x03, 0x24},
            {0x03, 0x24},
            {0x03, 0x24},
            {0x03, 0x24},
            {0x03, 0x24},
            {0x03, 0x24},
            {0x03, 0x24}
        },
    },
    {
        0x49,
        KBDNLS_TYPE_TOGGLE,
        KBDNLS_INDEX_NORMAL,
        0x10,
        {
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {0x03, 0x26},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0}
        },
        {
            {0x03, 0x26},
            {0x03, 0x26},
            {0x03, 0x26},
            {0x03, 0x26},
            {0x03, 0x26},
            {0x03, 0x26},
            {0x03, 0x26},
            {0x03, 0x26}
        },
    },
    {
        0x4F,
        KBDNLS_TYPE_TOGGLE,
        KBDNLS_INDEX_NORMAL,
        0x10,
        {
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {0x03, 0x23},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0}
        },
        {
            {0x03, 0x23},
            {0x03, 0x23},
            {0x03, 0x23},
            {0x03, 0x23},
            {0x03, 0x23},
            {0x03, 0x23},
            {0x03, 0x23},
            {0x03, 0x23}
        },
    },
    {
        0x50,
        KBDNLS_TYPE_TOGGLE,
        KBDNLS_INDEX_NORMAL,
        0x10,
        {
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {0x03, 0x2E},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0}
        },
        {
            {0x03, 0x2E},
            {0x03, 0x2E},
            {0x03, 0x2E},
            {0x03, 0x2E},
            {0x03, 0x2E},
            {0x03, 0x2E},
            {0x03, 0x2E},
            {0x03, 0x2E}
        },
    },
    {
        0x12,
        KBDNLS_TYPE_NORMAL,
        KBDNLS_INDEX_NORMAL,
        0x00,
        {
            {0x01, 0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {0x01, 0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0}
        },
        {
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0}
        },
    },
    {
        0x44,
        KBDNLS_TYPE_TOGGLE,
        KBDNLS_INDEX_NORMAL,
        0x10,
        {
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {0x03, 0x11},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0}
        },
        {
            {0x03, 0x11},
            {0x03, 0x11},
            {0x03, 0x11},
            {0x03, 0x11},
            {0x03, 0x11},
            {0x03, 0x11},
            {0x03, 0x11},
            {0x03, 0x11}
        },
    },
    {
        0x46,
        KBDNLS_TYPE_TOGGLE,
        KBDNLS_INDEX_NORMAL,
        0x10,
        {
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {0x03, 0x10},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0}
        },
        {
            {0x03, 0x10},
            {0x03, 0x10},
            {0x03, 0x10},
            {0x03, 0x10},
            {0x03, 0x10},
            {0x03, 0x10},
            {0x03, 0x10},
            {0x03, 0x10}
        },
    },
    {
        0x48,
        KBDNLS_TYPE_TOGGLE,
        KBDNLS_INDEX_NORMAL,
        0x10,
        {
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {0x03, 0x22},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0}
        },
        {
            {0x03, 0x22},
            {0x03, 0x22},
            {0x03, 0x22},
            {0x03, 0x22},
            {0x03, 0x22},
            {0x03, 0x22},
            {0x03, 0x22},
            {0x03, 0x22}
        },
    },
    {
        0x4A,
        KBDNLS_TYPE_TOGGLE,
        KBDNLS_INDEX_NORMAL,
        0x10,
        {
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {0x03, 0x25},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0}
        },
        {
            {0x03, 0x25},
            {0x03, 0x25},
            {0x03, 0x25},
            {0x03, 0x25},
            {0x03, 0x25},
            {0x03, 0x25},
            {0x03, 0x25},
            {0x03, 0x25}
        },
    },
    {
        0x4B,
        KBDNLS_TYPE_TOGGLE,
        KBDNLS_INDEX_NORMAL,
        0x10,
        {
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {0x03, 0x28},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0}
        },
        {
            {0x03, 0x28},
            {0x03, 0x28},
            {0x03, 0x28},
            {0x03, 0x28},
            {0x03, 0x28},
            {0x03, 0x28},
            {0x03, 0x28},
            {0x03, 0x28}
        },
    },
    {
        0x4C,
        KBDNLS_TYPE_TOGGLE,
        KBDNLS_INDEX_NORMAL,
        0x10,
        {
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {0x03, 0x27},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0}
        },
        {
            {0x03, 0x27},
            {0x03, 0x27},
            {0x03, 0x27},
            {0x03, 0x27},
            {0x03, 0x27},
            {0x03, 0x27},
            {0x03, 0x27},
            {0x03, 0x27}
        },
    },
    {
        0xBA,
        KBDNLS_TYPE_TOGGLE,
        KBDNLS_INDEX_NORMAL,
        0x10,
        {
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {0x03, 0x08},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0},
            {KBDNLS_SEND_BASE_VK,0}
        },
        {
            {0x03, 0x08},
            {0x03, 0x08},
            {0x03, 0x08},
            {0x03, 0x08},
            {0x03, 0x08},
            {0x03, 0x08},
            {0x03, 0x08},
            {0x03, 0x08}
        },
    }
};

/***********************************************************************\
* KbdNlsTables
*
\***********************************************************************/

static ALLOC_SECTION_LDATA KBDNLSTABLES KbdNlsTables101 = {
    0,                      // OEM ID (0 = Microsoft)
    0,                      // Information
    14,                      // Number of VK_F entry
    VkToFuncTable,      // Pointer to VK_F array
    0,                      // Number of MouseVk entry
    NULL                    // Pointer to MouseVk array
};

__declspec(dllexport) PKBDNLSTABLES KbdNlsLayerDescriptor(VOID)
{
    return &KbdNlsTables101;
}