using System.Runtime.Serialization;

namespace Nox.Reference.Data.World.Exceptions
{

    [Serializable]
    public class HolidayRegionNotFoundException : Exception
    {
        public HolidayRegionNotFoundException() : base("Holiday region not found.")
        {
        }

        public HolidayRegionNotFoundException(string message) : base(message)
        {
        }

        public HolidayRegionNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        // Without this constructor, deserialization would fail
        protected HolidayRegionNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}