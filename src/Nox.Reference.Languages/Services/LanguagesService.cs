using Nox.Reference.Abstractions.Languages;

namespace Nox.Reference.Languages.Services
{
    internal class LanguagesService : ILanguagesService
    {

        private static IReadOnlyList<ILanguageInfo> _languages = new List<ILanguageInfo>();

        public static void Init(IEnumerable<ILanguageInfo> langauges)
        {
            _languages = new List<ILanguageInfo>(langauges);
        }

        public IReadOnlyList<ILanguageInfo> GetLanguages()
        {
            return _languages;
        }
    }
}
