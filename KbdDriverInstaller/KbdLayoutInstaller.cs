using Microsoft.VisualBasic.Devices;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KbdLayoutInstaller
{
    internal partial class KbdLayoutInstaller
    {
        #region static fields
        // all dlls in my system32 folder to lower case
        private static readonly string[] basicKbdDlls = [
            "kbd101.dll",
            "kbd101a.dll",
            "kbd101b.dll",
            "kbd101c.dll",
            "kbd103.dll",
            "kbd106.dll",
            "kbd106n.dll",
            "kbda1.dll",
            "kbda2.dll",
            "kbda3.dll",
            "kbdadlm.dll",
            "kbdal.dll",
            "kbdarme.dll",
            "kbdarmph.dll",
            "kbdarmty.dll",
            "kbdarmw.dll",
            "kbdax2.dll",
            "kbdaze.dll",
            "kbdazel.dll",
            "kbdazst.dll",
            "kbdbash.dll",
            "kbdbe.dll",
            "kbdbene.dll",
            "kbdbgph.dll",
            "kbdbgph1.dll",
            "kbdbhc.dll",
            "kbdblr.dll",
            "kbdbr.dll",
            "kbdbu.dll",
            "kbdbug.dll",
            "kbdbulg.dll",
            "kbdca.dll",
            "kbdcan.dll",
            "kbdcher.dll",
            "kbdcherp.dll",
            "kbdcr.dll",
            "kbdcz.dll",
            "kbdcz1.dll",
            "kbdcz2.dll",
            "kbdda.dll",
            "kbddiv1.dll",
            "kbddiv2.dll",
            "kbddv.dll",
            "kbddzo.dll",
            "kbdes.dll",
            "kbdest.dll",
            "kbdfa.dll",
            "kbdfar.dll",
            "kbdfc.dll",
            "kbdfi.dll",
            "kbdfi1.dll",
            "kbdfo.dll",
            "kbdfr.dll",
            "kbdfthrk.dll",
            "kbdgae.dll",
            "kbdgeo.dll",
            "kbdgeoer.dll",
            "kbdgeome.dll",
            "kbdgeooa.dll",
            "kbdgeoqw.dll",
            "kbdgkl.dll",
            "kbdgn.dll",
            "kbdgr.dll",
            "kbdgr1.dll",
            "kbdgrlnd.dll",
            "kbdgthc.dll",
            "kbdhau.dll",
            "kbdhaw.dll",
            "kbdhe.dll",
            "kbdhe220.dll",
            "kbdhe319.dll",
            "kbdheb.dll",
            "kbdhebl3.dll",
            "kbdhela2.dll",
            "kbdhela3.dll",
            "kbdhept.dll",
            "kbdhu.dll",
            "kbdhu1.dll",
            "kbdibm02.dll",
            "kbdibo.dll",
            "kbdic.dll",
            "kbdinasa.dll",
            "kbdinbe1.dll",
            "kbdinbe2.dll",
            "kbdinben.dll",
            "kbdindev.dll",
            "kbdinen.dll",
            "kbdinguj.dll",
            "kbdinhin.dll",
            "kbdinkan.dll",
            "kbdinmal.dll",
            "kbdinmar.dll",
            "kbdinori.dll",
            "kbdinpun.dll",
            "kbdintam.dll",
            "kbdintel.dll",
            "kbdinuk2.dll",
            "kbdir.dll",
            "kbdit.dll",
            "kbdit142.dll",
            "kbdiulat.dll",
            "kbdjav.dll",
            "kbdjpn.dll",
            "kbdkaz.dll",
            "kbdkhmr.dll",
            "kbdkni.dll",
            "kbdkor.dll",
            "kbdkotest.dll",
            "kbdkurd.dll",
            "kbdkyr.dll",
            "kbdla.dll",
            "kbdlao.dll",
            "kbdlisub.dll",
            "kbdlisus.dll",
            "kbdlk41a.dll",
            "kbdlt.dll",
            "kbdlt1.dll",
            "kbdlt2.dll",
            "kbdlv.dll",
            "kbdlv1.dll",
            "kbdlvst.dll",
            "kbdmac.dll",
            "kbdmacst.dll",
            "kbdmaori.dll",
            "kbdmlt47.dll",
            "kbdmlt48.dll",
            "kbdmon.dll",
            "kbdmonmo.dll",
            "kbdmonst.dll",
            "kbdmyan.dll",
            "kbdne.dll",
            "kbdnec.dll",
            "kbdnec95.dll",
            "kbdnecat.dll",
            "kbdnecnt.dll",
            "kbdnepr.dll",
            "kbdnko.dll",
            "kbdno.dll",
            "kbdno1.dll",
            "kbdnso.dll",
            "kbdntl.dll",
            "kbdogham.dll",
            "kbdolch.dll",
            "kbdoldit.dll",
            "kbdosa.dll",
            "kbdosm.dll",
            "kbdpash.dll",
            "kbdphags.dll",
            "kbdpl.dll",
            "kbdpl1.dll",
            "kbdpo.dll",
            "kbdro.dll",
            "kbdropr.dll",
            "kbdrost.dll",
            "kbdru.dll",
            "kbdru1.dll",
            "kbdrum.dll",
            "kbdsf.dll",
            "kbdsg.dll",
            "kbdsl.dll",
            "kbdsl1.dll",
            "kbdsmsfi.dll",
            "kbdsmsno.dll",
            "kbdsn1.dll",
            "kbdsora.dll",
            "kbdsorex.dll",
            "kbdsors1.dll",
            "kbdsorst.dll",
            "kbdsp.dll",
            "kbdsw.dll",
            "kbdsw09.dll",
            "kbdsyr1.dll",
            "kbdsyr2.dll",
            "kbdtaile.dll",
            "kbdtajik.dll",
            "kbdtam99.dll",
            "kbdtat.dll",
            "kbdth0.dll",
            "kbdth1.dll",
            "kbdth2.dll",
            "kbdth3.dll",
            "kbdtifi.dll",
            "kbdtifi2.dll",
            "kbdtiprc.dll",
            "kbdtiprd.dll",
            "kbdtt102.dll",
            "kbdtuf.dll",
            "kbdtuq.dll",
            "kbdturme.dll",
            "kbdtzm.dll",
            "kbdughr.dll",
            "kbdughr1.dll",
            "kbduk.dll",
            "kbdukx.dll",
            "kbdur.dll",
            "kbdur1.dll",
            "kbdurdu.dll",
            "kbdus.dll",
            "kbdusa.dll",
            "kbdusl.dll",
            "kbdusr.dll",
            "kbdusx.dll",
            "kbduzb.dll",
            "kbdvntc.dll",
            "kbdwol.dll",
            "kbdyak.dll",
            "kbdyba.dll",
            "kbdycc.dll",
            "kbdycl.dll"];
        private const string registryPathKbdEntries = "SYSTEM\\CurrentControlSet\\Control\\Keyboard Layouts";
        private const string lanBarProcessName = "ctfmon";
        #endregion

        #region dllImport
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "LoadKeyboardLayout", SetLastError = true, ThrowOnUnmappableChar = false)]
        static extern uint LoadKeyboardLayout(string pwszKLID, uint flags);

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "ActivateKeyboardLayout", SetLastError = true, ThrowOnUnmappableChar = false)]
        static extern uint ActivateKeyboardLayout(uint hkl, uint Flags);

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "GetKeyboardLayout", SetLastError = true, ThrowOnUnmappableChar = false)]
        static extern uint GetKeyboardLayout(uint idThread);

        [DllImport("Kernel32", EntryPoint = "GetCurrentThreadId", ExactSpelling = true)]
        public static extern uint GetCurrentWin32ThreadId();
        #endregion

        /// <summary>
        /// puts the layout dll into system32.
        /// also checks the registry: if there is no registry with the defined languagecode and layoutname.dll a entry is made.
        /// the properties in the registry for that languagecode and layoutname are set according to the frmProperties entries.
        /// the "Layout Id" property of the registry is set automatically.
        /// 
        /// requires administrator rights
        /// </summary>
        /// <returns>returns 0 if all good or 1 if error</returns>
        internal static int InstallDll(string dllPath, string layoutLanguageCode, string layoutDescription)
        {

            bool isElevated;
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new (identity);
                isElevated = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            if (!isElevated)
            {
                MessageBox.Show("Administrator rights are necessary to install the keyboard layout.",
                    "Not Successful", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1;
            }

            string dllPathLower = dllPath.ToLower();
            string localeDeviceIdentifier;
            layoutLanguageCode = layoutLanguageCode.ToUpper();

            if (string.IsNullOrEmpty(dllPathLower))
            {
                MessageBox.Show("Please set a dllPath");
                return 1;
            }

            if (!File.Exists(dllPath))
            {
                MessageBox.Show("The layout dll was not found under the path:\r\n"
                    + dllPath);
                return 1;
            }

            if (basicKbdDlls.Contains(Path.GetFileName(dllPathLower)))
            {
                MessageBox.Show("Define a different name for the layout, the name: \"" +
                    Path.GetFileName(dllPathLower) +
                    "\" is already taken by a layout that comes with windows.");
                return 1;
            }

            if (!LanguageCodeRegex().IsMatch(layoutLanguageCode))
            {
                MessageBox.Show("Please select a valid language Code.\r\n" +
                    "The Code here: " + layoutLanguageCode);
                return 1;
            }


            if (!TryGetRegistryInfo(
                dllPathLower, layoutLanguageCode,
                out int highestLanguageKey, out int lowestLayoutId, 
                out bool dllEntryExists, out string existingEntry, out string LayoutId,
                out bool kbdDllIsActive))
            {
                MessageBox.Show("An Error occured while trying to deploy the Keyboard layout.\r\n" +
                    "No changes were done to the registry and the layout.dll was not deployed to system32.", 
                    "Not Successful", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1; 
            }

            if (kbdDllIsActive)
            {
                MessageBox.Show("The active Keyboardlayouts layout has the same name as this one.\r\n" +
                    "Please change the active Keyboardlayout and try again.",
                    "Change active layout", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 1;
            }

            if (dllEntryExists)
            {
                // message box decision: ask if replace
                if (MessageBox.Show("There already is a layout with the same languagecode and layout-name. \r\n" +
                    "Do You want to replace that?", 
                    "Replace existing?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                { return 1; }

                if (!TryUpdateKeyboardLayoutsEntry(dllPathLower, layoutDescription, existingEntry))
                {
                    MessageBox.Show("An Error occured while trying to modify the existing layout entry.\r\n" +
                    "The registry might be partially changed, the layout.dll was not deployed to system32.",
                    "Not Successful", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 1; 
                }
                localeDeviceIdentifier = existingEntry;
            }
            else
            {
                if (!TryCreateKeyboardLayoutsEntry(dllPathLower, layoutLanguageCode, layoutDescription, highestLanguageKey, lowestLayoutId, out string createdEntry, out LayoutId))
                {
                    MessageBox.Show("An Error occured while trying to create the layout entry.\r\n" +
                    "The registry might be partially created, and the layout.dll was not deployed to system32.",
                    "Not Successful", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 1; 
                }
                localeDeviceIdentifier = createdEntry;
            }

            try
            { File.Copy(dllPath, Environment.SystemDirectory + "\\" + Path.GetFileName(dllPathLower), true); }
            catch (Exception)
            {
                MessageBox.Show("An Error occured while trying to copy the layout.dll to system32.\r\n" +
                    "The registry entry for the layout was made successfully, but the layout.dll was not deployed to system32.",
                    "Not Successful", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1;
            }

            
            DialogResult msgBoxResult = DialogResult.No;
            var processes = Process.GetProcessesByName(lanBarProcessName);
            bool restartCTF = false;

            try
            {
                foreach (var prcs in processes)
                { prcs.Kill(); }    
                restartCTF = true;
            }
            catch (Exception)
            { restartCTF = false; }

            if (restartCTF)
            {
                msgBoxResult = MessageBox.Show(
                "The layout was deployed successfully. \r\n" +
                "Would you like to activate the layout now?\r\n" +
                "Yes: load the Layout under the current system language (" + CultureInfo.CurrentCulture.DisplayName + ")\r\n" +
                "No: dont load the layout (You can add it yourself in the languagesettings)",
                "Activate Layout now?",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            else
            {
                msgBoxResult = MessageBox.Show(
                "The layout was deployed successfully. \r\n" +
                "However the Collaborative Translation Framework (CTF) could not be restarted." +
                "The Keyboard will be recognized correctly by CTF once you restart windows.",
                "Layout was deployed",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 3;
            }

            if (msgBoxResult == DialogResult.Yes)
            {
                //check if substitude is necessary 
                var currentCultureLcid = InputLanguage.CurrentInputLanguage.Culture.LCID.ToString("X").PadLeft(4, '0').ToUpper();
                MessageBox.Show(currentCultureLcid);
                bool substitute = layoutLanguageCode != currentCultureLcid;
                string layoutLoadKey = localeDeviceIdentifier;

                if (substitute)
                {
                    if (!TryCreateSubstitudeEntry(localeDeviceIdentifier, currentCultureLcid, out layoutLoadKey))
                    {
                        MessageBox.Show("The substitude registry entry was not created successfully.",
                        "Registry access error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return 4;
                    }
                }

                if (!TryAddPreloadEntry(layoutLoadKey))
                {
                    MessageBox.Show("The preload registry entry was not created successfully.",
                        "Registry access error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 5;
                }

                if (!TryAddLanguageSetting(currentCultureLcid, localeDeviceIdentifier))
                {
                    MessageBox.Show("The settings registry entry was not created successfully.",
                        "Registry access error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 6;
                }

                if (!TryAddSortOrder(currentCultureLcid, LayoutId))
                {
                    MessageBox.Show("The Sort order registry entry was not created successfully.",
                            "Registry access error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 7;
                }

                // see https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-loadkeyboardlayouta for flags
                uint loadKbdFlag = 0x01 | 0x02 | 0x10;
                uint res = LoadKeyboardLayout(layoutLoadKey, loadKbdFlag);
                
                if (res != 0)
                {
                    MessageBox.Show("The layout was loaded and activated successfully."
                        , "Success", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("The layout was not loaded successfully.",
                        "Error while loading the layout", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 8;
                }
            }
            
            return 0;
        }

        /// <returns>8 digit long handle</returns>
        private static string GetCurrentLayoutHandle(out bool hasID)
        {
            // InputLanguage.CurrentInputLanguage.Handle returns a LayoutKey in digits 8..15.
            // The primaryLanguageCode of that key is the  of the language .
            //   THIS IS NOT neccessaryly the same languagecode as the keyboards language code. Any keyboard can be registered under any language (Windowsproperties»timeAndLanguage»LanguageAndRegion»language»languageOptions»addKeyboard)
            //   To map this to the actual layoutKey look at "Computer\HKEY_CURRENT_USER\Keyboard Layout\Preload" for the aktual layoutKey
            // InputLanguage.CurrentInputLanguage.Handle returns sth. like: FFFFFFFFF0CE0407
            //   InputLanguage.CurrentInputLanguage.Handle.ToString("X")[8..]; // same result as GetKeyboardLayout(GetCurrentWin32ThreadId()); 

            // InputLanguage.CurrentInputLanguage.Handle returns:
            // if the active kbdLayout has a Layout ID:
            //   [FFFFFFFFFXXXLLLL] where the digits 0..9 are the HexValue F, the digits 10..12 (XXX) are the digits 1..3 of the "Layout Id" and the digits 13..16 (LLLL) are the LanguageCode of the culture under which the keyboard was registered to the system.
            //   THIS IS NOT neccessaryly the same languagecode as the keyboards languagecode. Any keyboard can be registered under any language (Windowsproperties»timeAndLanguage»LanguageAndRegion»language»languageOptions»addKeyboard)
            // if the active kbdLayout has no Layout ID:
            //   [LLLLLLL] where the digits 0..2 are equal to digits 4..6 and the digits 3..6 are the language code
            //   I pad this to 8, since the entries if represented as strings are usually 8 long
            hasID = false;
            string res = InputLanguage.CurrentInputLanguage.Handle.ToString("X").PadLeft(8, '0');
            if (res.Length > 8)
            {
                hasID = true;
                res = res.Substring(8);
            }

            return res;
        }

        private static bool TryAddSortOrder(string currentCultureLCID, string LayoutId)
        {
            // since we register the layout to the current culture,
            // it should be possible to just copy the values for CLSID and Profile from the current layout
            // this is whats done here

            // maybe this way it is possible to register layouts under Korean and use the TIP

            //HKEY_CURRENT_USER\Software\Microsoft\CTF\SortOrder\AssemblyItem\LCID\GUID\< 8 - digit - decimal - counter >
            
            const string valNameClsid = "CLSID";
            const string valNameKbdLayout = "KeyboardLayout";
            const string valNameProfile = "Profile";

            try
            {
                string pathToLCID = @"Software\Microsoft\CTF\SortOrder\AssemblyItem\0x" + currentCultureLCID.ToLower().PadLeft(8, '0');
                string[] guids;
                using (RegistryKey? sortOrderLCID = Registry.CurrentUser.OpenSubKey(pathToLCID))
                {
                    if (sortOrderLCID == null)
                    { return false; } 
                    guids = sortOrderLCID.GetSubKeyNames();
                }

                // get the guid that has the subkey with the currently used keyboardLayout and get the highest Subkey <8 - digit - decimal - counter>
                string currentGuid = "";
                int highestSubKey = -1;// count starts at 0
                object? currentCLSID = "";
                object? currentProfile = "";
                string kbdHandle = GetCurrentLayoutHandle(out bool _);

                foreach (var guid in guids)
                {
                    bool containsCurrent = false;
                    using (RegistryKey? sortOrderGUID = Registry.CurrentUser.OpenSubKey(pathToLCID + "\\" + guid))
                    {
                        if (sortOrderGUID == null)
                        { continue; }

                        highestSubKey = -1; // count starts at 0
                        var subs = sortOrderGUID.GetSubKeyNames();
                        
                        foreach (string sub in subs)
                        {
                            using (RegistryKey? subEntry = sortOrderGUID.OpenSubKey(sub))
                            {
                                if (subEntry == null)
                                { continue; }

                                var subkbdLayoutHandle = subEntry.GetValue(valNameKbdLayout);
                                if (subkbdLayoutHandle == null)
                                { continue; }

                                string strsubkbdLayoutHandle = (((uint)(int)subkbdLayoutHandle).ToString("X"))!.PadLeft(8, '0');

                                if (string.Equals(kbdHandle.ToLower(), strsubkbdLayoutHandle.ToLower()))
                                { 
                                    containsCurrent = true;
                                    currentCLSID = subEntry.GetValue(valNameClsid);
                                    currentProfile = subEntry.GetValue(valNameProfile);
                                    currentGuid = guid;
                                }
                            }

                            if (int.TryParse(sub, out int subKeyValue))
                            { highestSubKey = Math.Max(subKeyValue, highestSubKey); }
                        }

                        if (containsCurrent)
                        { break; }
                    }
                }

                if (string.IsNullOrEmpty(currentGuid) || currentProfile == null || currentCLSID == null)
                { return false; }

                // add the next Subkey <8 - digit - decimal - counter> to the guid that contains the subkey with the currently used layout
                using (RegistryKey? sortOrderGUID = Registry.CurrentUser.OpenSubKey(pathToLCID + "\\" + currentGuid, true))
                {
                    if (sortOrderGUID == null)
                    { return false; }

                    using (RegistryKey subKey = sortOrderGUID.CreateSubKey((highestSubKey + 1).ToString().PadLeft(8, '0')))
                    {
                        if (subKey == null)
                        { return false; }

                        subKey.SetValue(valNameClsid, currentCLSID.ToString()!, RegistryValueKind.String);
                        subKey.SetValue(valNameKbdLayout, int.Parse(("f" + LayoutId[1..] + currentCultureLCID).ToUpper(), NumberStyles.HexNumber), RegistryValueKind.DWord);
                        subKey.SetValue(valNameProfile, currentProfile.ToString()!, RegistryValueKind.String);
                    }
                }
            }
            catch (Exception)
            { return false; }

            return true;
        }

        private static bool TryCreateSubstitudeEntry(string inputLayoutKey, string cultureLCID, out string layoutSubstituteKey)
        {
            layoutSubstituteKey = "";
            //Computer\HKEY_CURRENT_USER\Keyboard Layout\Substitutes
            
            try
            {
                using (RegistryKey? regSubstitudes = Registry.CurrentUser.OpenSubKey("Keyboard Layout\\Substitutes", true))
                {
                    if (regSubstitudes == null)
                    { return false; }

                    string[] valueNames = regSubstitudes.GetValueNames();
                    int highestSubstitudeNr = 0; // they start to count at 1

                    foreach (var name in valueNames)
                    {
                        if (SubstitudeRegex().IsMatch(name))
                        { highestSubstitudeNr = Math.Max(highestSubstitudeNr, Int32.Parse(name[1..4].ToUpper(), NumberStyles.HexNumber)); }
                    }

                    layoutSubstituteKey = ("d" + (highestSubstitudeNr + 1).ToString("X3") + cultureLCID).ToLower();
                    regSubstitudes.SetValue(layoutSubstituteKey, inputLayoutKey);
                }
            }
            catch (Exception)
            {
                layoutSubstituteKey = "";
                return false; 
            }
            
            return true;
        }
        
        private static bool TryAddLanguageSetting(string currentCultureLcid, string localeDeviceIdentifier)
        {
            // add layout to HKEY_CURRENT_USER\Control Panel\International\User Profile\de-DE
            // where de-DE is the current layout here is a example where 3 uslanguage layouts have been registered under de-DE culture
            // "0407:00060409" = dword:00000001
            // "0407:00070409" = dword:00000002
            // "0407:A0090409" = dword:00000003
            try
            {
                // get current culture name. its in HKEY_CURRENT_USER\Control Panel\International: LocaleName
                string? localeName = "";

                using (RegistryKey? regIntrntnl = Registry.CurrentUser.OpenSubKey("Control Panel\\International", false))
                {
                    if (regIntrntnl == null)
                    { return false; }

                    var locNameObj = regIntrntnl.GetValue("LocaleName");
                    if (locNameObj == null)
                    { return false; }
                    localeName = locNameObj.ToString();
                }

                // add layout to user profile.
                // loading it via loadKeyboardLayout adds it to the end of the list, unless the reorder flag is used
                // so we add it to the end of the dword list to mirror that 
                
                if (localeName == null)
                { return false; }
                
                using (RegistryKey? regUserProfile = Registry.CurrentUser.OpenSubKey("Control Panel\\International\\User Profile\\" + localeName, true))
                {
                    if (regUserProfile == null)
                    { return false; }
                    
                    string[] valueNames = regUserProfile.GetValueNames();
                    uint highestDecimal = 0; // counter of those registry entries starts at 1

                    foreach (var name in valueNames)
                    {
                        if (regUserProfile.GetValueKind(name) == RegistryValueKind.DWord)
                        {
                            // get value of dword stuff
                            var val = regUserProfile.GetValue(name);
                            if (uint.TryParse(val!.ToString(), out uint nameInt))
                            { highestDecimal = Math.Max(highestDecimal, nameInt); }
                        }
                    }

                    // format of value-name is <cultureLanguageCode>:<keyname in "Keyboard Layouts" registry folder upper case>
                    regUserProfile.SetValue(currentCultureLcid.ToUpper() + ":" + localeDeviceIdentifier.ToUpper(),
                        highestDecimal + 1, RegistryValueKind.DWord);
                }
            }
            catch (Exception)
            { return false; }
            return true;
        }
            

        private static bool TryAddPreloadEntry(string entryData)
        {
            //add layout to Computer\HKEY_CURRENT_USER\Keyboard Layout\Preload
            
            try
            {
                using (RegistryKey? regPreload = Registry.CurrentUser.OpenSubKey("Keyboard Layout\\Preload", true))
                {
                    if (regPreload == null)
                    { return false; }

                    string[] valueNames = regPreload.GetValueNames();
                    int highestDecimal = -1;

                    foreach (var name in valueNames)
                    {
                        if (int.TryParse(name, out int nameInt))
                        { highestDecimal = Math.Max(highestDecimal, nameInt); }
                    }

                    if (highestDecimal == -1)
                    { return false; }

                    regPreload.SetValue((highestDecimal + 1).ToString(), entryData.ToLower());
                }            
            }
            catch (Exception)
            { return false; }

            return true;
        }

        private static bool TryCreateKeyboardLayoutsEntry(
            string dllPath, string layoutLanguageCode, string layoutDescription,
            int highestLanguageKey, int lowestLayoutId,
            out string createdEntry, out string createdLayoutId)
        {
            createdEntry = "";
            createdLayoutId = "";
            try
            {
                using (var kbdEntrys = Registry.LocalMachine.OpenSubKey(registryPathKbdEntries, true))
                {
                    if (kbdEntrys == null)
                    { return false; }

                    if (highestLanguageKey < 0xa000)
                    { highestLanguageKey = 0xa000 - 1; }

                    string entry = (highestLanguageKey + 1).ToString("X4").ToLower() + layoutLanguageCode.ToLower();

                    using (var kbdLanKeyEntry = kbdEntrys.CreateSubKey(entry, true))
                    {
                        if (kbdLanKeyEntry == null)
                        { return false; }

                        createdEntry = entry;
                        createdLayoutId = (lowestLayoutId + 1).ToString("X4").ToUpper();
                        kbdLanKeyEntry.SetValue("Layout Id", createdLayoutId, RegistryValueKind.String);
                        kbdLanKeyEntry.SetValue("Layout File", Path.GetFileName(dllPath), RegistryValueKind.String);
                        kbdLanKeyEntry.SetValue("Layout Text", layoutDescription, RegistryValueKind.String);
                    }
                };
                
            }
            catch (Exception)
            { return false; }

            return true;
        }

        private static bool TryUpdateKeyboardLayoutsEntry(string dllPath, string layoutDescription, string existingEntry)
        {
            try
            {
                using (RegistryKey? kbdEntry = Registry.LocalMachine.OpenSubKey(registryPathKbdEntries + "\\" + existingEntry, true))
                {
                    if (kbdEntry == null)
                    { return false; }

                    kbdEntry.SetValue("Layout File", Path.GetFileName(dllPath), RegistryValueKind.String);
                    kbdEntry.SetValue("Layout Text", layoutDescription, RegistryValueKind.String);
                };
            }
            catch (Exception)
            { return false; }

            return true;
        }

        private static bool TryGetRegistryInfo(
            string dllPath, string layoutLanguageCode, 
            out int highestLanguageKey, out int lowestLayoutId, 
            out bool dllEntryExists, out string existingEntry, out string existingEntryLayoutId,
            out bool kbdDllIsActive)
        {
            // registry https://learn.microsoft.com/de-de/dotnet/api/microsoft.win32.registry?view=net-8.0
            // each layout is registered with a Hex number of length 8.
            // the last 4 Hexes represent the language code, the first 4 are a counter

            highestLanguageKey = -1;
            lowestLayoutId = -1;
            dllEntryExists = false;
            existingEntry = "";
            existingEntryLayoutId = "";
            kbdDllIsActive = false;

            string kbdHandle = GetCurrentLayoutHandle(out bool hasId);
            string activeKbdId = hasId ? "0" + kbdHandle[1..4] : "0000";
            
            try
            {
                using (var kbdLayouts = Registry.LocalMachine.OpenSubKey(registryPathKbdEntries))
                {
                    if (kbdLayouts == null)
                    { return false; }
                    var subkeys = kbdLayouts!.GetSubKeyNames();

                    foreach (string subkey in subkeys)
                    {
                        using (var subk = kbdLayouts.OpenSubKey(subkey))
                        {
                            if (subk == null)
                            { continue; }

                            var layoutFile = subk.GetValue("Layout File");
                            var layoutId = subk.GetValue("Layout Id");

                            if (layoutFile != null)
                            {
                                if (layoutFile.ToString() == Path.GetFileName(dllPath))
                                {
                                    dllEntryExists = true;
                                    existingEntry = subkey;

                                    if (layoutId != null)
                                    {
                                        existingEntryLayoutId = layoutId.ToString()!;

                                        if (string.Equals(layoutId.ToString(), activeKbdId))
                                        { kbdDllIsActive = true; }
                                    }
                                }
                            }

                            if (layoutId != null)
                            {
                                string strLayoutId = layoutId.ToString()!;
                                if (strLayoutId.Length == 4)
                                { lowestLayoutId = Math.Max(lowestLayoutId, Int32.Parse(strLayoutId.ToUpper(), NumberStyles.HexNumber)); }
                            }

                            if (Regex.IsMatch(subkey, "^[0-9A-Fa-f]{4}" + layoutLanguageCode + "$"))
                            { highestLanguageKey = Math.Max(highestLanguageKey, Int32.Parse(subkey[..4].ToLower(), NumberStyles.HexNumber)); }
                        };
                    }
                };
            }
            catch (Exception)
            { return false; }

            return true;
        }

        
        [GeneratedRegex("^d[0-9a-f]{7}$")]
        private static partial Regex SubstitudeRegex();

        [GeneratedRegex("^[0-9A-F]{4}$")]
        private static partial Regex LanguageCodeRegex();
    }
}
