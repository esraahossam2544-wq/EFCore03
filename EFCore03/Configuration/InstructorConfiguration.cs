using EFCore03.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore03.Configuration
{
    public class InstructorConfiguration  : IEntityTypeConfiguration<Instructor>
{
    public void Configure(EntityTypeBuilder<Instructor> b)
    {
        b.HasKey(i => i.Id);
        b.Property(i => i.FullName).IsRequired().HasMaxLength(100);
        b.Property(i => i.Email).IsRequired().HasMaxLength(150);
        b.Property(i => i.Bio).HasMaxLength(500);
        b.Property(i => i.JoinedAt).IsRequired();

        b.HasData(
            new Instructor { Id = 1, FullName = "Ahmed Hassan", Email = "ahmed@learn.com", Bio = "Senior .NET developer", JoinedAt = new DateTime(2022, 1, 10) },
            new Instructor { Id = 2, FullName = "Sara Ali", Email = "sara@learn.com", Bio = null, JoinedAt = new DateTime(2022, 6, 1) },
            new Instructor { Id = 3, FullName = "Omar Khaled", Email = "omar@learn.com", Bio = "UI/UX designer", JoinedAt = new DateTime(2023, 3, 15) });
    }
    
    }
}
