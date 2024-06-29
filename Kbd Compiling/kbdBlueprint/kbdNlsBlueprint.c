static ALLOC_SECTION_LDATA VK_F VkToFuncTable[] = {
    /*VkToFuncTableContent*/
};

static ALLOC_SECTION_LDATA KBDNLSTABLES KbdNlsTables101 = {
    0,                      // OEM ID (0 = Microsoft)
    0,                      // Information
    /*VkToFuncTableEntryCount*/,                      // Number of VK_F entry
    VkToFuncTable,      // Pointer to VK_F array
    0,                      // Number of MouseVk entry
    NULL                    // Pointer to MouseVk array
};

PKBDNLSTABLES KbdNlsLayerDescriptor(VOID)
{
    return &KbdNlsTables101;
}