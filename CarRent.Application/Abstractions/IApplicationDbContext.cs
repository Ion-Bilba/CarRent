using CarRent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;


namespace CarRent.Application.Abstractions
{
    public interface IApplicationDbContext
    {
        DbSet<Ad> Ads { get; }
        DbSet<Car> Cars { get; }
        DbSet<User> Users { get; }
        DbSet<Favorite> Favorites { get;}
        DbSet<AdImage> AdImages { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
