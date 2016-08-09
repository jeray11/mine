using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mine.web.Models.Common
{
    public class LanguageSelectorModel
    {
        public LanguageSelectorModel()
        {
            AvailableLanguages = new List<LanguageModel>();
        }

        public IList<LanguageModel> AvailableLanguages { get; set; }
        public int CurrentLanguageId { get; set; }
        public bool UseImages { get; set; }
    }
}