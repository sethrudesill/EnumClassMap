using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enums.Generic.Tests
{
    [TestClass]
    public class EnumPropertyTests
    {
        private enum TestEnum { Me, Myself, AndI }

        [TestMethod]
        public void Enum_Property_Can_Be_Reset()
        {
            var defaultEnumValue = TestEnum.Myself;
            
            var prop = EnumContainer.CreateProperty(defaultEnumValue);

            Assert.AreEqual(defaultEnumValue, prop.Value);

            prop.Value = TestEnum.AndI;

            Assert.AreEqual(TestEnum.AndI, prop.Value);

            prop.Reset();

            Assert.AreEqual(defaultEnumValue, prop.Value);
        }

        [TestMethod]
        public void Enum_Property_Notifies_Changing()
        {
            bool notifiedChanging = false;
            var prop = EnumContainer.CreatePropertyWithChangeEvents(TestEnum.Me);
            prop.Changing += (sender, args) =>
            {
                notifiedChanging = true;
            };

            Task.Run(() =>
            {
                prop.Value = TestEnum.Myself;
            }).Wait(500);

            Assert.IsTrue(notifiedChanging);
        }

        [TestMethod]
        public void Enum_Property_Notifies_Changed()
        {
            bool notifiedChanged = false;
            
            var prop = EnumContainer.CreatePropertyWithChangeEvents(TestEnum.Me);

            prop.Changed += (sender, args) =>
            {
                notifiedChanged = true;
            };

            Task.Run(() =>
            {
                prop.Value = TestEnum.Myself;
            }).Wait(500);

            Assert.IsTrue(notifiedChanged);
        }

        [TestMethod]
        public void Enum_Property_Notifies_Changing_And_Changed()
        {
            bool notifiedChanging = false;
            bool notifiedChanged = false;
            var prop = EnumContainer.CreatePropertyWithChangeEvents(TestEnum.Me);
            prop.Changing += (sender, args) =>
            {
                notifiedChanging = true;
            };
            prop.Changed += (sender, args) =>
            {
                notifiedChanged = true;
            };

            Task.Run(() =>
            {
                prop.Value = TestEnum.Myself;
            }).Wait(500);

            Assert.IsTrue(notifiedChanging);

            Assert.IsTrue(notifiedChanged);
        }

        [TestMethod]
        public void Enum_Property_Notifies_Changing_And_Allows_Cancellation()
        {
            bool notifiedChanging = false;
            bool notifiedChanged = false;
            var prop = EnumContainer.CreatePropertyWithChangeEvents(TestEnum.Me);
            prop.Changing += (sender, args) =>
            {
                notifiedChanging = true;
                args.Cancel = true;
            };
            prop.Changed += (sender, args) =>
            {
                notifiedChanged = true;
            };

            Task.Run(() =>
            {
                prop.Value = TestEnum.Myself;
            }).Wait(500);

            Assert.IsTrue(notifiedChanging);

            Assert.IsFalse(notifiedChanged);
        }
    }
}