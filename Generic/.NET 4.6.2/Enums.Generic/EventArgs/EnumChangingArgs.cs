using System.ComponentModel;

namespace Enums.Generic.EventArgs
{
    /// <summary>
    ///     Allows cancellation of the changing event.
    /// </summary>
    /// <typeparam name="TEnum">Must be <c>System.Enum</c> type.</typeparam>
    public class EnumChangingArgs<TEnum> : PropertyChangingEventArgs where TEnum : struct
    {
        public TEnum OldValue { get; }

        public TEnum NewValue { get; }

        public bool Cancel { get; set; }

        public EnumChangingArgs(string propertyName, TEnum oldValue, TEnum newValue) : base(propertyName)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}