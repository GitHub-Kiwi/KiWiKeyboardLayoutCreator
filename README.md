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
After this mapping everything is dependent on the VKs, while the Scancodes tipically are no longer looked at.  

There are special Keys that additionaly to Scancodes send the e0/e1 flag. E.g. AltGr (aka. RMenu) 
the naviagtion keys or the Numpad key send an additionaly e0.  
Ony the SCRoll Key sends the e1 flag. Because of that the SCRoll key is just always added to the Layout, it does not need to be explicitly defined.  
Also defining multimedia keys like play/pause/nextTrack, VolumeUp/VolumeMute, etc. doesn't seem to be necessary, but can be done.

Additionaly to the VK-value the entry for the VK can be masked with a **Extension/Special/Multi/Numpad** flag.  
The **Extension** flag should be set for the e0 keys.  
The **Numpad** flag is for the Numpad-digits & numpad-decimal keys.  
The **Multi** flag is for multimedia keys.  
The **Special** flag is for keys with special behaviour like Keys that have a NLS-layer behaviour.  

## Layer setup

The Ctrl Layer definitely should not be used, since Ctrl+key is commonly used for Hotkeys. Alt-layer kinda sortof should not be used for the same reason, although alt+key hotkeys are much more rare. 
Alt+Ctrl is ok and often used as a layer. AltGr equals Alt+Ctrl if that behaviour is activated in the Properties.  
Windows supports a maximum of 16 different Layers.  
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
