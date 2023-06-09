﻿namespace Nox.Reference;

public class MacAddress : NoxReferenceEntityBase,
    IKeyedNoxReferenceEntity<string>,
    IDtoConvertibleEntity<MacAddressInfo>
{
    public string Id => MacPrefix;
    public string IEEERegistry { get; private set; } = string.Empty;
    public string MacPrefix { get; private set; } = string.Empty;
    public string OrganizationName { get; private set; } = string.Empty;
    public string OrganizationAddress { get; private set; } = string.Empty;

    public MacAddressInfo ToDto()
    {
        return Machine.Mapper.Map<MacAddressInfo>(this);
    }
}