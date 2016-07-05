using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.services.Localization
{
   public interface ILocalizedEntityService
    {
        /// <summary>
        /// Find localized value
        /// </summary>
        /// <param name="languageId">Language identifier</param>
        /// <param name="entityId">Entity identifier</param>
        /// <param name="localeKeyGroup">Locale key group</param>
        /// <param name="localeKey">Locale key</param>
        /// <returns>Found localized value</returns>
       string GetLocalizedValue(int languageId, int entityId, string localeKeyGroup, string localeKey);
    }
}
