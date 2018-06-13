using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test_SPA.Models
{
    public class Teacher
    {
        //public String[] TeachingAreas { get; set; }

        [Key]
        public int Id { get; private set; }

        public String Country { get; set; }

        public String AcademicalDegree { get; set; }

        public List<TeacherTeachingArea> TeachingAreas { get; set; }

        public Teacher()
        {
            
        }

        public void AddTeachingArea(TeacherTeachingArea ta)
        {
            TeachingAreas.Add(ta);
        }

        public void RemoveTeachingArea(TeacherTeachingArea ta)
        {
            TeachingAreas.Remove(ta);
        }
    }
}
