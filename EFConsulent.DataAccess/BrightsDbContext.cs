using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using EFConsulent.Domain.Entities;
using EFConsulent.Domain;

namespace EFConsulent.DataAccess
{
    
        public class BrightsDbContext : DbContext
        {
            public DbSet<Consulent> Consulent => Set<Consulent>();
            public DbSet<Course> Course => Set<Course>();
            public DbSet<CourseModule> CourseModule => Set<CourseModule>();
            public DbSet<Email> Email => Set<Email>();
            public DbSet<Module> Module => Set<Module>();

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(
                @"Server = (localdb)\MSSQLLocalDB; " +
                "Database = EFBrights; " +
                "Trusted_Connection = True;");
            }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<CourseModule>().HasKey(c => new { c.CourseId, c.ModuleId });
            }
        }

    }

