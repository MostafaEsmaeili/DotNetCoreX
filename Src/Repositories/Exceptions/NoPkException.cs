using System;

namespace DotNetCoreX.Repositories.Exceptions
{
    public class NoPkException : Exception
    {
        public NoPkException()
        {
        }

        public NoPkException(string message)
            : base(message)
        {
        }

        public NoPkException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}