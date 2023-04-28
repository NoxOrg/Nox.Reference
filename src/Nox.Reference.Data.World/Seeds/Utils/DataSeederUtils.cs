using HtmlAgilityPack;

namespace Nox.Reference.Data.World.Seeds.Utils
{
    internal static class DataSeederUtils
    {
        internal static string GetNodeText(HtmlNode? htmlNode)
        {
            var value = htmlNode?.InnerText;
            if (string.IsNullOrWhiteSpace(value))
            {
#pragma warning disable S112 // General exceptions should never be thrown
                throw new Exception("Error! Null value was encountered on not nullable node.");
#pragma warning restore S112 // General exceptions should never be thrown
            }

            return value.Trim();
        }

        internal static string? GetNodeTextOrNull(HtmlNode? htmlNode)
        {
            var value = htmlNode?.InnerText;
            if (string.IsNullOrWhiteSpace(value))
            {
                value = null;
            }

            return value?.Trim();
        }

        internal static string GetDateNode(HtmlNode? htmlNode)
        {
            var value = htmlNode?.InnerHtml?.Replace("<br>", Environment.NewLine);
            if (string.IsNullOrWhiteSpace(value))
            {
#pragma warning disable S112 // General exceptions should never be thrown
                throw new Exception("Error! Null value was encountered on not nullable node.");
#pragma warning restore S112 // General exceptions should never be thrown
            }

            return value.Trim();
        }
    }
}
