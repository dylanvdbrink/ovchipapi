using System;

namespace OvChipApi.Exceptions
{
    internal class OvOAuthException : Exception
    {
        public OvOAuthException(string message) : base(message)
        {
        }
    }
}
