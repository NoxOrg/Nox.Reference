namespace Nox.Reference.Data.World;

public class PhoneCarrierInfo
{
    public string Name { get; set; } = string.Empty;
    public IReadOnlyList<int> PhoneNumbers { get; set; } = new List<int>();
}