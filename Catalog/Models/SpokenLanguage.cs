using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Models
{
    public class SpokenLanguage
    {
        [Key]
        public int Id { get; private set; }

        public string Name { get; set; }

        public List<TeacherSpokenLanguages> TeachersLanguageRelation { get; set; }

        public SpokenLanguage() { }
    }
}
