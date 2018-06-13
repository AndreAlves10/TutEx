using System;
using System.ComponentModel.DataAnnotations;

namespace Meeting.Models
{
    public class TeacherStudentMeeting
    {
        [Key]
        public int Id { get; set; }

        public int TeacherID { get; set; }

        public int StudentID { get; set; }

        public DateTime MeetingDate { get; set; }

        public int Duration { get; set; }

        public bool AcceptedByTeacher { get; set; }

        public double TotalCost { get; set; }

        //Currency must be added

        public TeacherStudentMeeting() { }
    }
}
