using System.Runtime.Serialization;

namespace Web.Core.Exceptions
{
    internal class UpdateEntityException : Exception
    {
        public UpdateEntityException()
        {
        }

        public UpdateEntityException(string? message) : base(message)
        {
        }

        public UpdateEntityException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UpdateEntityException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
