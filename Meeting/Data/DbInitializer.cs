using Meeting.Data;
using Meeting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Meeting.Data
{
    public static class DbInitializer
    {
        public static void Initialize(MeetingContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            if (context.Meetings.Any()) //DB has been seeded
                return;

            var meetings = new TeacherStudentMeeting[]
            {
                new TeacherStudentMeeting {
                   StudentID = 1,
                   TeacherID = 2,
                   MeetingDate = new DateTime(2018, 06, 10),
                   AcceptedByTeacher = false
                },
                 new TeacherStudentMeeting {
                   StudentID = 1,
                   TeacherID = 3,
                   MeetingDate = new DateTime(2018, 06, 10),
                   AcceptedByTeacher = true
                },
                new TeacherStudentMeeting {
                     StudentID = 2,
                     TeacherID = 1,
                     MeetingDate = new DateTime(2017, 05, 30),
                     AcceptedByTeacher = false
                }
            };

            foreach (TeacherStudentMeeting m in meetings)
            {
                context.Meetings.Add(m);
            }
            context.SaveChanges();
        }
    }
}
