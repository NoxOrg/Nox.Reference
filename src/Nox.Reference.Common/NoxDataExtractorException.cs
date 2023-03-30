using System.Reflection;

namespace Nox.Reference.Common;

[Serializable]
public class NoxDataExtractorException : Exception
{
    private const string HelpLinkUri = "https://github.com/NoxOrg/Nox.Reference";

    public NoxDataExtractorException()
    { }

    public NoxDataExtractorException(string message)
        : base(message) { }

    public NoxDataExtractorException(string message, Exception inner)
        : base(message, inner) { }

    public string ResourceName { get; }

    public override string? HelpLink
    {
        get => HelpLinkUri;
        set => base.HelpLink = value;
    }

    public override string Message => $"{base.Message}. See {HelpLinkUri} for more info.";
}