using System.Text.Json.Serialization;

namespace Nox.Reference.Abstractions.VatNumbers
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ChecksumAlgorithm
    {
        None,
        Luhn,
        Mod,
        ModAndSubstract,
        ModAndSubstractItaly,
        MexicanAlgorithm,
        GermanAlgorithm,
        FrenchAlgorithm,
        ColombianAlgorithm,
        AustralianAlgorithm,
        BelgianAlgorithm,
        BrazilianAlgorithm,
        CanadianAlgorithm,
        SwissAlgorithm,
        BritishAlgorithm,
        Spanish1Algorithm,
        Spanish2Algorithm,
        Spanish3Algorithm,
        DenmarkAlgorithm,
        AustrianAlgorithm,
        JapaneseAlgorithm,
        ChineseAlgorithm,
        TurkishAlgorithm,
        SwedishAlgorithm,
        NorwegianAlgorithm,
        RussianAlgorithm,
        NewZealandAlgorithm,
        IndonesianAlgorithm
    }
}
