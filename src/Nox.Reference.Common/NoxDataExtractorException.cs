﻿using System.Runtime.Serialization;

namespace Nox.Reference.Common;

/// <summary>
/// Exception is thrown during import data process.
/// </summary>
internal class NoxDataExtractorException : Exception
{
#pragma warning disable S1075 // URIs should not be hardcoded
    private const string HelpLinkUri = "https://github.com/NoxOrg/Nox.Reference";
#pragma warning restore S1075 // URIs should not be hardcoded

    public NoxDataExtractorException()
    { }

    public NoxDataExtractorException(string message)
        : base(message) { }

    public NoxDataExtractorException(string message, Exception inner)
        : base(message, inner) { }

    public NoxDataExtractorException(string message, string resourceName)
       : base(message)
    {
        ResourceName = resourceName;
    }

    public string? ResourceName { get; }

    public override string? HelpLink { get; set; } = HelpLinkUri;

    public override string Message => $"{base.Message}. See {HelpLinkUri} for more info.";
}