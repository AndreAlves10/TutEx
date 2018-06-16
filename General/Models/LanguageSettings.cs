using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace General.Models
{
    public sealed class LanguageSettings
    {
        static readonly LanguageSettings _instance = new LanguageSettings();

        public static LanguageSettings Instance
        {
            get
            {
                return _instance;
            }
        }

        private LanguageSettings() { }

        private void ChangeLanguage()
        {

        }
    }
}
