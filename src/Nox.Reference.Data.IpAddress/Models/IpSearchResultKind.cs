namespace Nox.Reference;

public enum IpSearchResultKind
{
    /// <summary>
    /// Ip Address input string has incorrect format.
    /// </summary>
    IncorrectInput,

    /// <summary>
    /// Ip Address has not been found
    /// </summary>
    NotFound = 1,

    /// <summary>
    /// Ip Address has been found
    /// </summary>
    Success = 2
}