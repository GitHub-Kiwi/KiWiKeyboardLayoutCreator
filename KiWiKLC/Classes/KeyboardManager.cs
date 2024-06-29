using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection.Emit;

namespace KiWi_Keyboard_Layout_Creator
{
    /// <summary>
    /// coordinates the Keys properties to ensure they are compatible with the limitations of the windows keyboarddriver. 
    /// Limitations: Each scancode can only be associated with one vk and every vk can only have one one NLS-behaviour / WCHAR per  layer.
    /// </summary>
    internal class KeyboardManager
    {
        #region properties
        private List<KeyboardKey> keys = [];
        private readonly Dictionary<KeyboardKey, KeyboardKey> keysPreviousState = [];
        /// <summary>
        /// returns a list with all keys, adding removing keys her has no effect on the keys in KeyboardManager, use Remove-/Clear-/Add- functions for that
        /// </summary>
        public List<KeyboardKey> Keys { get { return new List<KeyboardKey>(keys); } }
        public void RemoveKey(KeyboardKey key) 
        { 
            RemoveKeyEvents(key);
            keys.Remove(key); 
            keysPreviousState.Remove(key);
        }
        public void ClearKeys()
        {
            foreach (var key in keys)
            {
                RemoveKeyEvents(key);
                keysPreviousState.Remove(key);
            }
            keys.Clear();
        }
        public void AddKey(KeyboardKey key) 
        { 
            keys.Add(key); 
            keysPreviousState.Add(key, new KeyboardKey().CopyProperties(key));
        }
        #endregion

        #region Json Import Export
        internal JsonArray JsonExport()
        {

            JsonArray arr = [];
            foreach (var key in Keys)
            { arr.Add(key.JsonExport()); }
            
            return arr;
        }

        internal void JsonImport(JsonNode? arrKeys)
        {
            if (arrKeys == null)
            { return; }
            if (arrKeys.GetValueKind() != JsonValueKind.Array)
            { return; }

            List<KeyboardKey> tmpkeys = [];
            foreach (var item in arrKeys!.AsArray())
            {
                KeyboardKey key = new(item!.AsObject());
                RegisterKeyEvents(key);
                tmpkeys.Add(key);
                keysPreviousState.Add(key, new KeyboardKey().CopyProperties(key));
            }

            keys = tmpkeys;
        }
        #endregion

        #region event handlers
        private void RemoveKeyEvents(KeyboardKey key)
        {
            key.ScancodeChange -= Key_ScancodeChange;
            key.E0Change -= Key_E0Change;
            key.VkChange -= Key_VkChange;
            key.LayerToWCHAREntryChanged -= Key_LayerToWCHAREntryChanged;
            key.LayerToNlsEntryChanged -= Key_LayerToNlsEntryChanged;
            key.LayerToNlsKeyUpEntryChanged -= Key_LayerToNlsKeyUpEntryChanged;
            key.NlsKeyUpMaskChange -= Key_NlsKeyUpMaskChange;
        }

        private void RegisterKeyEvents(KeyboardKey key) 
        {
            key.ScancodeChange += Key_ScancodeChange;
            key.E0Change += Key_E0Change;
            key.VkChange += Key_VkChange;
            key.LayerToWCHAREntryChanged += Key_LayerToWCHAREntryChanged;
            key.LayerToNlsEntryChanged += Key_LayerToNlsEntryChanged;
            key.LayerToNlsKeyUpEntryChanged += Key_LayerToNlsKeyUpEntryChanged;
            key.NlsKeyUpMaskChange += Key_NlsKeyUpMaskChange;
        }

        private void Key_NlsKeyUpMaskChange(object? sender, ValueChangedEventArgs<byte> e)
        {
            if (sender is not KeyboardKey || e == null)
            { return; }

            ApplyNlsKeyUpMaskChange((KeyboardKey)sender, e.NewValue);
            keysPreviousState[(KeyboardKey)sender].CopyProperties((KeyboardKey)sender);
        }

        private void Key_LayerToNlsKeyUpEntryChanged(object? sender, EventDictionary<int, NLSPair>.EntryChangedEventArgs e)
        {
            if (sender is not KeyboardKey || e == null)
            { return; }

            ApplyNlsKeyUpEntryChange((KeyboardKey)sender, e.Key, e.NewValue, e.ChangeType);
            keysPreviousState[(KeyboardKey)sender].CopyProperties((KeyboardKey)sender);
        }

        private void Key_LayerToNlsEntryChanged(object? sender, EventDictionary<int, NLSPair>.EntryChangedEventArgs e)
        {
            if (sender is not KeyboardKey || e == null)
            { return; }

            ApplyNlsChange((KeyboardKey)sender, e.Key, e.NewValue, e.ChangeType);
            keysPreviousState[(KeyboardKey)sender].CopyProperties((KeyboardKey)sender);
        }

        private void Key_LayerToWCHAREntryChanged(object? sender, EventDictionary<int, char>.EntryChangedEventArgs e)
        {
            if (sender is not KeyboardKey || e == null)
            { return; }

            ApplyWCharChange((KeyboardKey)sender, e.Key, e.NewValue, e.ChangeType);
            keysPreviousState[(KeyboardKey)sender].CopyProperties((KeyboardKey)sender);
        }
        
        private void Key_VkChange(object? sender, ValueChangedEventArgs<VK?> e)
        {
            if (sender is not KeyboardKey || e == null)
            { return; }

            HandleVkConflict((KeyboardKey)sender, out bool conflictExisted);
            if(!conflictExisted)
            { ApplyVkChange((KeyboardKey)sender, e.NewValue); }
            keysPreviousState[(KeyboardKey)sender].CopyProperties((KeyboardKey)sender);
        }

        private void Key_ScancodeChange(object? sender, ValueChangedEventArgs<int?> e)
        {
            if (sender is not KeyboardKey)
            { return; }
            HandleScancodeConflict((KeyboardKey)sender);
            keysPreviousState[(KeyboardKey)sender].CopyProperties((KeyboardKey)sender);
        }

        private void Key_E0Change(object? sender, ValueChangedEventArgs<bool> e)
        {
            if (sender is not KeyboardKey)
            { return; }
            HandleScancodeConflict((KeyboardKey)sender);
            keysPreviousState[(KeyboardKey)sender].CopyProperties((KeyboardKey)sender);
        }
        #endregion

        #region handle changes
        private void ApplyNlsKeyUpEntryChange(KeyboardKey key, int layer, NLSPair? newValue, EventDictionaryChangeType changeType)
        {
            // apply the change to all Keys with the same Vk.
            var otherKeys = from k2 in Keys where k2.Vk == key!.Vk && k2 != key select k2;

            foreach (var otherKey in otherKeys)
            {
                if (changeType == EventDictionaryChangeType.Remove)
                { otherKey.LayerToNlsKeyUp.Remove(layer); }
                else
                { otherKey.LayerToNlsKeyUp[layer] = newValue!; }
            }
        }

        private void ApplyNlsChange(KeyboardKey key, int layer, NLSPair? newValue, EventDictionaryChangeType changeType)
        {
            // apply the Nls change to all Keys with the same VK.
            var otherKeys = from k2 in Keys where k2.Vk == key!.Vk && k2 != key select k2;

            foreach (var otherKey in otherKeys)
            {
                if (changeType == EventDictionaryChangeType.Remove)
                { otherKey.LayerToNls.Remove(layer); }
                else
                { otherKey.LayerToNls[layer] = newValue!; }
            }
        }

        private void ApplyWCharChange(KeyboardKey key, int layer, char? newValue, EventDictionaryChangeType changeType)
        {
            // apply the Wchar change to all Keys with the same scancode. Here we assume, that vk conflicts already are taken care of.
            var otherKeys = from k2 in Keys where k2.Vk == key!.Vk && k2 != key select k2;

            foreach (var otherKey in otherKeys)
            {
                if (changeType == EventDictionaryChangeType.Remove)
                { otherKey.LayerToWCHAR.Remove(layer); }
                else
                { otherKey.LayerToWCHAR[layer] = (char)newValue!; }
            }
        }

        private void ApplyNlsKeyUpMaskChange(KeyboardKey key, byte newValue)
        {
            // apply the change to all Keys with the same Vk.
            var otherKeys = from k2 in Keys where k2.Vk == key!.Vk && k2 != key select k2;

            foreach (var otherKey in otherKeys)
            { otherKey.NlsKeyUpMask = newValue; }
        }

        private void ApplyVkChange(KeyboardKey key, VK? newValue)
        {
            // apply the vk change to all Keys with the same scancode. Here we assume, that vk conflicts already are taken care of.
            var otherKeys = from k2 in Keys where k2.Scancode == key!.Scancode && k2.E0 == key!.E0 && k2 != key select k2;

            foreach (var otherKey in otherKeys)
            { otherKey.Vk = newValue; }
        }

        private void HandleVkConflict(KeyboardKey key, out bool conflictExisted)
        {
            conflictExisted = true;

            if (key == null)
            { return; }
            if (key.Vk == null)
            { return; }

            conflictExisted = false;
            // if two or more keys have that vk display dialoge to merge the properties that are dependent on Vk
            // the propertie dependend on Vk are the WCHAR and NLS-values

            var otherKeys = from k2 in Keys where k2.Vk == key!.Vk && k2 != key select k2;

            if (!otherKeys.Any())
            { return; }
            
            conflictExisted = true;
            var kOther = otherKeys.First();

            string msg = "There already is atleast one other Key with this Virtual-Keycode(0x"
                + ((int)kOther.Vk!).ToString("X2") + " = " + VKFunc.VKToString(kOther.Vk)
                + ")\r\n"
                + "The Keyname(s) is/are:";

            foreach (var k3 in otherKeys)
            { msg = msg + "\"" + k3.KeyName + "\", "; }

            msg = msg.TrimEnd([',', ' ']);
            msg = msg + ".\r\n\r\n"

                + "One VK can always only be associated with one set of Wchar-/Nls-layers and always have the same VK handling flags and lock behaviour.\r\n\r\n"

                + "Choose Yes to keep the VK on this key and assign the layers & flags to this key that the other keys with the same VK have.\r\n"
                + "Choose No to keep the VK on this key and assign the layers & flags of this key to the other keys with the same VK.\r\n"
                + "Choose Cancel to undo the changes.";

            var msgBoxResult = MessageBox.Show(msg, "Virtual-Keycode conflict", MessageBoxButtons.YesNoCancel);

            KeyboardKey cpyKey;
            switch (msgBoxResult)
            {
                case DialogResult.Yes:
                    cpyKey = new KeyboardKey().CopyProperties(kOther);
                    cpyKey.KeyName = key.KeyName;
                    cpyKey.Scancode = key.Scancode;
                    key.CopyProperties(cpyKey);
                    keysPreviousState[key].CopyProperties(key);
                    break;
                case DialogResult.No:
                    cpyKey = new KeyboardKey().CopyProperties(key);
                    foreach (var k3 in otherKeys)
                    {
                        cpyKey.KeyName = k3.KeyName;
                        cpyKey.Scancode = k3.Scancode;
                        k3.CopyProperties(cpyKey);
                        keysPreviousState[k3].CopyProperties(k3);
                    }
                    break;
                case DialogResult.None:
                case DialogResult.Cancel:
                default:
                    key.CopyProperties(keysPreviousState[key]);
                    break;
            }
        }

        private void HandleScancodeConflict(KeyboardKey key)
        {
            if (key == null)
            { return; }
            if (key.Scancode == null)
            { return; }

            // if two or more keys have that scancode and e0/e1 display a dialoge to merge the properties that are dependent on Scancode
            // the properties that dependend on Scancode are the VirtualKeycode, VKHandlingMask, Lockmask and the properties that depend on VK

            var otherKeys = from k2 in Keys where k2.Scancode == key!.Scancode && k2.E0 == key.E0 && k2 != key select k2;

            if (!otherKeys.Any())
            { return; }

            var kOther = otherKeys.First();

            string msg = "There already is atleast one other Key with this scancode("
                + kOther.Scancode.ToString() + (kOther.E0 ? " & E0" : "")
                + ")\r\n"
                + "The VK mapped to that other scancode is: " + kOther.Vk?.ToString() + "\r\n"
                + "and Key-Name(s) is/are: ";

            foreach (var k3 in otherKeys)
            { msg = msg + "\"" + k3.KeyName + "\", "; }

            msg = msg.TrimEnd([',', ' ']);
            msg = msg + ".\r\n\r\n"

                + "One Scancode can always only be associated with one VK and always have the same VK handling flags and lock behaviour.\r\n"
                + "The layers (Wchar / Nls) are also dependent on the VK and will be copied along with the VK.\r\n\r\n"

                + "Choose Yes to keep the scancode on this key and assign the VK, etc. to this key that the other keys with the same scancode " + (kOther.E0 ? " & E0" : "") + "have.\r\n"
                + "Choose No to keep the scancode on this key and assign the VK, etc. of this key to the other keys with the same scancode \" + (kOther.E0 ? \" & E0\" : \"\") + \".\\r\\n"
                + "Choose Cancel to undo the changes.";

            var msgBoxResult = MessageBox.Show(msg, "Scancode conflict", MessageBoxButtons.YesNoCancel);

            KeyboardKey cpyKey;
            switch (msgBoxResult)
            {
                case DialogResult.Yes:
                    cpyKey = new KeyboardKey().CopyProperties(kOther);
                    cpyKey.KeyName = key.KeyName;
                    key.CopyProperties(cpyKey);
                    keysPreviousState[key].CopyProperties(key);
                    break;
                case DialogResult.No:
                    cpyKey = new KeyboardKey().CopyProperties(key);
                    foreach (var k3 in otherKeys)
                    {
                        cpyKey.KeyName = k3.KeyName;
                        k3.CopyProperties(cpyKey);
                        keysPreviousState[k3].CopyProperties(k3);
                    }
                    break;
                case DialogResult.None:
                case DialogResult.Cancel:
                default:
                    key.CopyProperties(keysPreviousState[key]);
                    break;
            }
        }
        #endregion
    }
}
