using CarRent.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRent.Infrastructure.Configurations
{
    public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
    {
        public void Configure(EntityTypeBuilder<Favorite> builder)
        {
            builder.ToTable("Favorites");
            builder.HasKey(x => x.Id);

            builder.HasIndex(c => new {c.UserId,c.AdId})
                  .IsUnique();

            builder.HasOne(c => c.User)
                .WithMany(c => c.Favorites)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(C=>C.Ad)
                .WithMany(C=>C.Favorites)
                .HasForeignKey(c=>c.AdId)
                .OnDelete(DeleteBehavior.Restrict);
            
        }
    }
}