using mine.core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.services.Localization
{
    public interface ILanguageService
    {
        /// <summary>
        /// Gets all languages
        /// </summary>
        /// <param name="storeId">Load records allowed only in a specified store; pass 0 to load all records</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Languages</returns>
        IList<Language> GetAllLanguages(bool showHidden = false, int storeId = 0);

        Language GetLanguageById(int langId);
    }
}
