using System;
using System.Runtime;
using System.Runtime.Serialization;

namespace TicketPunch.Core
{
    [Serializable]
    public class InvalidLicenseException : Exception
    {
        public InvalidLicenseException() { }
        public InvalidLicenseException(string message) : base(message) { }
        public InvalidLicenseException(string message, Exception inner) : base(message, inner) { }
        protected InvalidLicenseException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}