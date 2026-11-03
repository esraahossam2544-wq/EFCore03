using EFCore03.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore03.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> b)
        {
            b.HasKey(c => c.Id);
            b.Property(c => c.Name).IsRequired().HasMaxLength(100);
            b.Property(c => c.Description).HasMaxLength(300);

            // Self-reference: Restrict blocks deleting a parent that still has sub-categories
            b.HasOne<Category>()
             .WithMany()
             .HasForeignKey(c => c.ParentCategoryId)
             .OnDelete(DeleteBehavior.Restrict);

            b.HasData(
                new Category { Id = 1, Name = "Programming", Description = "All programming topics", ParentCategoryId = null },
                new Category { Id = 2, Name = "Backend", ParentCategoryId = 1 },
                new Category { Id = 3, Name = "ASP.NET", ParentCategoryId = 2 },
                new Category { Id = 4, Name = "Frontend", ParentCategoryId = 1 },
                new Category { Id = 5, Name = "Design", Description = "Design topics", ParentCategoryId = null });
        }
    }
}
