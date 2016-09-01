using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Enums.Generic
{
    public abstract class EnumContainer
    {
        public abstract bool IsEnum { get; }

        /// <summary>
        ///     Attempts to instantiate an instance of the EnumContainer using the supplied <c>TEnum</c>.
        /// </summary>
        /// <typeparam name="TEnum">Must be <c>System.Enum</c> type.</typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Signature definition of <c>TEnumType</c> must be <c>System.Enum</c> type.</exception>
        public static EnumContainer<TEnum> Create<TEnum>() where TEnum : struct
        {
            var container = new EnumContainer<TEnum>();

            if (!container.IsEnum)
                throw new ArgumentException($"Invalid argument. {nameof(TEnum)} must be a System.Enum type.");

            return container;
        }



        /// <summary>
        ///     Creates a property for the specified System.Enum value.
        /// </summary>
        /// <param name="defaultEnumValue">The default System.Enum value to wrap in a property.</param>
        /// <returns>An EnumProperty set with the specified value.</returns>
        public static EnumProperty<TEnum> CreateProperty<TEnum>(TEnum defaultEnumValue) where TEnum : struct => new EnumProperty<TEnum>(defaultEnumValue);



        /// <summary>
        ///     Creates a property for the specified System.Enum value which can be observed for changes.
        /// </summary>
        /// <param name="defaultEnumValue">The default System.Enum value to wrap in a property.</param>
        /// <returns>An EnumProperty set with the specified value.</returns>
        public static EnumPropertyWithChangeEvents<TEnum> CreatePropertyWithChangeEvents<TEnum>(TEnum defaultEnumValue) where TEnum : struct => new EnumPropertyWithChangeEvents<TEnum>(defaultEnumValue);
    }

    /// <summary>
    ///     
    /// </summary>
    /// <typeparam name="TEnum">Must be <c>System.Enum</c> type.</typeparam>
    public sealed class EnumContainer<TEnum> : EnumContainer, IEnumerable<TEnum> where TEnum : struct
    {
        public override bool IsEnum { get; } = default(TEnum).GetType().IsEnum;
        
        private readonly TEnum[] _values = Array.Empty<TEnum>();

        public IEnumerable<TEnum> Values => _values.AsEnumerable();

        /// <summary>
        ///     Constructor is internal to disallow external instantiation.
        /// </summary>
        internal EnumContainer()
        {
            if (!IsEnum)
                return;

            _values = Enum.GetNames(typeof(TEnum)).Select(e => (TEnum)Enum.Parse(typeof(TEnum), e.ToString())).ToArray();
        }

        public IEnumerator<TEnum> GetEnumerator()
        {
            return Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
