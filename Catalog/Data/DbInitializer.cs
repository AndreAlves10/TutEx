using Catalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Catalog.Data
{
    public static class DbInitializer
    {
        public static void Initialize(CatalogContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            if (context.Teachers.Any()) //DB has been seeded
                return;

            var spokenLanguages = new SpokenLanguage[]
            {
                new SpokenLanguage {Name = "PT"},
                new SpokenLanguage {Name = "EN"}
            };

            foreach (SpokenLanguage sl in spokenLanguages)
            {
                context.SpokenLanguages.Add(sl);
            }
            context.SaveChanges();

            var teachers = new Teacher[]
            {
                new Teacher {
                    Name = "André Alves",
                    Birthday =  new DateTime(1991, 6, 10),
                    Nacionality = "PT",
                    AcademicalDegree = "Master",
                    //SpokenLanguages = new List<SpokenLanguage>(new SpokenLanguage[]{spokenLanguages[0], spokenLanguages[1]}),
                    PricePerHour = 15.0,
                    TeachingAreas = "Math, History"
                },
                new Teacher {
                    Name = "Sofia Natálio",
                    Birthday =  new DateTime(1990, 8, 25),
                    Nacionality = "PT",
                    AcademicalDegree = "Bachelor",
                    //SpokenLanguages = new List<SpokenLanguage>(new SpokenLanguage[]{spokenLanguages[0], spokenLanguages[1]}),
                    PricePerHour = 20.0,
                    TeachingAreas = "Programming, History, Arts"
                }
            };

            foreach (Teacher s in teachers)
            {
                context.Teachers.Add(s);
            }
            context.SaveChanges();

            var teacherSpokenLanguages = new TeacherSpokenLanguages[]
            {
                new TeacherSpokenLanguages
                {
                    Teacher = teachers[0],
                    SpokenLanguage = spokenLanguages[0]
                },
                new TeacherSpokenLanguages
                {
                    Teacher = teachers[1],
                    SpokenLanguage = spokenLanguages[1]
                }
            };

            foreach (TeacherSpokenLanguages tsl in teacherSpokenLanguages)
            {
                context.TeacherSpokenLanguages.Add(tsl);
            }
            context.SaveChanges();
        }
    }
}
