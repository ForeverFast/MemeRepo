using System.Runtime.Serialization;

namespace Web.Core.Exceptions
{
    internal class DeleteEntityException : Exception
    {
        public DeleteEntityException()
        {
        }

        public DeleteEntityException(string? message) : base(message)
        {
        }

        public DeleteEntityException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DeleteEntityException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
