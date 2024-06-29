using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace KiWi_Keyboard_Layout_Creator
{

    /// <summary>
    /// holds the data for a key and reports changes with events.
    /// </summary>
    internal class KeyboardKey : Button
    {
        #region fields
        //margins should to be multiple of 4
        private const int marginLeft = 0;
        private const int marginTop = 0;
        private const int marginRight = 4;
        private const int marginBottom = 4;
        public const int ButtonSizeUnit = 56;
        #endregion

        #region events
        internal event EventHandler<ValueChangedEventArgs<bool>>? E0Change;
        internal event EventHandler<ValueChangedEventArgs<int?>>? ScancodeChange;
        internal event EventHandler<ValueChangedEventArgs<VK?>>? VkChange;
        internal event EventHandler<ValueChangedEventArgs<string>>? KeyNameChange;
        internal event EventHandler<ValueChangedEventArgs<int>>? VKHandlingMaskChange;
        internal event EventHandler<ValueChangedEventArgs<int>>? LockmasksChange;
        internal event EventHandler<ValueChangedEventArgs<byte>>? NlsKeyUpMaskChange;
        internal event EventHandler<EventDictionary<int, char>.EntryChangedEventArgs>? LayerToWCHAREntryChanged;
        internal event EventHandler<EventDictionary<int, NLSPair>.EntryChangedEventArgs>? LayerToNlsEntryChanged;
        internal event EventHandler<EventDictionary<int, NLSPair>.EntryChangedEventArgs>? LayerToNlsKeyUpEntryChanged;
        #endregion
        
        #region properties
        public new static Padding Margin 
        {
            get
            { return new Padding(marginLeft, marginTop, marginRight, marginBottom); }
        }

        /// <param name="width">see KeycapSizesUnits for possible values</param>
        /// <param name="height">see KeycapSizesUnits for possible values</param>
        public static Size SizeFromKeycapUnits(double width, double height)
        {
            // + (width-1)*marginRight adds the space, that is otherwise lost, when one row has less keys than another
            return new Size(
                Convert.ToInt32(ButtonSizeUnit * width + (width-1)*marginRight)
                , Convert.ToInt32(ButtonSizeUnit * height + (height - 1) * marginBottom)
                );
        }

        private int? __scancode = null;
        internal int? Scancode
        {
            get
            { return __scancode; }
            set
            {
                var eArgs = new ValueChangedEventArgs<int?>(__scancode, value);
                __scancode = value;
                if (eArgs.LastValue != eArgs.NewValue)
                { ScancodeChange?.Invoke(this, eArgs); }
            }
        }
        private bool __E0;
        internal bool E0
        {
            get
            { return __E0; }
            set
            {
                var eArgs = new ValueChangedEventArgs<bool>(__E0, value);
                __E0 = value;
                if (eArgs.LastValue != eArgs.NewValue)
                { E0Change?.Invoke(this, eArgs); }
            }
        }

        private VK? __vk = null;
        internal VK? Vk
        {
            get
            { return __vk; }
            set
            {
                var eArgs = new ValueChangedEventArgs<VK?>(__vk, value);
                __vk = value;
                if (eArgs.LastValue != eArgs.NewValue)
                { VkChange?.Invoke(this, eArgs); }
            }
        }


        internal string __keyName = "";
        internal string KeyName
        {
            get
            { return __keyName; }
            set
            {
                var eArgs = new ValueChangedEventArgs<string>(__keyName, value);
                __keyName = value;
                if (eArgs.LastValue != eArgs.NewValue)
                { KeyNameChange?.Invoke(this, eArgs); }
            }
        }

        internal int __vKHandlingMask = 0x00;
        internal int VKHandlingMask
        {
            get
            { return __vKHandlingMask; }
            set
            {
                var eArgs = new ValueChangedEventArgs<int>(__vKHandlingMask, value);
                __vKHandlingMask = value;
                if (eArgs.LastValue != eArgs.NewValue)
                { VKHandlingMaskChange?.Invoke(this, eArgs); }
            }
        }


        internal int __lockmask = 0x00;
        internal int Lockmask
        {
            get
            { return __lockmask; }
            set
            {
                var eArgt = new ValueChangedEventArgs<int>(__lockmask, value);
                __lockmask = value;
                if (eArgt.LastValue != eArgt.NewValue)
                { LockmasksChange?.Invoke(this, eArgt); }
            }
        }

        private EventDictionary<int, char> __layerToWCHAR = [];
        internal EventDictionary<int, char> LayerToWCHAR
        {
            get
            { return __layerToWCHAR; }
            private set
            {
                __layerToWCHAR.EntryChanged -= LayerToWCHAR_EntryChanged;
                __layerToWCHAR = value;
                __layerToWCHAR.EntryChanged += LayerToWCHAR_EntryChanged;

            }
        }

        private EventDictionary<int, NLSPair> __layerToNls = [];
        internal EventDictionary<int, NLSPair> LayerToNls 
        {
            get
            { return __layerToNls; }
            private set
            {
                __layerToNls.EntryChanged -= LayerToNls_EntryChanged;
                __layerToNls = value;
                __layerToNls.EntryChanged += LayerToNls_EntryChanged;
            }
        }

        private EventDictionary<int, NLSPair> __layerToNlsKeyUp = [];
        internal EventDictionary<int, NLSPair> LayerToNlsKeyUp
        {
            get
            { return __layerToNlsKeyUp; }
            private set
            {
                __layerToNlsKeyUp.EntryChanged -= LayerToNlsKeyUp_EntryChanged;
                __layerToNlsKeyUp = value;
                __layerToNlsKeyUp.EntryChanged += LayerToNlsKeyUp_EntryChanged;
            }
        }

        private byte __nlsKeyUpMask = 0x00;
        internal byte NlsKeyUpMask
        {
            get
            { return __nlsKeyUpMask; }
            set
            {
                var eArgt = new ValueChangedEventArgs<byte>(__nlsKeyUpMask, value);
                __nlsKeyUpMask = value;
                if (eArgt.LastValue != eArgt.NewValue)
                { NlsKeyUpMaskChange?.Invoke(this, eArgt); }
            }
        }

        /// <param name="layer">combination of Layers base/shift/ctrl/alt</param>
        /// <param name="bitValue">false if the bit should be 0, true for 1</param>
        internal void SetNlsKeyUpMaskBit(int layer, bool bitValue)
        {
            switch (layer)
            {
                case 0x00:
                    NlsKeyUpMask = (byte)((int)NlsKeyUpMask & 0xFE | (bitValue ? 0x01 : 0x00));
                    break;
                case (int)KbdLayers.KBDSHIFT:
                    NlsKeyUpMask = (byte)((int)NlsKeyUpMask & 0xFD | (bitValue ? 0x02 : 0x00));
                    break;
                case (int)KbdLayers.KBDCTRL:
                    NlsKeyUpMask = (byte)((int)NlsKeyUpMask & 0xFB | (bitValue ? 0x04 : 0x00));
                    break;
                case (int)KbdLayers.KBDCTRL | (int)KbdLayers.KBDSHIFT:
                    NlsKeyUpMask = (byte)((int)NlsKeyUpMask & 0xF7 | (bitValue ? 0x08 : 0x00));
                    break;
                case (int)KbdLayers.KBDALT:
                    NlsKeyUpMask = (byte)((int)NlsKeyUpMask & 0xEF | (bitValue ? 0x10 : 0x00));
                    break;
                case (int)KbdLayers.KBDALT | (int)KbdLayers.KBDSHIFT:
                    NlsKeyUpMask = (byte)((int)NlsKeyUpMask & 0xDF | (bitValue ? 0x20 : 0x00));
                    break;
                case (int)KbdLayers.KBDALT | (int)KbdLayers.KBDCTRL:
                    NlsKeyUpMask = (byte)((int)NlsKeyUpMask & 0xBF | (bitValue ? 0x40 : 0x00));
                    break;
                case (int)KbdLayers.KBDALT | (int)KbdLayers.KBDSHIFT | (int)KbdLayers.KBDCTRL:
                    NlsKeyUpMask = (byte)((int)NlsKeyUpMask & 0x7F | (bitValue ? 0x80 : 0x00));
                    break;
                default:
                    break;
            }
        }

        /// <param name="layer">combination of Layers base/shift/ctrl/alt</param>
        /// <returns>the value of the bit of the layer</returns>
        internal bool GetNlsKeyUpMaskBit(int layer)
        {

            switch (layer)
            {
                case 0x00:
                    return ((int)NlsKeyUpMask & 0x01) == 0x01;
                case (int)KbdLayers.KBDSHIFT:
                    return ((int)NlsKeyUpMask & 0x02) == 0x02;
                case (int)KbdLayers.KBDCTRL:
                    return ((int)NlsKeyUpMask & 0x04) == 0x04;
                case (int)KbdLayers.KBDCTRL | (int)KbdLayers.KBDSHIFT:
                    return ((int)NlsKeyUpMask & 0x08) == 0x08;
                case (int)KbdLayers.KBDALT:
                    return ((int)NlsKeyUpMask & 0x10) == 0x10;
                case (int)KbdLayers.KBDALT | (int)KbdLayers.KBDSHIFT:
                    return ((int)NlsKeyUpMask & 0x20) == 0x20;
                case (int)KbdLayers.KBDALT | (int)KbdLayers.KBDCTRL:
                    return ((int)NlsKeyUpMask & 0x40) == 0x40;
                case (int)KbdLayers.KBDALT | (int)KbdLayers.KBDSHIFT | (int)KbdLayers.KBDCTRL:
                    return ((int)NlsKeyUpMask & 0x80) == 0x80;
                default:
                    break;
            }
            return false;
        }

        internal bool IsNlsKey()
        {
            foreach (var val in LayerToNls.Values)
            {
                if (val.NlsType != NlsType.NULL && val.NlsType != NlsType.SEND_BASE_VK)
                { return true; }
            }
            foreach (var val in LayerToNlsKeyUp.Values)
            {
                if (val.NlsType != NlsType.NULL && val.NlsType != NlsType.SEND_BASE_VK)
                { return true; }
            }

            return false;
        }

        /// <summary>
        /// Copies all the properties and after all the copying, fires the change events of the properties that changed.
        /// Does not copy the position / size.
        /// </summary>
        /// <returns>returns the key itself</returns>
        internal KeyboardKey CopyProperties(KeyboardKey? key)
        {
            if (key == null)
            { key = new KeyboardKey(); }

            var eArgsScancode = (false, new ValueChangedEventArgs<int?>(__scancode, key.Scancode));
            var eArgsE0 = (false, new ValueChangedEventArgs<bool>(__E0, key.E0));
            var eArgsVK = (false, new ValueChangedEventArgs<VK?>(__vk, key.Vk));
            var eArgsKeyname = (false, new ValueChangedEventArgs<string>(__keyName, key.KeyName));
            var eArgsHandlingMask = (false, new ValueChangedEventArgs<int>(__vKHandlingMask, key.VKHandlingMask));
            var eArgsLockmask = (false, new ValueChangedEventArgs<int>(__lockmask, key.Lockmask));
            var eArgsNlsKeyUpMask = (false, new ValueChangedEventArgs<byte>(__nlsKeyUpMask, key.NlsKeyUpMask));
            
            if (key.Scancode != __scancode)
            {
                eArgsScancode.Item1 = true;
                __scancode = key.Scancode;
            }
            if (key.E0 != __E0)
            {
                eArgsE0.Item1 = true;
                __E0 = key.E0;
            }
            if (key.Vk != __vk)
            {
                eArgsVK.Item1 = true;
                __vk = key.Vk;
            }
            if (key.KeyName != __keyName)
            {
                eArgsKeyname.Item1 = true;
                __keyName = key.KeyName;
            }
            if (key.VKHandlingMask != __vKHandlingMask)
            {
                eArgsHandlingMask.Item1 = true;
                __vKHandlingMask = key.VKHandlingMask;
            }
            if (key.Lockmask != __lockmask)
            {
                eArgsLockmask.Item1 = true;
                __lockmask = key.Lockmask;
            }
            if (key.NlsKeyUpMask != __nlsKeyUpMask)
            {
                eArgsNlsKeyUpMask.Item1 = true;
                __nlsKeyUpMask = key.NlsKeyUpMask;
            }

            // nls / wchar event dictionarys
            // find differences, remember those in a eventlist with the change event handlers, then set the thisKey.dic = keyToCopy.dic 

            var wCharLayerMaybeChanged = this.LayerToWCHAR.Keys.Intersect(key.LayerToWCHAR.Keys);
            var wCharLayerRemoved = this.LayerToWCHAR.Keys.Except(wCharLayerMaybeChanged);
            var wCharLayerAdded = key.LayerToWCHAR.Keys.Except(wCharLayerMaybeChanged);
            
            var wCharLayerChanged = new List<EventDictionary<int, char>.EntryChangedEventArgs>();

            foreach (var layer in wCharLayerRemoved)
            { wCharLayerChanged.Add(new EventDictionary<int, char>.EntryChangedEventArgs(layer, this.LayerToWCHAR[layer], default, EventDictionaryChangeType.Remove)); }
            foreach (var layer in wCharLayerAdded)
            { wCharLayerChanged.Add(new EventDictionary<int, char>.EntryChangedEventArgs(layer, default, key.LayerToWCHAR[layer], EventDictionaryChangeType.Insert)); }
            foreach (var layer in wCharLayerMaybeChanged)
            {
                if (!this.LayerToWCHAR[layer].Equals(key.LayerToWCHAR[layer]))
                { wCharLayerChanged.Add(new EventDictionary<int, char>.EntryChangedEventArgs(layer, this.LayerToWCHAR[layer], key.LayerToWCHAR[layer], EventDictionaryChangeType.Modify)); }
            }
            this.LayerToWCHAR = key.LayerToWCHAR;


            var nlsLayerMaybeChanged = this.LayerToNls.Keys.Intersect(key.LayerToNls.Keys);
            var nlsLayerRemoved = this.LayerToNls.Keys.Except(nlsLayerMaybeChanged);
            var nlsLayerAdded = key.LayerToNls.Keys.Except(nlsLayerMaybeChanged);

            var nlsLayerChanged = new List<EventDictionary<int, NLSPair>.EntryChangedEventArgs>();

            foreach (var layer in nlsLayerRemoved)
            { nlsLayerChanged.Add(new EventDictionary<int, NLSPair>.EntryChangedEventArgs(layer, this.LayerToNls[layer], default, EventDictionaryChangeType.Remove)); }
            foreach (var layer in nlsLayerAdded)
            { nlsLayerChanged.Add(new EventDictionary<int, NLSPair>.EntryChangedEventArgs(layer, default, key.LayerToNls[layer], EventDictionaryChangeType.Insert)); }
            foreach (var layer in nlsLayerMaybeChanged)
            {
                if (!this.LayerToNls[layer].Equals(key.LayerToNls[layer]))
                { nlsLayerChanged.Add(new EventDictionary<int, NLSPair>.EntryChangedEventArgs(layer, this.LayerToNls[layer], key.LayerToNls[layer], EventDictionaryChangeType.Modify)); }
            }
            this.LayerToNls = key.LayerToNls;

            var nlsKeyUpLayerMaybeChanged = this.LayerToNlsKeyUp.Keys.Intersect(key.LayerToNlsKeyUp.Keys);
            var nlsKeyUpLayerRemoved = this.LayerToNlsKeyUp.Keys.Except(nlsKeyUpLayerMaybeChanged);
            var nlsKeyUpLayerAdded = key.LayerToNlsKeyUp.Keys.Except(nlsKeyUpLayerMaybeChanged);

            var nlsKeyUpLayerChanged = new List<EventDictionary<int, NLSPair>.EntryChangedEventArgs>();

            foreach (var layer in nlsKeyUpLayerRemoved)
            { nlsKeyUpLayerChanged.Add(new EventDictionary<int, NLSPair>.EntryChangedEventArgs(layer, this.LayerToNlsKeyUp[layer], default, EventDictionaryChangeType.Remove)); }
            foreach (var layer in nlsKeyUpLayerAdded)
            { nlsKeyUpLayerChanged.Add(new EventDictionary<int, NLSPair>.EntryChangedEventArgs(layer, default, key.LayerToNlsKeyUp[layer], EventDictionaryChangeType.Insert)); }
            foreach (var layer in nlsKeyUpLayerMaybeChanged)
            {
                if (!this.LayerToNlsKeyUp[layer].Equals(key.LayerToNlsKeyUp[layer]))
                { nlsKeyUpLayerChanged.Add(new EventDictionary<int, NLSPair>.EntryChangedEventArgs(layer, this.LayerToNlsKeyUp[layer], key.LayerToNlsKeyUp[layer], EventDictionaryChangeType.Modify)); }
            }
            this.LayerToNlsKeyUp = key.LayerToNlsKeyUp;

            if (eArgsScancode.Item1)
            { ScancodeChange?.Invoke(this, eArgsScancode.Item2); }
            if (eArgsE0.Item1)
            { E0Change?.Invoke(this, eArgsE0.Item2); }
            if (eArgsVK.Item1)
            { VkChange?.Invoke(this, eArgsVK.Item2); }
            if (eArgsKeyname.Item1)
            { KeyNameChange?.Invoke(this, eArgsKeyname.Item2); }
            if (eArgsHandlingMask.Item1)
            { VKHandlingMaskChange?.Invoke(this, eArgsHandlingMask.Item2); }
            if (eArgsLockmask.Item1)
            { LockmasksChange?.Invoke(this, eArgsLockmask.Item2); }
            if (eArgsNlsKeyUpMask.Item1)
            { NlsKeyUpMaskChange?.Invoke(this, eArgsNlsKeyUpMask.Item2); }
            

            foreach (var changeEventArgs in wCharLayerChanged)
            { LayerToWCHAREntryChanged?.Invoke(this, changeEventArgs); }

            foreach (var changeEventArgs in nlsLayerChanged)
            { LayerToNlsEntryChanged?.Invoke(this, changeEventArgs); }

            foreach (var changeEventArgs in nlsKeyUpLayerChanged)
            { LayerToNlsKeyUpEntryChanged?.Invoke(this, changeEventArgs); }

            return this;
        }
        #endregion

        #region constructors
        public KeyboardKey(JsonObject data) : base()
        {
            AllConstructors();

            JsonImport(data);
        }

        /// <param name="size">should be created with SizeFromKeycapUnits</param>
        public KeyboardKey(Size size, Point location) : base()
        {
            AllConstructors();

            base.Size = size;
            base.Location = location;
        }

        public KeyboardKey() : base()
        {
            AllConstructors();
        }

        private void AllConstructors()
        {
            LayerToWCHAR = [];
            LayerToNls = [];
            LayerToNlsKeyUp = [];
            UseMnemonic = false;

            Padding = new(marginLeft, marginTop, marginRight, marginBottom);
        }
        #endregion

        #region eventhandlers
        private void LayerToNls_EntryChanged(object? sender, EventDictionary<int, NLSPair>.EntryChangedEventArgs e)
        {
            if (e.LastValue != e.NewValue)
            { LayerToNlsEntryChanged?.Invoke(this, e); }
        }
        
        private void LayerToNlsKeyUp_EntryChanged(object? sender, EventDictionary<int, NLSPair>.EntryChangedEventArgs e)
        {
            if (e.LastValue != e.NewValue)
            { LayerToNlsKeyUpEntryChanged?.Invoke(this, e); }
        }

        private void LayerToWCHAR_EntryChanged(object? sender, EventDictionary<int, char>.EntryChangedEventArgs e)
        {
            if (e.LastValue != e.NewValue)
            { LayerToWCHAREntryChanged?.Invoke(this, e); }
        }
        #endregion

        #region utility
        internal bool ListCoversLayers(List<int> lst)
        {
            if (LayerToWCHAR.Keys.Count == 0)
            { return false; }

            foreach (int layer in LayerToWCHAR.Keys)
            {
                if (!lst.Contains(layer))
                { return false; }
            }
            return true;
        }
        #endregion

        #region Json Import Export
        const string exKeyLocation = "Location";
        const string exKeySize = "Size";
        const string exKeyScancode = "Scancode";
        const string exKeyExtensionKey = "ExtensionKey";
        const string exKeyVirtualKey = "VirtualKey";
        const string exKeyKeyName = "KeyName";
        const string exKeyLayerLockmask = "LayerLockmask";
        const string exKeyLayers = "Layers";
        const string exKeyLayersKBDNLS = "LayersNLS";
        const string exKeyLayersNLSKeyUpMask = "LayersNLSKeyUpMask";
        const string exKeyLayersNLSKeyUp = "LayersNLSKeyUp";
        const string exKeyVKHandlingMask = "VKHandlingMask";

        internal JsonObject JsonExport()
        {
            var exp = new JsonObject
            {
                { exKeyLocation, JsonSerializer.SerializeToNode(Location) },
                { exKeySize, JsonSerializer.SerializeToNode(Size) },
                { exKeyScancode, JsonSerializer.SerializeToNode(Scancode) },
                { exKeyExtensionKey, JsonSerializer.SerializeToNode(E0) },
                { exKeyVirtualKey, JsonSerializer.SerializeToNode(Vk) },
                { exKeyKeyName, JsonSerializer.SerializeToNode(KeyName) },
                { exKeyVKHandlingMask, JsonSerializer.SerializeToNode(VKHandlingMask) },
                { exKeyLayerLockmask, JsonSerializer.SerializeToNode(Lockmask) },
                { exKeyLayers, JsonSerializer.SerializeToNode(LayerToWCHAR)},
                { exKeyLayersKBDNLS, JsonSerializer.SerializeToNode(LayerToNls)},
                { exKeyLayersNLSKeyUpMask, JsonSerializer.SerializeToNode(NlsKeyUpMask)},
                { exKeyLayersNLSKeyUp, JsonSerializer.SerializeToNode(LayerToNlsKeyUp)},
            };
            return exp;
        }

        /// <exception cref="JsonException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        internal void JsonImport(JsonObject data)
        {
            if (data == null)
            { return; }

            foreach (var val in data)
            {
                switch (val.Key)
                {
                    case exKeyLocation:
                        Location = val.Value.Deserialize<Point>();
                        break;
                    case exKeySize:
                        Size = val.Value.Deserialize<Size>();
                        break;
                    case exKeyScancode:
                        Scancode = val.Value.Deserialize<int?>();
                        break;
                    case exKeyExtensionKey:
                        E0 = val.Value.Deserialize<bool>();
                        break;
                    case exKeyVirtualKey:
                        Vk = val.Value.Deserialize<VK?>();
                        break;
                    case exKeyKeyName:
                        KeyName = val.Value.Deserialize<string>()!;
                        break;
                    case exKeyVKHandlingMask:
                        VKHandlingMask = val.Value.Deserialize<int>();
                        break;
                    case exKeyLayerLockmask:
                        Lockmask = val.Value.Deserialize<int>();
                        break;
                    case exKeyLayers:
                        var WcharLayers = val.Value.Deserialize<Dictionary<int, char>>();
                        if (WcharLayers != null)
                        { LayerToWCHAR.AddRange(WcharLayers); }
                        break;
                    case exKeyLayersKBDNLS:
                        var NlsLayers = val.Value.Deserialize<Dictionary<int, NLSPair>>();
                        if (NlsLayers != null)
                        { LayerToNls.AddRange(NlsLayers); }
                        break;
                    case exKeyLayersNLSKeyUp:
                        var NlsKeyUpLayers = val.Value.Deserialize<Dictionary<int, NLSPair>>();
                        if (NlsKeyUpLayers != null)
                        { LayerToNlsKeyUp.AddRange(NlsKeyUpLayers); }
                        break;
                    case exKeyLayersNLSKeyUpMask:
                        NlsKeyUpMask = val.Value.Deserialize<byte>();
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
    }

    public class NLSPair(NlsType nlsType, VK? nlsVk) : IEquatable<NLSPair?>
    {
        #region properties
        // getter setter instead of readonly, so that json.serialize exports these properties
        public NlsType NlsType { get; private set; } = nlsType;
        public VK? NlsVk { get; private set; } = nlsVk;
        #endregion

        #region utility
        public bool Equals(NLSPair? other)
        {
            if (other == null)
            { return false; }

            return (other.NlsType == this.NlsType && other.NlsVk == this.NlsVk);
        }
        #endregion
    }
}