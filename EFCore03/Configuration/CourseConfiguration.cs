using EFCore03.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore03.Configuration
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {

        public void Configure(EntityTypeBuilder<Course> b)
        {
            b.HasKey(c => c.Id);
            b.Property(c => c.Title).IsRequired().HasMaxLength(150);
            b.Property(c => c.Description).HasMaxLength(1000);
            b.Property(c => c.Price).HasColumnType("decimal(10,2)");
            b.Property(c => c.Level).IsRequired();
            b.Property(c => c.IsPublished).HasDefaultValue(false);

            b.HasOne<Instructor>().WithMany().HasForeignKey(c => c.InstructorId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne<Category>().WithMany().HasForeignKey(c => c.CategoryId).OnDelete(DeleteBehavior.Restrict);

            var d = new DateTime(2024, 1, 1);
            b.HasData(
                new Course { Id = 1, Title = "ASP.NET Core Basics", Price = 100m, Level = CourseLevel.Beginner, IsPublished = true, PublishedAt = d, InstructorId = 1, CategoryId = 3 },
                new Course { Id = 2, Title = "ASP.NET Core MVC", Price = 150m, Level = CourseLevel.Intermediate, IsPublished = true, PublishedAt = d, InstructorId = 1, CategoryId = 3 },
                new Course { Id = 3, Title = "REST APIs Design", Price = 200m, Level = CourseLevel.Advanced, IsPublished = true, PublishedAt = d, InstructorId = 2, CategoryId = 2 },
                new Course { Id = 4, Title = "HTML & CSS", Price = 80m, Level = CourseLevel.Beginner, IsPublished = false, PublishedAt = null, InstructorId = 2, CategoryId = 4 },
                new Course { Id = 5, Title = "JavaScript Essentials", Price = 120m, Level = CourseLevel.Intermediate, IsPublished = true, PublishedAt = d, InstructorId = 3, CategoryId = 4 },
                new Course { Id = 6, Title = "Clean Architecture", Price = 250m, Level = CourseLevel.Advanced, IsPublished = false, PublishedAt = null, InstructorId = 3, CategoryId = 1 });
        } 
    }
}
