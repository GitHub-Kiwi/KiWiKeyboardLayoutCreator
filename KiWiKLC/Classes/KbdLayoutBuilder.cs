using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KiWi_Keyboard_Layout_Creator
{
    /// <summary>
    /// builds the kbd.dll keyboard driver
    /// </summary>
    internal class KbdLayoutBuilder
    {
        #region fields
        private const string resFolderName = "\\Blueprints\\";
        private const string kbdBPHeader = resFolderName + "kbdBlueprint.h";
        private const string kbdBPName = resFolderName + "kbdBlueprint.c";
        private const string kbdNlsBPName = resFolderName + "kbdNlsBlueprint.c";
        private const string kbdBPResource = resFolderName + "kbdBlueprint.rc";
        private const string kbdBPDef = resFolderName + "kbdBlueprint.def";
        private const string kbdBPProj = resFolderName + "kbdBlueprint.vcxproj";
        private const string kbdBPProjFilters = resFolderName + "kbdBlueprint.vcxproj.Filters";
        private const string kbdBPCompile = resFolderName + "compile.ps1";
        #endregion

        #region properties
        internal string kbdBlueprintDestinationFolder = "BP";
        internal List<KeyboardKey> kbdKeys;
        internal FormProperties frmProperties;
        internal FormNumpad frmNumpad;
        #endregion

        #region constructor
        internal KbdLayoutBuilder(List<KeyboardKey> kbdKeys,
            FormProperties frmProperties,
            FormNumpad frmNumpad)
        {
            this.kbdKeys = kbdKeys;
            this.frmProperties = frmProperties;
            this.frmNumpad = frmNumpad;
        }
        #endregion

        #region utility
        private bool TryReadFile(string path, out StringBuilder strFile)
        {
            strFile = new StringBuilder();
            try
            { strFile = new StringBuilder(File.ReadAllText(path)); }
            catch (Exception)
            { return false; }
            return true;
        }

        private bool TrySaveFile(string fileName, ref StringBuilder strFile, out string resFilePath)
        {
            resFilePath = "";
            if (strFile == null || String.IsNullOrEmpty(fileName))
            { return false; }

            try
            {
                var assembly = Assembly.GetEntryAssembly();
                if (assembly == null)
                { return false; }

                string destination = Path.GetDirectoryName(assembly!.Location) + "\\" + kbdBlueprintDestinationFolder.Trim('\\') + '\\';
                if (destination == null)
                { return false; }
                
                Directory.CreateDirectory(destination);

                if (!Path.Exists(destination))
                { return false; }

                resFilePath = destination + fileName;
                File.WriteAllText(resFilePath, strFile.ToString());
            }
            catch (Exception)
            { return false; }
            
            return true;
        }

        private bool HasNlsKeys()
        {
            return kbdKeys.Find((k) => k.IsNlsKey()) != null;
        }
        #endregion

        #region build
        internal bool BuildKbd_Dll(out string driverFilePath, bool readabilityComments = true)
        {
            driverFilePath = "";
            if (!BuildKbd_Def())
            { return false; }
            if (!BuildKbd_Header())
            { return false; }
            if (!BuildKbd_ResourceFile())
            { return false; }
            if (!BuildKbd_vcxproj(out string projFilePath))
            { return false; }
            if (!BuildKbd_Filters())
            { return false; }
            if (!BuildKbd_c(readabilityComments))
            { return false; }
            if (!CopyCompileScript())
            { return false; }

            bool success = false;
            IEnumerable<string>? initialFolderStructure = null;
            string? projDir = null;
            try
            {
                projDir = Path.GetDirectoryName(projFilePath);

                if (projDir == null)
                { return false; }

                initialFolderStructure = Directory.EnumerateDirectories(projDir, "*", SearchOption.AllDirectories).Distinct().ToArray();

                var processInfo = new System.Diagnostics.ProcessStartInfo
                {
                    WorkingDirectory = projDir,
                    Verb = "RunAs",
                    LoadUserProfile = true,
                    FileName = @"powershell.exe",
                    RedirectStandardOutput = false,
                    RedirectStandardInput = true,
                    CreateNoWindow = true,
                };

                var p = System.Diagnostics.Process.Start(processInfo);

                if (p == null)
                { return false; }

                // allow script execution for this process, then execute the compilescript
                p.StandardInput.WriteLine("Set-ExecutionPolicy -ExecutionPolicy Unrestricted -Scope Process");
                p.StandardInput.Flush();
                p.StandardInput.WriteLine(@".\" + Path.GetFileName(kbdBPCompile));
                p.StandardInput.Flush();
                // close standardinput, so that the process exits after the script is done
                p.StandardInput.Close();
                while (!p.HasExited)
                { }

                if (p.ExitCode == 0)
                {
                    var files = Directory.EnumerateFiles(projDir, frmProperties.KbdLayoutName + ".dll", SearchOption.AllDirectories);
                    files = from file in files where Path.GetDirectoryName(file) != projDir select file;

                    if (files.Any())
                    {
                        string resPath = projDir + "\\" + Path.GetFileName(files.First());
                        File.Move(files.First(), resPath, true);
                        driverFilePath = resPath;
                        success = true;
                    }
                }
            }
            catch (Exception)
            { success = false; }
            
            // delete created folders
            try
            {
                var folderStructure = Directory.EnumerateDirectories(projDir!, "*", SearchOption.AllDirectories);
                var createdFolders = folderStructure.Except(initialFolderStructure!);

                foreach (var folder in createdFolders)
                {
                    if (Path.Exists(folder))
                    { Directory.Delete(folder, true); }
                }
            }
            catch (Exception)
            { 
                // access rights might make trouble, not cleaning up is not the end of the world
            }

            return success;
        }

        private bool BuildKbd_Def()
        {
            if (!TryReadFile(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location) + kbdBPDef
                , out StringBuilder strFile))
            { return false; }
            
            strFile.Replace("KBDTARGETNAME", frmProperties.KbdLayoutName.ToUpper());

            if (HasNlsKeys())
            { strFile.Replace("KBDNLSLAYERDESCRIPTOR", "    KbdNlsLayerDescriptor @2"); }
            else
            { strFile.Replace("KBDNLSLAYERDESCRIPTOR", ""); }
            
            return TrySaveFile(string.Concat(frmProperties.KbdLayoutName, kbdBPDef.AsSpan(kbdBPDef.IndexOf('.'))), ref strFile, out _);
        }

        private bool BuildKbd_Header()
        {
            if (!TryReadFile(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location) + kbdBPHeader
                , out StringBuilder strFile))
            { return false; }

            strFile.Replace("/*def_KBD_TYPE*/", "#define KBD_TYPE " + frmProperties.KbdLayoutType);

            return TrySaveFile(string.Concat(frmProperties.KbdLayoutName, kbdBPHeader.AsSpan(kbdBPHeader.IndexOf('.'))), ref strFile, out _);
        }

        private bool BuildKbd_ResourceFile()
        {
            if (!TryReadFile(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location) + kbdBPResource
                , out StringBuilder strFile))
            { return false; }

            strFile.Replace("/*FILEDESCRIPTION*/", "\"" + frmProperties.KbdLayoutDescriptionText + "\"" );
            strFile.Replace(
                "/*INTERNALNAME*/", "\"" + frmProperties.KbdLayoutName
                + " (" + frmProperties.KbdLayoutVersion + ")\""
                );
            strFile.Replace("/*ORIGINALFILENAME*/", "\"" + frmProperties.KbdLayoutName + ".dll\"");

            return TrySaveFile(string.Concat(frmProperties.KbdLayoutName, kbdBPResource.AsSpan(kbdBPResource.IndexOf('.'))), ref strFile, out _);
        }

        private bool BuildKbd_vcxproj(out string projFileName)
        {
            projFileName = "";

            if (!TryReadFile(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location) + kbdBPProj
                , out StringBuilder strFile))
            { return false; }

            // additional include directories könnte vlt. problematisch sein
            strFile.Replace("<TargetName>kbdTargetName</TargetName>",
                "<TargetName>"
                + frmProperties.KbdLayoutName
                + "</TargetName>");
            strFile.Replace("<ProjectName>kbdBlueprint</ProjectName>",
                "<ProjectName>"
                + frmProperties.KbdLayoutName
                + "</ProjectName>");

            strFile.Replace("kbdBlueprint.", frmProperties.KbdLayoutName + ".");

            return TrySaveFile(string.Concat(frmProperties.KbdLayoutName, kbdBPProj.AsSpan(kbdBPProj.IndexOf('.'))), ref strFile, out projFileName);
        }
        
        private bool BuildKbd_Filters()
        {
            if (!TryReadFile(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location) + kbdBPProjFilters
                , out StringBuilder strFile))
            { return false; }

            strFile.Replace("kbdBlueprint.", frmProperties.KbdLayoutName + ".");

            return TrySaveFile(string.Concat(frmProperties.KbdLayoutName, kbdBPProjFilters.AsSpan(kbdBPProjFilters.IndexOf('.'))), ref strFile, out _);
        }

        private bool CopyCompileScript()
        {
            if (!TryReadFile(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location) + kbdBPCompile
                , out StringBuilder strFile))
            { return false; }
            
            return TrySaveFile(Path.GetFileName(kbdBPCompile), ref strFile, out _);
        }

        private bool BuildKbd_c(
            bool readabilityComments = true)
        {
            const string strKeyNames = "/*key_names*/";
            const string strKeyNamesExt = "/*key_names_ext*/";
            //const string strKeyNamesDead = "/*key_names_dead*/";
            const string strScancodeToVk = "/*scancode_to_vk*/";
            const string strScancodeToVkE0 = "/*scancode_to_vk_e0*/";
            const string strScancodeToVkE1 = "/*scancode_to_vk_e1*/";
            const string strVkToBits = "/*vk_to_bits*/";
            const string strCharModifiersWMaxModBits = "/*char_modifiers_wMaxModBits*/";
            const string strCharModifiersModNumber = "/*char_modifiers_ModNumber*/";
            const string strVkToWcharX = "/*vk_to_wcharX*/";
            const string strVkToWchar = "/*vk_to_wchar*/";
            //const string strDeadKeys = "/*dead_keys*/";
            const string strFLocaleFlags = "/*fLocaleFlags*/";
            const string strNlsVkToFuncTable = "/*VkToFuncTableContent*/";
            const string strNlsVkToFuncTableEntryCount = "/*VkToFuncTableEntryCount*/";
            const string strKbdnls = "/*KBDNLS*/";

            if (!TryReadFile(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location) + kbdBPName
                , out StringBuilder strFile))
            { return false; }

            var sortedKeys = kbdKeys.Where(x => x.Scancode != null).OrderBy(x => x.Scancode);
            var nlfKeys = from key in kbdKeys where key.IsNlsKey() select key;

            if (sortedKeys == null)
            { return false; }
            else if (!sortedKeys.Any())
            { return false; }

            StringBuilder strReplace = new();

            #region keynames
            strReplace.Clear();

            if (readabilityComments)
            { strReplace.Append("//{scancode, keyname}, \r\n    "); }
            foreach (var key in sortedKeys)
            {
                if (string.IsNullOrEmpty(key.KeyName) || key.Scancode == null || key.E0)
                { continue; }
                if (readabilityComments && key.Vk == null)
                { continue; }

                strReplace.Append("{0x");
                strReplace.Append(((int)key.Scancode!).ToString("X2"));
                strReplace.Append(", L\"");
                strReplace.Append(key.KeyName);
                strReplace.Append("\"}, ");
                if (readabilityComments)
                {
                    strReplace.Append("//VK:");
                    strReplace.Append(((int)key.Vk!).ToString("X2"));
                }
                strReplace.Append("\r\n    ");
            }

            strFile = strFile.Replace(strKeyNames, strReplace.ToString().TrimEnd());
            #endregion

            #region keyNamesExt
            strReplace.Clear();
            if (readabilityComments)
            { strReplace.Append("//{scancode, keyname}, \r\n    "); }
            foreach (var key in sortedKeys)
            {
                if (string.IsNullOrEmpty(key.KeyName) || !key.E0)
                { continue; }

                strReplace.Append("{0x");
                strReplace.Append(((int)key.Scancode!).ToString("X2"));
                strReplace.Append(", L\"");
                strReplace.Append(key.KeyName);
                strReplace.Append("\"}, ");
                if (readabilityComments)
                {
                    strReplace.Append("//VK:");

                    if (key.Vk != null)
                    { strReplace.Append(((int)key.Vk!).ToString("X2")); }
                    else
                    { strReplace.Append("null"); }
                }
                strReplace.Append("\r\n    ");
            }
            strFile = strFile.Replace(strKeyNamesExt, strReplace.ToString().TrimEnd());
            #endregion

            // TODO key names dead

            #region scancode_to_vk
            strReplace.Clear();
            int prevScancode = 0;
            strReplace.Append("VK__none_,\r\n    ");
            foreach (var key in sortedKeys)
            {
                if (key.E0)
                { continue; }

                while (key.Scancode! - prevScancode > 1)
                {
                    strReplace.Append("VK__none_,");
                    prevScancode++;
                    if (readabilityComments)
                    {
                        strReplace.Append("//Scancode:");
                        strReplace.Append(prevScancode);
                    }
                    strReplace.Append("\r\n    ");
                }

                prevScancode = (int)key.Scancode!;

                if (key.Vk == null)
                { strReplace.Append("VK__none_,\r\n    "); }
                else
                {
                    strReplace.Append("0x");
                    strReplace.Append(((int)key.Vk!).ToString("X2"));

                    if (key.VKHandlingMask != 0x00)
                    {
                        strReplace.Append(" | 0x");
                        strReplace.Append(key.VKHandlingMask.ToString("X2"));
                    }
                    strReplace.Append(',');
                    if (readabilityComments)
                    {
                        strReplace.Append("//Scancode:");
                        strReplace.Append(prevScancode);
                        strReplace.Append(" Keyname:");
                        strReplace.Append(key.KeyName);
                    }
                    strReplace.Append("\r\n    ");
                }
            }
            strFile.Replace(strScancodeToVk, strReplace.ToString().TrimEnd([',', ' ', '\r', '\n']));
            #endregion

            #region scancode_to_vk0
            strReplace.Clear();
            foreach (var key in sortedKeys)
            {
                if (key.Vk == null || !key.E0)
                { continue; }
                else
                {
                    strReplace.Append("{ 0x");
                    strReplace.Append(((int)key.Scancode!).ToString("X2"));
                    strReplace.Append(", 0x");
                    strReplace.Append(((int)key.Vk!).ToString("X2"));
                    if (key.VKHandlingMask != 0x00)
                    {
                        strReplace.Append(" | 0x");
                        strReplace.Append(key.VKHandlingMask.ToString("X2"));
                    }
                    strReplace.Append("},");
                    if (readabilityComments)
                    {
                        strReplace.Append("//Scancode:");
                        strReplace.Append(((int)key.Scancode!));
                        strReplace.Append(" Keyname:");
                        strReplace.Append(key.KeyName);
                    }
                    strReplace.Append("\r\n    ");
                }
            }
            strFile.Replace(strScancodeToVkE0, strReplace.ToString().TrimEnd());
            #endregion

            #region scancode_to_vk1
            // TODO maybe add support for customized E1 scancode translations
            strReplace.Clear();
            strFile.Replace(strScancodeToVkE1, "{0x1D, VK_PAUSE},\r\n    ");
            #endregion

            #region Layer query
            //find which layers are used and how often
            strReplace.Clear();
            Dictionary<int, int> usedLayers_Cnt = [];
            foreach (var key in sortedKeys)
            {
                if (key.Vk == null)
                { continue; }
                foreach (var layer in key.LayerToWCHAR.Keys)
                {
                    if (!usedLayers_Cnt.TryAdd(layer, 1))
                    { usedLayers_Cnt[layer]++; }
                }
            }
            // add numpad keys to usedLayers_Cnt
            HashSet<int> numpadLayers = frmNumpad.GetUsedLayers();
            foreach (int numpadLayer in numpadLayers)
            {
                // only try add them, to ensure the used layes are present. They dont count to the overall count of a layer, because numpad keys get their own table
                usedLayers_Cnt.TryAdd(numpadLayer, 1);
            }

            Dictionary<KbdLayers, int> usedBMs_Cnt = [];
            foreach (var layer in usedLayers_Cnt)
            {
                foreach (var bitmod in LayersFunc.GetEnumsFromLayer(layer.Key))
                {
                    if (!usedBMs_Cnt.TryAdd(bitmod, layer.Value))
                    { usedBMs_Cnt[bitmod] += layer.Value; }
                }
            }
            #endregion

            #region vk_to_bits
            var usedBMsSorted = from bm in usedBMs_Cnt where bm.Key != KbdLayers.KBDBASE orderby bm.Key ascending select bm.Key;
            var VKbyBM = frmProperties.GetBitmodVKMapping();
            foreach (var BM in usedBMsSorted)
            {
                strReplace.Append("{0x");
                strReplace.Append(((int)VKbyBM[BM]).ToString("X2"));
                strReplace.Append(", 0x");
                strReplace.Append(((int)BM).ToString("X2"));
                strReplace.Append("},");
                if (readabilityComments)
                {
                    strReplace.Append("//VK:");
                    strReplace.Append(VKbyBM[BM].ToString());
                    strReplace.Append(" BitModifier:");
                    strReplace.Append(BM.ToString());
                    strReplace.Append(" Bit:");
                    strReplace.Append(Convert.ToString((int)BM, 2).PadLeft(usedBMsSorted.Count(), '0'));
                }
                strReplace.Append("\r\n    ");
            }
            strFile.Replace(strVkToBits, strReplace.ToString().TrimEnd());
            #endregion

            #region CharModifiersModNumber
            strReplace.Clear();
            var usedLayersSorted = (from lyr in usedLayers_Cnt where lyr.Key != 0x00 orderby lyr.Value descending select lyr.Key).ToList();
            int maxLayerMask = Convert.ToInt32(2 * (int)usedBMsSorted.Last());
            int usedLyrsCnt = usedLayersSorted.Count;
            int lyrsAdded = 0;
            int linesAdded = 0;

            strReplace.Append("0, ");
            if (readabilityComments)
            {
                strReplace.Append("// ");
                strReplace.Append("".PadLeft(usedBMsSorted.Count(), '0'));
                strReplace.Append(" = no bitmod ");
            }
            strReplace.Append("\r\n        ");
            for (int layerMask = 1; layerMask < maxLayerMask && lyrsAdded < usedLyrsCnt; layerMask++)
            {
                if (usedLayers_Cnt.ContainsKey(layerMask))
                {
                    strReplace.Append(usedLayersSorted.IndexOf(layerMask) + 1);
                    strReplace.Append(", ");
                    if (readabilityComments)
                    {
                        strReplace.Append("// ");
                        strReplace.Append(Convert.ToString(layerMask, 2).PadLeft(usedBMsSorted.Count(), '0'));
                    }
                    strReplace.Append("\r\n        ");
                    lyrsAdded++;
                }
                else
                {
                    strReplace.Append("SHFT_INVALID, ");
                    if (readabilityComments)
                    {
                        strReplace.Append("// ");
                        strReplace.Append(Convert.ToString(layerMask, 2).PadLeft(usedBMsSorted.Count(), '0'));
                    }
                    strReplace.Append("\r\n        ");
                }

                linesAdded++;
            }
            strFile.Replace(strCharModifiersModNumber, strReplace.ToString().TrimEnd([',', ' ', '\r', '\n']));
            #endregion

            #region CharModifiersWMaxModBits
            strReplace.Clear();
            strReplace.Append(".wMaxModBits = ");
            strReplace.Append(linesAdded);
            strReplace.Append(',');
            strFile.Replace(strCharModifiersWMaxModBits, strReplace.ToString());
            #endregion

            #region VkToWcharX
            void parseKey(KeyboardKey pkey, ref StringBuilder strReplace, ref List<int> parsedLayers, ref List<KeyboardKey> keysNotParsed)
            {
                keysNotParsed.Remove(pkey);

                if (pkey.Vk == null)
                { return; }

                strReplace.Append("{0x");
                strReplace.Append(((int)pkey.Vk!).ToString("X2"));
                strReplace.Append(",\t0x");
                strReplace.Append(pkey.Lockmask.ToString("X2"));

                foreach (int pLayer in parsedLayers)
                {
                    strReplace.Append(",\t");
                    if (pkey.LayerToWCHAR.TryGetValue(pLayer, out char wchar))
                    {
                        strReplace.Append("0x");
                        strReplace.Append(((int)wchar).ToString("X4"));
                    }
                    else
                    { strReplace.Append("WCH_NONE"); }
                }
                strReplace.Append("},");
                if (readabilityComments)
                {
                    strReplace.Append("// KeyName: ");
                    strReplace.Append(pkey.KeyName);
                }
                strReplace.Append("\r\n    ");
            }

            void parseLayerEnd(ref StringBuilder strReplace, ref List<int> parsedLayers)
            {
                strReplace.Append("{0"); //Vk
                strReplace.Append(",\t0"); //Lockmask

                foreach (int pLayer in parsedLayers)
                {
                    strReplace.Append("\t,0");
                }
                strReplace.Append("}\r\n");

                strReplace.Append("};\r\n\r\n");
            }

            strReplace.Clear();

            List<int> parsedLayers = [];
            List<KeyboardKey> keysNotParsed = new(kbdKeys);
            List<(string, int)> lstTablesSuffixMaskSize = [];
            const string strTableName = "vk_to_wchar";
            usedLayersSorted.Insert(0, 0x00);

            // remove keys that have no wchar and put numpad in special list to handel them last
            for (int i = 0; i < keysNotParsed.Count; i++)
            {
                if (keysNotParsed[i].LayerToWCHAR.Count == 0)
                {
                    keysNotParsed.RemoveAt(i);
                    i--;
                }
                //else if ((keysNotParsed[i].VKHandlingMask & (int)VKHandlingMask.KBDNUMPAD) == (int)VKHandlingMask.KBDNUMPAD)
                //{
                //    if (keysNotParsed[i].LayerToWCHAR.Count > 0)
                //    {
                //        numpadKeysNotParsed.Add(keysNotParsed[i]);
                //    }

                //    keysNotParsed.RemoveAt(i);
                //    i--;
                //}
            }

            foreach (int layer in usedLayersSorted)
            {
                parsedLayers.Add(layer);

                var keysToParse = from key in keysNotParsed where key.ListCoversLayers(parsedLayers) && key.Vk != null orderby (int)key.Vk! select key;

                if (!keysToParse.Any())
                { continue; }

                lstTablesSuffixMaskSize.Add((parsedLayers.Count.ToString(), parsedLayers.Count));

                strReplace.Append("static ALLOC_SECTION_LDATA VK_TO_WCHARS");
                strReplace.Append(parsedLayers.Count);
                strReplace.Append(' ');
                strReplace.Append(strTableName);
                strReplace.Append(parsedLayers.Count);
                strReplace.Append("[] = { \r\n    ");

                foreach (var pkey in keysToParse)
                { parseKey(pkey, ref strReplace, ref parsedLayers, ref keysNotParsed); }

                parseLayerEnd(ref strReplace, ref parsedLayers);
            }

            parsedLayers.Clear();

            // numpad vkToWchar table (has to come last) 
            List<KeyboardKey> numpadKeysNotParsed = new(frmNumpad.GetKeyboardKeys());

            foreach (int layer in usedLayersSorted)
            {
                parsedLayers.Add(layer);

                bool layersCoverNumpadKeys = true;

                foreach (var numpadLayer in numpadLayers)
                {
                    if (!parsedLayers.Contains(numpadLayer))
                    {
                        layersCoverNumpadKeys = false;
                        break;
                    }
                }

                if (!layersCoverNumpadKeys)
                { continue; }

                lstTablesSuffixMaskSize.Add(("Numpad", parsedLayers.Count));

                strReplace.Append("static ALLOC_SECTION_LDATA VK_TO_WCHARS");
                strReplace.Append(parsedLayers.Count);
                strReplace.Append(' ');
                strReplace.Append(strTableName);
                strReplace.Append("Numpad");
                strReplace.Append("[] = { \r\n    ");


                foreach (var numpadVK in Enum.GetValues<FormNumpad.NumpadVK>())
                { parseKey(frmNumpad.GetKeyboardKey(numpadVK), ref strReplace, ref parsedLayers, ref numpadKeysNotParsed); }

                parseLayerEnd(ref strReplace, ref parsedLayers);

                break;
            }


            strFile.Replace(strVkToWcharX, strReplace.ToString());
            #endregion

            #region strVkToWchar
            strReplace.Clear();

            foreach ((string, int) tableEntry in lstTablesSuffixMaskSize)
            {
                strReplace.Append("{(PVK_TO_WCHARS1)");
                strReplace.Append(strTableName);
                strReplace.Append(tableEntry.Item1);
                strReplace.Append(", ");
                strReplace.Append(tableEntry.Item2);
                strReplace.Append(", sizeof(");
                strReplace.Append(strTableName);
                strReplace.Append(tableEntry.Item1);
                strReplace.Append("[0])},\r\n    ");
            }
            strFile.Replace(strVkToWchar, strReplace.ToString().TrimEnd());
            #endregion

            // strDeadKeys Todo 

            # region fLocaleFlags
            strReplace.Clear();
            strReplace.Append("MAKELONG(0x");
            strReplace.Append(frmProperties.GetKLLFMask().ToString("X4"));
            strReplace.Append(", KBD_VERSION),");
            if (readabilityComments)
            {
                strReplace.Append(" // ");
                strReplace.Append("KLLF_ALTGR:");
                strReplace.Append(frmProperties.KLLF_AltGr);
                strReplace.Append(", KLLF_SHIFTLOCK:");
                strReplace.Append(frmProperties.KLLF_Shiftlock);
                strReplace.Append(", KLLF_LRM_RLM:");
                strReplace.Append(frmProperties.KLLF_LrmRml);
            }
            strFile.Replace(strFLocaleFlags, strReplace.ToString());
            #endregion

            #region NlsKeys
            // read NlsPattern file, fill it and paste the result in the final Pattern
            strReplace.Clear();
            if (HasNlsKeys())
            {
                StringBuilder? strNlsFile = null;
                bool fileLoaded = true;

                try
                { strNlsFile = new StringBuilder(File.ReadAllText(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location) + kbdNlsBPName)); }
                catch (Exception)
                { fileLoaded = false; }

                if (fileLoaded)
                {
                    int nlsEntries = 0;

                    foreach (var key in nlfKeys)
                    {
                        if (key.Vk == null)
                        { continue; }

                        if (nlsEntries != 0)
                        { strReplace.Append(",\r\n    "); }

                        nlsEntries++;

                        strReplace.Append("{\r\n        ");

                        strReplace.Append("0x");//base VK
                        strReplace.Append(((int)key.Vk!).ToString("X2"));
                        strReplace.Append(",\r\n        ");
                        
                        // NLSFEProcType
                        if (key.NlsKeyUpMask == 0)
                        { strReplace.Append("KBDNLS_TYPE_NORMAL,\r\n        "); }
                        else
                        { strReplace.Append("KBDNLS_TYPE_TOGGLE,\r\n        "); }
                        // NLSFEProcCurrent
                        strReplace.Append("KBDNLS_INDEX_NORMAL,\r\n        ");
                        // NLSFEProcSwitch
                        strReplace.Append("0x");
                        strReplace.Append(key.NlsKeyUpMask.ToString("X2"));
                        strReplace.Append(",\r\n        ");

                        // NLSFEProc
                        strReplace.Append("{\r\n            ");
                        //foreach layer from base until shift+control+alt
                        for (int layer = 0; layer < 8; layer++)
                        {
                            if (layer > 0)
                            { strReplace.Append(",\r\n            "); }

                            if (key.LayerToNls.TryGetValue(layer, out NLSPair? nlsPair))
                            {
                                if (nlsPair.NlsType == NlsType.NULL || nlsPair.NlsType == NlsType.SEND_BASE_VK)
                                { strReplace.Append("{KBDNLS_SEND_BASE_VK,0}"); }
                                else
                                {
                                    strReplace.Append("{0x");
                                    strReplace.Append(((int)nlsPair.NlsType).ToString("X2"));

                                    if (nlsPair.NlsType == NlsType.SEND_PARAM_VK && nlsPair.NlsVk != null)
                                    {
                                        strReplace.Append(", 0x");
                                        strReplace.Append(((int)nlsPair.NlsVk!).ToString("X2"));
                                    }
                                    else
                                    { strReplace.Append(", 0"); }

                                    strReplace.Append('}');
                                }
                            }
                            else
                            { strReplace.Append("{KBDNLS_SEND_BASE_VK,0}"); }
                        }
                        strReplace.Append("\r\n        },\r\n        ");
                        
                        // NLSFEProcAlt
                        strReplace.Append("{\r\n            ");
                        //foreach layer from base until shift+control+alt
                        for (int layer = 0; layer < 8; layer++)
                        {
                            if (layer > 0)
                            { strReplace.Append(",\r\n            "); }

                            if (key.LayerToNlsKeyUp.TryGetValue(layer, out NLSPair? nlsUpPair))
                            {
                                if (nlsUpPair.NlsType == NlsType.NULL || nlsUpPair.NlsType == NlsType.SEND_BASE_VK)
                                { strReplace.Append("{KBDNLS_SEND_BASE_VK,0}"); }
                                else
                                {
                                    strReplace.Append("{0x");
                                    strReplace.Append(((int)nlsUpPair.NlsType).ToString("X2"));

                                    if (nlsUpPair.NlsType == NlsType.SEND_PARAM_VK && nlsUpPair.NlsVk != null)
                                    {
                                        strReplace.Append(", 0x");
                                        strReplace.Append(((int)nlsUpPair.NlsVk!).ToString("X2"));
                                    }
                                    else
                                    { strReplace.Append(", 0"); }

                                    strReplace.Append('}');
                                }
                            }
                            else
                            { strReplace.Append("{KBDNLS_SEND_BASE_VK,0}"); }
                        }
                        strReplace.Append("\r\n        },\r\n    }");
                    }

                    strNlsFile?.Replace(strNlsVkToFuncTable, strReplace.ToString());
                    strNlsFile?.Replace(strNlsVkToFuncTableEntryCount, nlsEntries.ToString());

                    strFile.Replace(strKbdnls, strNlsFile?.ToString());
                }
            }
            #endregion

            return TrySaveFile(string.Concat(frmProperties.KbdLayoutName, kbdBPName.AsSpan(kbdBPName.IndexOf('.'))), ref strFile, out _);
        }
        #endregion
    }
}
