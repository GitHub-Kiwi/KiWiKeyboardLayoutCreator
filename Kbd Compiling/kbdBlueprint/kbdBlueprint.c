//---------------------------------------------------------------------------
// basic pattern of a kbd*.c file
// kommented parts show one example, followed by a comment, that will be replaced by the data from the Layout Creator
// Made by Ki.Wi.
//---------------------------------------------------------------------------

// #define KBD_TYPE 4
/*def_KBD_TYPE*/

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
    /*key_names*/
    {0x00, NULL}
};

//---------------------------------------------------------------------------
// Scan codes to key names (extended keypad)
//---------------------------------------------------------------------------

static ALLOC_SECTION_LDATA VSC_LPWSTR key_names_ext[] = {
    /*key_names_ext*/
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
    
    /*scancode_to_vk*/
};

//---------------------------------------------------------------------------
// Scan code to virtual key conversion table (scancodes with E0 prefix)
//---------------------------------------------------------------------------

static ALLOC_SECTION_LDATA VSC_VK scancode_to_vk_e0[] = {
    /*{0x10, VK_MEDIA_PREV_TRACK | KBDEXT},*/
    /*scancode_to_vk_e0*/
    {0x00, 0x0000}
};

//---------------------------------------------------------------------------
// Scan code to virtual key conversion table (scancodes with E1 prefix)
//---------------------------------------------------------------------------

static ALLOC_SECTION_LDATA VSC_VK scancode_to_vk_e1[] = {
    /*{0x1D, VK_PAUSE},*/
    /*scancode_to_vk_e1*/ // i think this is a concept that was not followed further. Seems to always just be {0x1D, VK_PAUSE} only
    {0x00, 0x0000}
};

//---------------------------------------------------------------------------
// Associate a virtual key with a modifier bitmask
//---------------------------------------------------------------------------

static ALLOC_SECTION_LDATA VK_TO_BIT vk_to_bits[] = {
    /*{VK_SHIFT,   KBDSHIFT},*/
    /*vk_to_bits*/
    {0, 0}
};

//---------------------------------------------------------------------------
// Map character modifier bits to modification number
//---------------------------------------------------------------------------

static ALLOC_SECTION_LDATA MODIFIERS char_modifiers = {
    .pVkToBit = vk_to_bits,
    /*char_modifiers_wMaxModBits*/
    .ModNumber = {
        /*char_modifiers_ModNumber*/
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

/*vk_to_wcharX*/

//---------------------------------------------------------------------------
// Virtual Key to WCHAR translations with shift states
//---------------------------------------------------------------------------

static ALLOC_SECTION_LDATA VK_TO_WCHAR_TABLE vk_to_wchar[] = {
    /*{(PVK_TO_WCHARS1)vk_to_wchar5, 5, sizeof(vk_to_wchar5[0])},*/
    /*vk_to_wchar*/
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
    .fLocaleFlags = /*fLocaleFlags*/

    
    .nLgMax = 0,// Ligature support is on the todo
    .cbLgEntry = 0,// Ligature support is on the todo
    .pLigature = NULL,// Ligature support is on the todo
    .dwType = KBD_TYPE,//defined in .h
    .dwSubType = 0,// not shure right now what this was, smth. like a keyboard subtype maybe for FarEast?
};

//---------------------------------------------------------------------------
// Keyboard layout entry point
//---------------------------------------------------------------------------

PKBDTABLES KbdLayerDescriptor(void)
{
    return &kbd_tables;
}


/*KBDNLS*/