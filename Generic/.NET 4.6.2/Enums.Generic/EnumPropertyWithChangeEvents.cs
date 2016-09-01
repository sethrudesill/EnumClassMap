using System;
using Enums.Generic.EventArgs;

namespace Enums.Generic
{
    /// <summary>
    ///     Enables a System.Enum to behave as a property with changing and changed events.
    /// </summary>
    /// <typeparam name="TEnum">Must be <c>System.Enum</c> type.</typeparam>
    public class EnumPropertyWithChangeEvents<TEnum> : EnumProperty<TEnum> where TEnum : struct
    {
        /// <summary>
        ///     Allows cancellation when the <c>Value</c> property is changing.
        /// </summary>
        public event EventHandler<EnumChangingArgs<TEnum>> Changing;

        /// <summary>
        ///     Raised when the <c>Value</c> property has changed.
        /// </summary>
        public event EventHandler<EnumChangedArgs<TEnum>> Changed;
        
        /// <summary>
        ///     Overrides the <c>base.Value</c> and raises events when changing and/or changed.
        /// </summary>
        public override TEnum Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                if (base.Value.Equals(value))
                    return;

                var oldValue = base.Value;

                if (Changing != null)
                {
                    var args = new EnumChangingArgs<TEnum>(nameof(Value), oldValue, value);

                    Changing?.Invoke(this, args);

                    if (args.Cancel)
                        return;
                }

                base.Value = value;

                Changed?.Invoke(this, new EnumChangedArgs<TEnum>(nameof(Value), oldValue, value));
            }
        }

        /// <summary>
        ///     Constructor is internal to disallow external instantiation.
        /// </summary>
        /// <param name="defaultEnumValue">Initial value of the <c>Value</c> property.</param>
        internal EnumPropertyWithChangeEvents(TEnum defaultEnumValue) : base(defaultEnumValue)
        {
        }
    }
}