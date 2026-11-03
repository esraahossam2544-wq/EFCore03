using EFCore03.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore03.Configuration
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
    public void Configure(EntityTypeBuilder<Student> b)
        {
            b.HasKey(s => s.Id);
            b.Property(s => s.FullName).IsRequired().HasMaxLength(100);
            b.Property(s => s.Email).IsRequired().HasMaxLength(150);
            b.Property(s => s.DateOfBirth).IsRequired();
        }
    }
}
