using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test_SPA.Models
{
    public class TeachingArea
    {
        [Key]
        public int Id { get; private set; }

        public String Name { get; set; }

        public String Area { get; set; }

        public List<TeacherTeachingArea> Teachers { get; set; }

        public TeachingArea()
        {

        }

        public void AddTeacher(TeacherTeachingArea t)
        {
            Teachers.Add(t);
        }

        public void RemoveTeacher(TeacherTeachingArea t)
        {
            Teachers.Remove(t);
        }
    }
}
