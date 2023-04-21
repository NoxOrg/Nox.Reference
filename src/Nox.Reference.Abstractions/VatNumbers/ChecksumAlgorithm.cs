using System.Text.Json.Serialization;

namespace Nox.Reference.Abstractions
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ChecksumAlgorithm
    {
        None,
        Luhn,
        Mod,
        ModAndSubstract,
        ModAndSubstract_IT,
        MX_Algorithm,
        DE_Algorithm,
        FR_Algorithm,
        CO_Algorithm,
        AU_Algorithm,
        BE_Algorithm,
        BR_Algorithm,
        CA_Algorithm,
        CH_Algorithm,
        GB_Algorithm,
        ES_Algorithm1,
        ES_Algorithm2,
        ES_Algorithm3,
        DK_Algorithm,
        AT_Algorithm,
        JP_Algorithm,
        CN_Algorithm,
        TR_Algorithm,
        SE_Algorithm,
        NO_Algorithm,
        RU_Algorithm,
        NZ_Algorithm,
        ID_Algorithm
    }
}