# Readme KiWi Keybaord Layout Creator

The purpose of this project is, to realise the GUI that lets Users define a keyboardLayout.  
Also this Project builds the kbd.dll which then can be deployed in Windows (thats what KbdLayoutInstaller project does).

## layout language

Conventionally layout dlls are named kbd[iso639-name][optional informative namepart].dll.  
[ISO-639](https://de.wikipedia.org/wiki/Liste_der_ISO-639-Sprachcodes) maps languages to 2 or 3 letters.  
The Layout language does not necessaryly need to be the language under which it will be used by the user. 
The name defined as the language is more like a friendly suggestion for most languages.  
**FarEast** layouts that use a Text Input Processor (TIP) might be a exception. The 

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

How Windows chooses, which Scancodes it maps to which VK_Numpad_X when Numpad-Layer is active:  
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

See KbdDriverInstaller-Readme

---

# TODO

next:  
add layout display test property  
add language abbreviation to the layoutname based on the languagecode  

Features:

- list loaded keyboards
- remove loaded keyboard by id
- move location of multiple keys by selecting them first by drawing a box 
  - snapp moved keys by their boundingbox
- implement alternative Caps behaviour (SGCAPS key-flag)
- Deadkeys
- Ligatures
- Big enter
- add MouseVk capability

others:
- create standard Layout preset
  - almost done, only special keys like media stuff missing, but those dont even seem to be necessary
  - redo with new keywidth
- FixSpecialKeys function to suggest the correct values for R/LCTRL, R/LShift, Numlock, etc. when they are captured with the capture btn.
- adjust compiling
  - try to remove the requirement of msbuild being installed. Maybe thats possible by compiling with Microsoft.Build

works but could be nicer:
- use txtVK in properties would be more elegant, but it works how it is now.
- create and use WChar-/Hex-txtInput, but it works how it is now.