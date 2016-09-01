using System.Net.Configuration;

namespace Enums.Generic
{
    /// <summary>
    ///     Enables a System.Enum to behave as a property.
    /// </summary>
    /// <typeparam name="TEnum">Must be <c>System.Enum</c> type.</typeparam>
    public class EnumProperty<TEnum> where TEnum : struct
    {
        private readonly TEnum _default;
        private TEnum _value;

        public virtual TEnum Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (_value.Equals(value))
                    return;

                _value = value;
            }
        }

        /// <summary>
        ///     Constructor is internal to disallow external instantiation.
        /// </summary>
        /// <param name="defaultEnumValue">Initial value of the <c>Value</c> property.</param>
        internal EnumProperty(TEnum defaultEnumValue)
        {
            _default = defaultEnumValue;
            _value = _default;
        }

        /// <summary>
        ///     Changes the <c>Value</c> property back to the default value supplied in the constructor.
        /// </summary>
        public void Reset()
        {
            Value = _default;
        }
    }
}