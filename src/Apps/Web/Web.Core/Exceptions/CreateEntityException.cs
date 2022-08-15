using System.Runtime.Serialization;

namespace Web.Core.Exceptions
{
    internal class CreateEntityException : Exception
    {
        public CreateEntityException()
        {
        }

        public CreateEntityException(string? message) : base(message)
        {
        }

        public CreateEntityException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CreateEntityException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
