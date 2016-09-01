using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enums.Generic.Tests
{
    [TestClass]
    public class EnumContainerTests
    {
        private enum TestEnum { Me, Myself, AndI }

        private static int TOTAL_ENUM_NAMES = Enum.GetNames(typeof(TestEnum)).Length;

        [TestMethod]
        public void Enum_Container_Contains_Exact_Number_Of_Items_As_The_Enum_Declaration()
        {
            var container = EnumContainer.Create<TestEnum>();

            Assert.AreEqual(container.Count(), TOTAL_ENUM_NAMES);
        }

        [TestMethod]
        public void Enum_Container_Throws_Argument_Exception_With_Invalid_Type()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                var container = EnumContainer.Create<int>();
            });
        }
    }
}