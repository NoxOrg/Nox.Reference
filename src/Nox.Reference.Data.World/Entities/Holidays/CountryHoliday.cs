namespace Nox.Reference;

public class CountryHoliday : NoxReferenceEntityBase,
    IDtoConvertibleEntity<CountryHolidayInfo>
{
    public int Year { get; private set; }
    public virtual Country Country { get; private set; } = new Country();
    public string? CountryName { get; private set; } = string.Empty;
    public string? DayOff { get; private set; } = string.Empty;
    public virtual IReadOnlyList<HolidayData> Holidays { get; private set; } = new List<HolidayData>();
    public virtual IReadOnlyList<StateHoliday> States { get; private set; } = new List<StateHoliday>();

    public CountryHolidayInfo ToDto()
    {
#pragma warning disable CS0618 // Type or member is obsolete
        return World.Mapper.Map<CountryHolidayInfo>(this);
#pragma warning restore CS0618 // Type or member is obsolete
    }
}