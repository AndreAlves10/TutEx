using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; private set; }

        #region PersonalInformation
        public string Name { get; set; }

        public string Nacionality { get; set; }

        public DateTime Birthday { get; set; }

        public List<TeacherSpokenLanguages> TeacherSpokenLanguages { get; set; }

        public string AcademicalDegree { get; set; }
        #endregion

        #region TeachingInfo
        public string TeachingAreas { get; set; }

        public double PricePerHour { get; set; }
        #endregion

        public Teacher() { }
    }
}
