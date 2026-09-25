using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ViraelStudio.Domain.Entities;

namespace ViraelStudio.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Product>()
                .Property(p => p.Description)
                .HasMaxLength(2000);
            
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(8, 2);

            modelBuilder.Entity<Product>()
                .Property(p => p.ImageUrl)
                .HasMaxLength(500);

        }
    }
}
