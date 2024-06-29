using System;
using System.Windows.Forms;

namespace KiWi_Keyboard_Layout_Creator
{
    /// <summary>
    /// the types of operation that can cause a change to EventDictionary<T, U>
    /// </summary>
    public enum EventDictionaryChangeType
    {
        // defined outside of the EventDictionary<T, U>, because if defined inside, referencing this class is messy in other contexts
        Remove,
        Insert,
        Modify
    }

    /// <summary>
    /// dictionary that fires events for changes
    /// </summary>
    public class EventDictionary<T, U> : Dictionary<T, U>
            where T : notnull
            where U : notnull, IEquatable<U>
    {
        /// <param name="lastValue">default(U) for insert</param>
        /// <param name="newValue">default(U) for remove</param>
        /// <param name="changeType">what type of operation caused the event</param>
        public class EntryChangedEventArgs(T key, U? lastValue, U? newValue, EventDictionaryChangeType changeType) : EventArgs
        {
            public readonly T Key = key;
            public readonly U? LastValue = lastValue;
            public readonly U? NewValue = newValue;
            public readonly EventDictionaryChangeType ChangeType = changeType;
        }
        public event EventHandler<EntryChangedEventArgs>? EntryChanged;

        public new U this[T key]
        {
            get { return base[key]; }

            set
            {
                if (!base.TryGetValue(key, out U? oldvalue))
                { 
                    base[key] = value;
                    var eArgs = new EntryChangedEventArgs(key, oldvalue, value, EventDictionaryChangeType.Insert);
                    EntryChanged?.Invoke(this, eArgs);
                }
                else if (!oldvalue.Equals(value))
                { 
                    base[key] = value;
                    var eArgs = new EntryChangedEventArgs(key, oldvalue, value, EventDictionaryChangeType.Modify);
                    EntryChanged?.Invoke(this, eArgs);
                }
            }
        }

        public new bool Remove(T key)
        {
            base.TryGetValue(key, out U? oldvalue);
            
            if (base.Remove(key))
            {
                EntryChanged?.Invoke(this, new EntryChangedEventArgs(key, oldvalue, default, EventDictionaryChangeType.Remove));
                return true;
            }
            return false;
        }

        /// <summary>
        /// doesnt fire change events if fireChangeEvents is not set
        /// </summary>
        public void AddRange(Dictionary<T, U> values, bool overrideIfKeyExists = true, bool fireChangeEvents = false)
        {
            foreach (var value in values)
            {
                if (base.ContainsKey(value.Key))
                {
                    if (overrideIfKeyExists)
                    {
                        var originalValue = base[value.Key];
                        base[value.Key] = value.Value;

                        if (fireChangeEvents)
                        { EntryChanged?.Invoke(this, new EntryChangedEventArgs(value.Key, originalValue, value.Value, EventDictionaryChangeType.Modify)); }
                    }
                }
                else 
                {
                    base[value.Key] = value.Value;

                    if (fireChangeEvents)
                    { EntryChanged?.Invoke(this, new EntryChangedEventArgs(value.Key, default, value.Value, EventDictionaryChangeType.Insert)); }
                }
            }
        }

        public new bool TryAdd(T key, U value)
        {
            if (base.TryAdd(key, value))
            { 
                EntryChanged?.Invoke(this, new EntryChangedEventArgs(key, default, value, EventDictionaryChangeType.Insert));
                return true;
            }

            return false;
        }
    }
}
