using EFCore03.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore03.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Instructor> Instructors => Set<Instructor>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();

        protected override void OnConfiguring(DbContextOptionsBuilder o) =>
            o.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=OnlineLearningDb;Trusted_Connection=True;TrustServerCertificate=True");

        protected override void OnModelCreating(ModelBuilder mb) =>
            mb.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
    
    }

