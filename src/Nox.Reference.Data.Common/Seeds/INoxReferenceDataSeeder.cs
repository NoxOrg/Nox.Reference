﻿namespace Nox.Reference.Data.Common;

public interface INoxReferenceDataSeeder
{
    void Seed();

    string TargetFileName { get; }
}