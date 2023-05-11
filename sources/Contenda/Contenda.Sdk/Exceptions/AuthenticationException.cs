using System;

namespace Contenda.Sdk.Exceptions
{
    /// <summary>
    /// Authentication Exception
    /// </summary>
    public sealed class AuthenticationException : Exception
    {
        /// <summary>
        /// Exception reason
        /// </summary>
        public string Reason { get; }

        internal AuthenticationException(string reason)
        {
            Reason = reason;
        }
    }
}
