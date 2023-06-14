using System.Runtime.Serialization;

namespace Nox.Reference.Data.World.Exceptions
{

    [Serializable]
    public class HolidayCountryNotFoundException : Exception
    {
        public HolidayCountryNotFoundException() : base("Holiday country not found.")
        {
        }

        public HolidayCountryNotFoundException(string message) : base(message)
        {
        }

        public HolidayCountryNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        // Without this constructor, deserialization would fail
        protected HolidayCountryNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}