﻿using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class VatNumberDefinition : NoxReferenceEntityBase
{
    public string LocalName { get; private set; } = string.Empty;
    public VerificationApi VerificationApi { get; private set; }
    public string CountryCode { get; private set; } = string.Empty;
    public virtual Country Country { get; internal set; } = null!;
    public virtual IReadOnlyList<VatNumberValidationRule> ValidationRules { get; private set; } = new List<VatNumberValidationRule>();
}