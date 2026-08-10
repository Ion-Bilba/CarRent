using System;
using System.Collections.Generic;
using System.Text;
using CarRent.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRent.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users")
                .HasKey(t => t.Id);

            builder.Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(c => c.Email)
                   .IsUnique();

            builder.Property(c => c.PasswordHash).IsRequired();

            builder.Property(c => c.PhoneNumber)
                .IsRequired(false)
                .HasMaxLength(20);

            builder.Property(c=>c.CreatedAt).IsRequired();
        }
    }
}