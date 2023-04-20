using Nox.Reference.Abstractions.Languages;

namespace Nox.Reference.Languages.Services
{
    public interface ILanguagesService
    {
        /// <summary>
        /// Get language list
        /// </summary>
        /// <returns>Languages info</returns>
        public IReadOnlyList<ILanguageInfo> GetLanguages();
    }
}
