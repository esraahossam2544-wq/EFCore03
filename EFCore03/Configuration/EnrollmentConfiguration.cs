using EFCore03.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore03.Configuration
{
    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> b)
        {
            // Composite key => a student can enroll in the same course only once
            b.HasKey(e => new { e.StudentId, e.CourseId });
            b.Property(e => e.ProgressPercentage).HasColumnType("decimal(5,2)");
            b.Property(e => e.FinalGrade).HasColumnType("decimal(5,2)");
            b.Property(e => e.Status).IsRequired();
            b.ToTable(t => t.HasCheckConstraint("CK_Enrollment_Progress", "[ProgressPercentage] >= 0 AND [ProgressPercentage] <= 100"));

            b.HasOne<Student>().WithMany().HasForeignKey(e => e.StudentId).OnDelete(DeleteBehavior.Cascade);
            // Cascade: deleting a course removes its enrollments at the database level
            b.HasOne<Course>().WithMany().HasForeignKey(e => e.CourseId).OnDelete(DeleteBehavior.Cascade);
        }
    
    }
}
