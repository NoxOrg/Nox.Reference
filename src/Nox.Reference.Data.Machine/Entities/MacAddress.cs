﻿using Nox.Reference.Data.Common;

namespace Nox.Reference.Data;

public class MacAddress : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string IEEERegistry { get; private set; } = string.Empty;
    public string MacPrefix { get; private set; } = string.Empty;
    public string OrganizationName { get; private set; } = string.Empty;
    public string OrganizationAddress { get; private set; } = string.Empty;
}