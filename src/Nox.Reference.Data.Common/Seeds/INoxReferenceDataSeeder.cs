namespace Nox.Reference.Data.Common;

internal interface INoxReferenceDataSeeder
{
    void Seed();

    string TargetFileName { get; }
}