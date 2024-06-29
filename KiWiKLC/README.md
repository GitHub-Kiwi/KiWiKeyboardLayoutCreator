# KnowHow

Informative Websites:  
- [winkbdlayouts](https://github.com/lelegard/winkbdlayouts) explains a lot about how a kbdXXXX.c file works
- [windows driver samples](https://github.com/microsoft/Windows-driver-samples/tree/main/input/layout) windows keyboard layout driver samples
- [kbdlayout info](https://kbdlayout.info/) is a database about Keyboard Layouts
  - [United States International (kbdus)](https://kbdlayout.info/kbdusx)
  - [some often used terminology / abreviations](https://kbdlayout.info/terminolog)
- [Microsoft Keyboard Layout Creator (MSKLC)](https://www.microsoft.com/en-us/download/details.aspx?id=102134) a Keyboard Layout Creator from Microsoft with very basic functionality.
- [kbdedit](http://www.kbdedit.com/) is another tool which can create keyboard layouts. Documents a lot of stuff and has lots of functionality but still some restrictions.
- [keyboardchecker](https://keyboardchecker.com/) good to figure out, which results are produced by the layout
- [keycap sizes](https://intercom.help/omnitype/en/articles/5121683-keycap-sizes)

## Scancode to VK

Maps the Scancodes that the keyboard sends to the PC to VirtualKeycodes (VK). 
This is the first thing that needs to be setup, after this mapping everything is dependent on the VKs, while the Scancodes tipically are no longer looked at.  

There are special Keys that additionaly to Scancodes send the e0/e1 flag. E.g. AltGr (aka. RMenu) the naviagtion keys or the Numpad key send an additionaly e0. Ony the SCRoll Key sends the e1 flag.

Additionaly to the VK-value the entry for the VK can be masked with a **Extension/Special/Multi/Numpad** flag.  
The **Extension** flag should be set for the e0/e1 keys.  
The **Numpad** flag is for the Numpad-digits/numpad-decimal keys.  
The **Multi** flag is for multimedia keys.  
The **Special** flag is for keys with special behaviour like Keys that have a NLS-layer behaviour.  

## Layer setup

The Ctrl Layer definitely should not be used, since Ctrl+key is commonly used for Hotkeys. Alt-layer kinda sortof should not be used for the same reason, although alt+key hotkeys are much more rare. 
Alt+Ctrl is ok and often used as a layer. AltGr equals Alt+Ctrl if the flag in Properties is set.  
Windows supports a maximum of 16 Layers.  
Which key toggles which Layer can be defined in the Layout-Properties. Shift/Control/Menu(Alt) also get triggered by LShift/RShift LControl/RControl LMenu/RMenu.  
However NLS-Keys can only be defined on the Shift/Alt/Ctrl Layers and they are always triggered by the shift/control/menu keys. Read more about that in the NLS Layers section.

### NLS Layers

For the Shift/Alt/Ctrl Layers and their combinations Keys can have NLS Properties. That means that they can do more than just send their Virtual-Keycode (VK) and Unicode-Characters on these Layers.  
E.g. they can send other VKs.  

**The NLS-Layers are even activated, when the key activating the layer is canceled with the NLS Drop-key-event option.**  
The drop-key-event completely prevents the propagation of the keyevent by  windows.
This means by assigning vk_Shift to a key and then the drop-key-event on its base layer, this key will trigger the shift layer for the nls keys, without having an effect on anything else. 
It won't change the WCHAR keys into the shift-layer, only the nls-keys. Also Applications won't see know that shift is pressed.  

When a new VK is send with the VK-Param option, Windows will not regard the VK-to-WCHAR tables for that VK. So this option is only usefull for non-Letter-VKs.

If vk_to_bits maps other keys to layer bits, this does not affect the nls layers. The keys that shift the nls layers are always the classic shift/alt/ctrl VKs. 
This is independent of the Scancode-to-vk mapping, so the shift/alt/ctrl VKs can be mapped to different keys to make other keys shift the nls layers.

AltGr does not trigger the NLS-Alt+Ctrl layer even if the AltGr=Alt+Ctrl property is set. If the property is not set, AltGr activates the nls-Als-layer.	
Enabeling the AltGr-equals-Alt+Ctrl options sends the Alt and the Ctrl VK, but only to applications aswell as the VK-to-WCHAR-tables.  
For the NLS-layers AltGr always only shifts keys to the Alt layer. 
The same thing happens, when a VK of a key is overwritten via nls-different-Vk option. 
If this option is used to send a alt/ctrl/shift VK, these will not cause a nls-layer shift, but they will work in VK-to-WCHAR-tables and in applications.

The VKs that trigger the VK layers are: shift{VK_SHIFT, VK_LSHIFT, VK_RSHIFT}, alt{VK_MENU, VK_LMENU, VK_RMENU}, Ctrl{VK_CONTROL, VK_LCONTROL, VK_RCONTROL}


### Numpad setup

The Numpad keys need the Numpad scancodes(69-83) and the numlock must not be a E0 key, but must have the Extension flag set.  
The Virtual keys assigned to decimal and 0-9 need to have the Numlock-deactivated-layer values (home,end,up,left, etc.) and the Numpad & Special flags must be set for those keys.  
When the user switches to the Numlock-activated-layer, windows seems to override those virtual-key values with the VK_Numpad_X / VK_Decimal values.  
To reflect this behaviour a numpad dialoge was implemented, that allows to set the VK-to-WChar mapping for the layers of those Virtual Keys. 

Remember that Windows has the feature, that u can hold alt and input a unicode with the Numpad to type that character.
For this to work, the normal number-values need to be defined on the alt layer.  

---

# Additional explanation of the other parts of the kbdLLXXXX.c file

This section is only interesting if you want to know how the internals of the driver work.

## layout language & registry stuff

[ISO-639](https://de.wikipedia.org/wiki/Liste_der_ISO-639-Sprachcodes) maps languages to 2 or 3 letters.  
Conventionally layout dlls are named kbd[iso639-name][optional additional namepart].dll.

[CultureTypes Enumeration](https://learn.microsoft.com/de-de/dotnet/api/system.globalization.culturetypes?view=net-8.0) enumerates all cultures with CultureTypes.AllCultures.  
It might be possible to use this to select a keyboardlanguage and set the languagecode and keyboard-name part accordingly.

Computer\HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Keyboard Layout\DosKeybCodes
is a List of the Languagecodes and their languageNameAbbreviation

"Computer\HKEY_CURRENT_USER\Keyboard Layout" contains the currently preloaded keyboardlayouts, under which language they are registered

## Scancode to VK tables

The Scancode to VK mapping for the e0/e1 Keys is defined in seperate table scancode to e0 / scancode to e1 tables.  
In these Scancode-e0/e1 tables it is possible to define the mapping for the same Scancode multiple times, however windows will only use one of those, the others will simply be ignored.

## vk_to_bits

Declaration which Virtual Key activates which layer-bit in the WCHAR table.
A layer is activated from a combination of different Virtual Keys, each key sets its bit in the Layer-Bitmask.  
E.g. if VK_Shift is mapped to KBDSHIFT { VK_SHIFT, KBDSHIFT} and VK_Shift is pressed the results is 00000001. 
VK_Alt and VK_Ctrl pressed with the mapping { VK_CONTROL, KBDCTRL }, { VK_MENU, KBDALT } results in 00000110.  
It is possible to set two bits to the same VK. E.g. u can set { VK_CUSTOM, KBDCTRL } and { VK_CUSTOM, KBDMENU } to have VK_CUSTOM activate ur Alt+Ctrl WCHAR-layer.

Declaring other keys than the standard shift/ctrl/alt keys to trigger a bitmask has no effect on which keys trigger the nls layers except for the AltGR key. 
If a different key is set to trigger alt or ctrl layer, then altGr will only trigger the nls-alt layer.   

## NLS layer

These are the internal NLS properties:
```
// Base Vk defines, which key gets new behaviour  
KBDNLS_TYPE_NORMAL,  // NLSFEProcType if toggle, the keyboard toggles into the alternative keyup table when the key is pressed down
KBDNLS_INDEX_NORMAL, // NLSFEProcCurrent probably defines, which of the two tables is currently used  
NLSFEProcSwitch //Bitmask, that defines for which layers when this key is pressed it toggles to use the alternative keyup function table 
Two tables// the first one defines the normal/keydown behaviour, the second one defines alternative keyup behaviour if KBDNLS_Type is KBDNLS_TYPE_TOGGLE.  
```

## Numpad

I just dont understand jet, how Windows chooses, which Scancodes it maps to which VK_Numpad_X when Numpad-Layer is active.  
It either has to go with the Scancodes, that have VK_home, etc. with a KBDNUMPAD flag, 
or it has to always take the same Scancodes, but then the KBDNUMPAD flag would be unnecessary, 
unless it is just there to unlock the behaviour instead of mapping the behaviour.  
In the end of kbd.h are some Scancodes defined:  
```
#define SCANCODE_LSHIFT      0x2A
#define SCANCODE_RSHIFT      0x36
#define SCANCODE_CTRL        0x1D
#define SCANCODE_ALT         0x38
#define SCANCODE_SIMULATED   (FAKE_KEYSTROKE >> 16)

#define SCANCODE_NUMPAD_FIRST 0x47
#define SCANCODE_NUMPAD_LAST  0x52

#define SCANCODE_LWIN         0x5B
#define SCANCODE_RWIN         0x5C

#define SCANCODE_THAI_LAYOUT_TOGGLE 0x29
```
This indicates, that Windows changes the VK of the keys that have a Scancode within the range of SCANCODE_NUMPAD_FIRST - LAST to VK_NUMPADX.  

## Building the layout

I have managed to setup a C++ Project, that successfully compiles a working KbdDriver. 
The generated c file and the other Properties can be written into that Project and the compiling can be started via commandline.  

- [how to compile proj from commandline](https://stackoverflow.com/questions/498106/how-do-i-compile-a-visual-studio-project-from-the-command-line)
- [MSBuild documentation](https://learn.microsoft.com/en-us/cpp/build/msbuild-visual-cpp?view=msvc-170)

This is done by the KbdDriverBuilder, which adjusts the blueprints, copies them to a new folder and then builds the Project with a Powershell-script.  
That building is done with msbuild, which is only possible if thats installed, which effectively means, that a VisualStudio installation is required.  

### Deploying the layout

See KbdDriverInstaller Projects Readme

---

# TODO

next:  
add layout display test property

add utilitys:
list loaded keyboards
remove loaded keyboard by id

Features:
- move location of multiple keys by selecting them first by drawing a box 
  - snapp moved keys by their boundingbox
- implement alternative Caps behaviour (SGCAPS key-flag)
- Deadkeys
- Ligatures
- Big enter
- add MouseVk capability
- FarEast installation support.

others:
- create standard Layout preset
  - almost done, only special keys like media stuff missing, but those dont even seem to be necesary
  - redo with new keywidth
- FixSpecialKeys function to suggest the correct values for R/LCTRL, R/LShift, Numlock, etc. when they are captured with the capture btn.
- use txtVK in properties would be more elegant, but it works how it is now.
- create WChar-/Hex-txtInput
- adjust compiling
  - try to remove the requirement of msbuild being installed. Maybe thats possible by compiling with Microsoft.Build