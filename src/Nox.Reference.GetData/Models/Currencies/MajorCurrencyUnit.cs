﻿using System.Text.Json.Serialization;
using Nox.Reference.Abstractions.Currencies;

namespace Nox.Reference.GetData.Models;

public class MajorCurrencyUnit : IMajorCurrencyUnit
{
    [JsonPropertyName("name")] public string Name_ { get; set; } = "";
    [JsonPropertyName("symbol")] public string Symbol_ { get; set; } = "";

    [JsonIgnore] public string Name => Name_;
    [JsonIgnore] public string Symbol => Symbol_;
}