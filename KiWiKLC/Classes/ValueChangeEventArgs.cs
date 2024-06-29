namespace KiWi_Keyboard_Layout_Creator
{
    public class ValueChangedEventArgs<T>(T lastValue, T newValue) : EventArgs
    {
        public readonly T LastValue = lastValue;
        public readonly T NewValue = newValue;
    }
}
