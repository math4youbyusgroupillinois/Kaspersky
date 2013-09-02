using System;
using Kaspersky.Antivirus.Internal;

namespace Kaspersky.Antivirus
{
    public sealed class KavEngine : IDisposable
    {
        private static bool s_instantiated;
        private static object s_lock = new object();

        public KavEngine()
        {
            lock (s_lock)
            {
                if (s_instantiated)
                    throw new InvalidOperationException("KavEngine only can be instantiated once.");

                NativeMethods.kaveInitializeEx();

                s_instantiated = true;
            }
        }

        private void Dispose(bool disposeManaged)
        {
            if (_disposed)
                return;

            _disposed = true;

            lock (s_lock)
            {
                NativeMethods.kaveUninitialize();

                s_instantiated = false;
            }
        }

        #region IDisposable pattern
        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~KavEngine()
        {
            Dispose(false);
        }
        #endregion
    }
}
