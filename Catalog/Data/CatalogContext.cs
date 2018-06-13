using Catalog.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Data
{
    public class CatalogContext : DbContext
    {

        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options) { }

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<SpokenLanguage> SpokenLanguages { get; set; }
        public DbSet<TeacherSpokenLanguages> TeacherSpokenLanguages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teacher>().ToTable("Teacher");
            modelBuilder.Entity<SpokenLanguage>().ToTable("SpokenLanguage");

            modelBuilder.Entity<TeacherSpokenLanguages>()
                .HasKey(tsl => new { tsl.TeacherID, tsl.SpokenLanguageID });

            modelBuilder.Entity<TeacherSpokenLanguages>()
                .HasOne(tsl => tsl.Teacher)
                .WithMany(t => t.TeacherSpokenLanguages)
                .HasForeignKey(tsl => tsl.TeacherID);

            modelBuilder.Entity<TeacherSpokenLanguages>()
                .HasOne(tsl => tsl.SpokenLanguage)
                .WithMany(sl => sl.TeachersLanguageRelation)
                .HasForeignKey(tsl => tsl.SpokenLanguageID);
        }
    }
}
