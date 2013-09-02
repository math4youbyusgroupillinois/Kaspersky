using System;
using System.Threading;
using Kaspersky.Antivirus;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.QualityTools.Testing.Fakes.Shims;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable once CheckNamespace
namespace Test
{
    [TestClass]
    public class AntivirusTests
    {
        [TestMethod]
        public void KavException_HResultInitialized()
        {
            var ex1 = new ProductNotRegisteredException();
            Assert.AreEqual(ex1.ErrorCode, ProductNotRegisteredException.HRESULT);

            var ex2 = new ProductNotRegisteredException("Message message message");
            Assert.AreEqual(ex1.ErrorCode, ProductNotRegisteredException.HRESULT);

            var inner = new OutOfMemoryException();
            var ex3 = new ProductNotRegisteredException("Message message message", inner);
            Assert.AreEqual(ex1.ErrorCode, ProductNotRegisteredException.HRESULT);
        }

        [TestMethod]
        public void KavEngine_Kave8InitializeExMethodCalled()
        {
            var initializeCalled = false;

            using (ShimsContext.Create())
            {
                Kaspersky.Antivirus.Internal.Fakes.ShimNativeMethods.kaveInitializeEx
                    = () => initializeCalled = true;
                Kaspersky.Antivirus.Internal.Fakes.ShimNativeMethods.kaveUninitialize
                    = () => { };

                using (var kav = new KavEngine()) { }
            }

            Assert.IsTrue(initializeCalled);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void KavEngine_SecondInstantiationFails()
        {
            using (ShimsContext.Create())
            {
                Kaspersky.Antivirus.Internal.Fakes.ShimNativeMethods
                    .Behavior = ShimBehaviors.DefaultValue;

                using (var engine1 = new KavEngine())
                    using (var engine2 = new KavEngine()) { }
            }
        }

        [TestMethod]
        public void KavEngine_OnlyInstantiationSucceeds()
        {
            using (ShimsContext.Create())
            {
                Kaspersky.Antivirus.Internal.Fakes.ShimNativeMethods
                    .Behavior = ShimBehaviors.DefaultValue;

                using (var engine1 = new KavEngine()) { }
                using (var engine2 = new KavEngine()) { }
            }
        }
    }
}
