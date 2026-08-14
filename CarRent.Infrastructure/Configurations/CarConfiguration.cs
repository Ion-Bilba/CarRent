using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CarRent.Domain.Entities;

namespace CarRent.Infrastructure.Configurations
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.ToTable("Cars");
            builder.HasKey(x => x.Id);

            builder.Property(C=>C.Brand)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c=>c.Model)
                .IsRequired()
                .HasMaxLength (50);

            builder.Property(c => c.Year).IsRequired();

            builder.Property(c => c.Color)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(c=>c.Mileage).IsRequired();

            builder.Property(c => c.FuelType)
       .HasConversion<string>() 
       .HasMaxLength(30)        
       .IsRequired();

            builder.Property(c=>c.EngineCapacity).IsRequired();

            builder.Property(c => c.Transmission)
                .HasConversion<string>()
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(c => c.TractionType)
    .HasConversion<string>()
    .HasMaxLength(30)
    .IsRequired();
        }
    }
}