using CarRent.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRent.Infrastructure.Configurations
{
    public class AdImageConfiguration : IEntityTypeConfiguration<AdImage>
    {
        public void Configure(EntityTypeBuilder<AdImage> builder)
        {
            builder.ToTable("AdImages");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.ImageUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(c => c.DisplayOrder)
                .HasDefaultValue(0);

            builder.HasOne(c => c.Ad)
                .WithMany(c => c.AdImages)
                .HasForeignKey(c => c.AdId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}