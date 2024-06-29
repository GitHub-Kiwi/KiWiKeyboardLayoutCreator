# Readme - KbdLayoutInstaller

The maingoal of this Project is to deploy a given layout.dll without a system restart.  

The build procedure of this Project is setup, so that it compiles to the same folder as the main Project, so that the exe of this project can be called from the main project without a relative path.

This is a own Project, because Adminprivileges are required, since the .dll needs to be copied to windows/System32 and entries in the Registry need to be altered / created.  

The settings_add_kbd_changes.reg.txt is a recording of the registry entries that were done by windows-settings when a keyboardlayout was added to a language.

common abbreviations:
- TSF: [Text Service Framework](https://en.wikipedia.org/wiki/Text_Services_Framework)
  - https://learn.microsoft.com/de-de/windows/win32/tsf/about-text-services-framework
- CTF: Collaborative Translation Framework 
- TIP: Text Input Processor
- IME: (Microsoft) [Input Method Processor](https://de.wikipedia.org/wiki/Microsoft_IME)
- CLSID: [Class Identifier](https://learn.microsoft.com/de-de/windows/win32/com/clsid-key-hklm)
- HKCR: HKEY_CLASSES_ROOT (Registry folder)
- HKCU: HKEY_CURRENT_MACHINE (Registry folder)
- HKML: HKEY_LOCAL_MACHINE (Registry folder)


# kbdLayout Registry references

There are 3 commonly used identifiers:
- **Language Code Identifier** ([LCID](https://learn.microsoft.com/en-us/windows/win32/intl/language-identifier-constants-and-strings)) is a 4 Hexdigit Number.
  - e.g. 0x409 for en / 0x407 is ger / 0x412 is kor
- **Layout device identifier** is a 4 Hexdigit Number. This is a increasing counter, where every new Layout of a languageCode recieves the lowest available value.  
  - e.g. all English(US) layouts have the LCID 0409, but the primary layout has the device identifier 0000, US Dvorak has 0001, US International has 0002
  - Some Layout creaters start counting at a000 as a convention, signaling that this layout is a custom.
- **Layout Id** Every Layout except the layouts with Layout device identifier 0000 has a Layout ID. This is a increasing 4 Hexdigit counter thats unique for every layout across all languages.  
Every new layout should recieve the lowest available LayoutID
- a 8-Hex-Key the **input locale identifier**
  - the first 4 Hexnumbers are the Layout device identifier
  - the last 4 Hexnumbers are the Language code identifier
- a nameless but often used format that I'll call the **Layout handle**, since it can be retrieved with InputLanguage.CurrentInputLanguage.Handle: 
  - the format for basic Layouts that don't have a LayoutID is just LCIDLCID (so their LCID twice). As bits thats (LCID<<16 | LCID)
    - oftentimes this is represented as a number. This then cuts off the leading 0
  - the format is FXXXLLLL if the Layout is one of the basic ones that do not have a LayoutID 
    - the first digit is just the HexValue F
    - the digits 2-4 (XXX) are the digits 2-4 of the "Layout Id" 
    - the digits 5-8 (LLLL) are the LanguageCode of the culture under which the keyboard was registered to the system.

## Keyboard Layouts

The minimum that needs to be done to get the Layout into windows:
create the Layouts Registry-Key in  
```HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Keyboard Layouts```

- each Keyboard is registered with its **input locale identifier**
- the registry Key needs the following value-data pairs:
  - "Layout File" with the name of the kbdxyz.dll
  - "Layout Id" holds the Layout Id ofc.
  - "Layout Text" is the description that is displayed in windows.
  - optional a Layout Display Name can be added. ctfmon is looking for that in the registry.
- the kbdxyz.dll needs to be copied to windows\system32 and be loaded by windows

now Windows-Settings will find this entry and the user will be able to load the layout in the languageSettings.  

**Tell Keyboard Layouts to language-bar  & ctfmon.exe**:  
The language-bar will only display and add the layout correctly once ctfmon.exe knows about the keyboard.  
ctfmon.exe can learn about the layout by getting killed (windows will restart it immediately).  
It seems to scan the registrys "Keyboard Layouts" when it starts.

## Preload

```HKEY_CURRENT_USER\Keyboard Layout\Preload```  
Holds Values that are references to the Keyboards that are loaded when windows starts.  
Value-name is a decimal number/index . A new Keyboard gets the highest number + 1.  
Value-data can have the format dXXXLCID or be a input locale identifier.  

format dXXXLLLL:  
- d is a flag that indicates that the layout is substituted to a different language (loaded under a language that is different from the layouts language)  
- XXX is a increasing hexadecimal counter for different language substitutes  
- LCID is the LCID of the language they are loaded under 

## Substitutes

```HKEY_CURRENT_USER\Keyboard Layout\Substitutes```  
maps the substitute values (value-name) to the input locale identifier (value-data).

e.g. Value-name 00000407 with Value-data 00060409 would substitute the standard German layout with the US international layout.  
This way the US. international layout becomes the standard layout when the windows language is set to german (language set to german sets 00000407 at first place in the preload registry).  
or d0010407 00020409 would load the english (0409) layout under the german(0407) language.

## UserProfile

```HKEY_CURRENT_USER\Control Panel\International\User Profile\```  
contains the activated languages (settings » time and language » language and region), 
and their loaded keyboards. 

## CTF-SortOrder

```HKEY_CURRENT_USER\Software\Microsoft\CTF\SortOrder\``` holds info about the activated keyboardLayout.  

```HKEY_CURRENT_USER\Software\Microsoft\CTF\SortOrder\AssemblyItem\LCID\GUID\< 8 - digit - decimal - counter >```  
Are the Currently Loaded Layouts and apparently their Sort order(in the language bar? / in the settings? / both?).  

The GUID in this Path is always the same for all LCIDs for me(i have ger, en and kor languages in use).  

The KeyboardLayout entry identifies the keyboardLayout.  
"KeyboardLayout" = dword:f0d60407 // f + LayoutId + owningCultureLCID
seems to be 0x00000000 if a TIP is used

The CLSID entry seems to hold info about the keyboards TIP-CLSID. Is 0-GUID if the Layout doesn't use TIP.  
The Profile entry seems to be the chosen LanguageProfile of the TIP. Is 0-GUID if the Layout doesn't use TIP.  
maybe setting a CLSID to a TIP here allows to add a TIP to a different Language.
e.g add Korean IME under a the english language.
See next section for more info on TIP entries.

### CTF-TIP

```HKEY_CURRENT_USER\Software\Microsoft\CTF\TIP\```
seems to holds the TIPs CLSIDs used by the layouts that the current user has loaded.  

```HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\CTF\TIP\```
and 
```HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\CTF\TIP\```
Seems to hold info about the installed TIPs in form of CLSIDs.  
A TIP can have no or multiple possible LanguageProfiles. 

## Registry information sources

a lot of details can gathered when observing the registry.  
tools for that:  
- [x64 tools](https://www.nirsoft.net/x64_download_package.html) has tools to monitor changes in registry by specific applications or to search the registry for specific values.
- [Process Monitor (procmon)](https://learn.microsoft.com/de-de/sysinternals/downloads/procmon) can be used to monitor the registry, all access to a Key/keypath or everything a specific application does.

tools to monitor interactions with dlls / interfaces like e.g. LoadKeyboardLayout:  
- [Process Explorer (procexp)](https://learn.microsoft.com/de-de/sysinternals/downloads/process-explorer) can be used to monitor, which process opened which process or dll.
- API Monitor (rohitab.com) is able to monitor system api calls (calls to dlls such as winuser32.dll etc.)

informative resources about some of the KeyboardLayout registry stuff:  
- [winkbdlayout](https://github.com/lelegard/winkbdlayouts)
- [Keyboard_Layout_DLL_files](http://www.kbdedit.com/manual/admin_deploy.html#Keyboard_Layout_DLL_files).  

Korean and Japanese layouts are deployed differently.  
More details [here](http://www.kbdedit.com/manual/low_level_special_nls_functions.html#japanese_korean_layout_registration),
[here](http://archives.miloush.net/michkap/archive/2012/04/30/10298801.html),
and [here](http://archives.miloush.net/michkap/archive/2012/07/09/10327871.html).

# Deploying without restart:

LoadKeyboardLayout / ActivateKeyboardLayout don't update the RegistryEntries.  
Load only adds a KeyboardLayout to the TSF/CTF, and Activate activates it there.  
In order to fully Deploy the Layout so that it remains after the restart, 
the registry entries also need to be set.

**LoadKeyboardLayout** makes the layout available for activation. 
Since Windows8 it can also immediately activate.  
https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-loadkeyboardlayouta  
This requires the layout to be in registry»"Keyboard Layouts".  
If the layout is supposed to be substituted the registry entry also needs to be there.  

**ActivateKeyboardLayout** then sets the layout as active, can also be done with a flag in LoadKeyboardLayout   
the HKL is Layout device identifier (last 3 Hexdigits) + LCID under which the layout got registered.  
https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-activatekeyboardlayout  
seems to do the same like  
```
PostMessage(this.Handle, WM_INPUTLANGCHANGEREQUEST, 0, ulong.Parse(textBox4.Text, System.Globalization.NumberStyles.HexNumber));  
PostMessage(this.Handle, WM_INPUTLANGCHANGE, 0, ulong.Parse(textBox4.Text, System.Globalization.NumberStyles.HexNumber));  
```
but has additional flags to do a bit more like reseting shiftstate or reordering the layouts

**GetKeyboardLayout** returns same value like InputLanguage.CurrentInputLanguage.Handle  
https://learn.microsoft.com/de-de/windows/win32/api/winuser/nf-winuser-getkeyboardlayout?redirectedfrom=MSDN  

# todo: 

add registry Keyboard Layouts value that marks the Layouts created with this tool

implement registry rollback after failed change via. 
https://learn.microsoft.com/de-de/windows-server/administration/windows-commands/reg-export

add layoutdisplay-name when creating/editing a Layout

abort registration if current language uses TIP, TIP is not supported so far

add uninstall layout functionality

make installer form1 invisible alternative use setActiveKeyboardLayout
might be possible to make this without a form, by using loadkeyboardlayout without setActive flag and using setactivelayoutKeyboard afterwards
would be a bit cleaner

add support to laod keyboard for TIP languages

