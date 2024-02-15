using System.Runtime.Serialization;

namespace RoyalLifeSavings.Integrations.VitalSource
{
    [Serializable]
    internal class VitalSourceException : Exception
    {
        public VitalSourceException()
        {
        }

        public VitalSourceException(string? message) : base(message)
        {
        }

        public VitalSourceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected VitalSourceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
