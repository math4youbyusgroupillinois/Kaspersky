using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass

namespace Kaspersky.Antivirus.Internal
{
    internal sealed class NativeMethods
    {
        private static class ExternMethods
        {
            [DllImport("Kave8.dll", EntryPoint = "kavef00", CharSet = CharSet.Unicode, PreserveSig = false)]
            public static extern void kaveInitializeEx();

            [DllImport("Kave8.dll", EntryPoint = "kavef00", CharSet = CharSet.Unicode, PreserveSig = false)]
            public static extern void kaveUninitialize();
        }

        public static void kaveInitializeEx()
        {
            ExternMethods.kaveInitializeEx();
        }

        public static void kaveUninitialize()
        {
            ExternMethods.kaveUninitialize();
        }
    }
}
