﻿using Nox.Reference.Data.World.Extensions.Queries;

namespace Nox.Reference.Data.World.Tests;

public class LanguagesTests
{
    [TestCase("uk", "true", "Ukrainian", "4")]
    public void GetLanguages_ReturnsProperValue(
        string input,
        string expectedIsCommon,
        string expectedName,
        string expectedCount)
    {
        var info = WorldInfo.Languages.Get(input);

        Assert.That(info, Is.Not.Null);
        Assert.That(info?.Common, Is.EqualTo(bool.Parse(expectedIsCommon)));
        Assert.That(info?.Name, Is.EqualTo(expectedName));
        Assert.That(info?.NameTranslations.Count, Is.EqualTo(int.Parse(expectedCount)));
    }
}