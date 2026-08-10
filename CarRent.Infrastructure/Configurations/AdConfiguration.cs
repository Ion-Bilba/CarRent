using CarRent.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRent.Infrastructure.Configurations
{
    public class AdConfiguration : IEntityTypeConfiguration<Ad>
    {
        public void Configure(EntityTypeBuilder<Ad> builder)
        {
            builder.ToTable("Ads");
            builder.HasKey(c=>c.Id);

            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(c => c.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(c=>c.AdType)
                .HasConversion<string>()
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(c => c.IsActive)
                .HasDefaultValue(true);

            builder.Property(c => c.Views)
                .HasDefaultValue(0);

            builder.Property(c => c.CreatedAt).IsRequired();

            builder.Property(c => c.UpdatedAt).IsRequired(false);

            builder.HasOne(c => c.User)
                .WithMany(c => c.Ads)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(c => c.Car)
                .WithMany(c => c.Ads)
                .HasForeignKey(c => c.CarId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}