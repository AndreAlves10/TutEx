using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading.Tasks;
using Test_SPA.Models;

namespace Test_SPA.DAL
{
    public class TestSPADBContext : DbContext
    {

        public TestSPADBContext(DbContextOptions<TestSPADBContext> options) : base(options) {}

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeachingArea> TeachingAreas { get; set; }
        public DbSet<TeacherTeachingArea> TeacherTeachingAreas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teacher>().ToTable("Teacher");
            modelBuilder.Entity<TeachingArea>().ToTable("TeachingArea");
            modelBuilder.Entity<TeacherTeachingArea>().ToTable("TeacherTeachingArea");

            modelBuilder.Entity<TeacherTeachingArea>().HasKey(tta => new { tta.TeacherID, tta.TeachingAreaID });
            modelBuilder.Entity<TeacherTeachingArea>()
                .HasOne(tta => tta.Teacher)
                .WithMany(tta => tta.TeachingAreas)
                .HasForeignKey(tta => tta.TeacherID);
            modelBuilder.Entity<TeacherTeachingArea>()
                .HasOne(tta => tta.TeachingArea)
                .WithMany(tta => tta.Teachers)
                .HasForeignKey(tta => tta.TeachingAreaID);
        }
    }
}
