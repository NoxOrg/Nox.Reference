namespace Nox.Reference.Data.Common;

// TODO: make internal
public interface INoxReferenceDataSeeder
{
    void Seed();

    string TargetFileName { get; }
}