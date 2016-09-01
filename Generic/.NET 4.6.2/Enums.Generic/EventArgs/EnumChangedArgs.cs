using System.ComponentModel;

namespace Enums.Generic.EventArgs
{
    /// <summary>
    ///     Provides a reference for comparing the old value versus the new value after the property has changed.
    /// </summary>
    /// <typeparam name="TEnum">Must be <c>System.Enum</c> type.</typeparam>
    public class EnumChangedArgs<TEnum> : PropertyChangedEventArgs where TEnum : struct
    {
        public TEnum OldValue { get; }

        public TEnum NewValue { get; }

        public EnumChangedArgs(string propertyName, TEnum oldValue, TEnum newValue) : base(propertyName)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}