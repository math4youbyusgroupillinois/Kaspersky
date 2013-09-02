using System;
using System.Runtime.Serialization;

namespace Kaspersky.Antivirus
{
    [Serializable]
    public class KavException : Exception
    {
        public KavException() { }
        public KavException(string message) : base(message) { }
        public KavException(string message, Exception inner) : base(message, inner) { }
        protected KavException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        internal int ErrorCode { get { return HResult; }}
    }

    [Serializable]
    public class ProductNotRegisteredException : KavException
    {
        internal const int HRESULT = unchecked((int)(0x80040000 | 0x25C));

        public ProductNotRegisteredException() : this(null, null) { }
        public ProductNotRegisteredException(string message) : this(message, null) { }
        public ProductNotRegisteredException(string message, Exception inner) : base(message, inner) { HResult = HRESULT; }
        protected ProductNotRegisteredException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
