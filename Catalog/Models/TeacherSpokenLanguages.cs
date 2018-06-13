using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Models
{
    public class TeacherSpokenLanguages
    {

        public int TeacherID { get; set; }
        public Teacher Teacher { get; set; }

        public int SpokenLanguageID { get; set; }
        public SpokenLanguage SpokenLanguage { get; set; }

        public TeacherSpokenLanguages() { }
    }
}
