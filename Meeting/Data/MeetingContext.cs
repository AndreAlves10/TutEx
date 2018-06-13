using Meeting.Models;
using Microsoft.EntityFrameworkCore;

namespace Meeting.Data
{
    public class MeetingContext : DbContext
    {
        public MeetingContext(DbContextOptions<MeetingContext> options) : base(options) { }

        public DbSet<TeacherStudentMeeting> Meetings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeacherStudentMeeting>().ToTable("TeacherStudentMeeting");
        }
    }
}
