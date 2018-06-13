using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_SPA.Models
{
    public class TeacherTeachingArea
    {
        public TeacherTeachingArea() { }

        public int TeacherID { get; set; }
        public Teacher Teacher { get; set; }

        public int TeachingAreaID { get; set; }
        public TeachingArea TeachingArea { get; set; }
    }
}
