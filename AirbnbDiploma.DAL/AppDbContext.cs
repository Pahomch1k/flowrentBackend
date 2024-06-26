﻿using System.Reflection;
using AirbnbDiploma.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AirbnbDiploma.DAL;

public class AppDbContext : IdentityDbContext<User, Role, Guid>
{
    public AppDbContext(DbContextOptions options)
    : base(options)
    {
    }

    public DbSet<Booking> Countries { get; set; }

    public DbSet<Review> Reviews { get; set; }

    public DbSet<Stay> Stays { get; set; }

    public DbSet<Tag> Tags { get; set; }

    public DbSet<WhishlistCategory> WhishlistCategories { get; set; }

    public DbSet<WhishlistItem> WhishlistItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        foreach (var entity in builder.Model.GetEntityTypes())
        {
            if (entity?.GetTableName() != null)
            {
                entity.SetTableName(entity.GetTableName()?.ToLower());
            }
        }
    }
}
