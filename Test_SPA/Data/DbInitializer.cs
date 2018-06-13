using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_SPA.DAL;
using Test_SPA.Models;

namespace Test_SPA.Data
{
    public static class DbInitializer
    {
        public static void Initialize(TestSPADBContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            if (context.Teachers.Any()) //DB has been seeded
                return;

            var teachers = new Teacher[]
            {
                new Teacher{Country="PT", AcademicalDegree="Bachelor"},
                new Teacher{Country="ES", AcademicalDegree="Master"}
            };

            foreach (Teacher s in teachers)
            {
                context.Teachers.Add(s);
            }
            context.SaveChanges();

            var teachingAreas = new TeachingArea[]
            {
                new TeachingArea{Name="Math", Area="Math" },
                new TeachingArea{Name="History", Area="History"}
            };

            foreach (TeachingArea ta in teachingAreas)
            {
                context.TeachingAreas.Add(ta);
            }
            context.SaveChanges();

            /*teachingAreas[0].AddTeacher(teachers[0]);
            teachingAreas[1].AddTeacher(teachers[1]);*/

            context.TeacherTeachingAreas.Add(new TeacherTeachingArea { Teacher = teachers[0], TeachingArea = teachingAreas[0] });
            

            context.SaveChanges();
        }
    }
}
