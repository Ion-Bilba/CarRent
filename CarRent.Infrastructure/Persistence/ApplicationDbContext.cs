using CarRent.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRent.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Car> Cars => Set<Car>();
        public DbSet<Favorite> Favorites => Set<Favorite>();
        public DbSet<AdImage> AdImages => Set<AdImage>();
        public DbSet<Ad> Ads => Set<Ad>();
    }
}
