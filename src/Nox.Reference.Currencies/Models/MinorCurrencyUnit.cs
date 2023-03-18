﻿using System.Text.Json.Serialization;

namespace Nox.Reference.Abstractions.Currencies;

public class MinorCurrencyUnit : IMinorCurrencyUnit
{
    [JsonPropertyName("name")] public string Name_ { get; set; } = null;
    [JsonPropertyName("symbol")] public string Symbol_ { get; set; } = null;
    [JsonPropertyName("majorValue")] public decimal MajorValue_ { get; set; } = 0;
    
    [JsonIgnore] public string Name => Name_;
    [JsonIgnore] public string Symbol => Symbol_;
    [JsonIgnore] public decimal MajorValue => MajorValue_;
}   