namespace Nox.Reference.Data.World.Exceptions;

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
}