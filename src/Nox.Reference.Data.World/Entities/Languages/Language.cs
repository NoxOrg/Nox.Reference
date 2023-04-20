﻿using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

internal class Language : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string Name { get; set; } = string.Empty;
}