using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options): base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Platform>()
            .HasMany(m=>m.Commands)
            .WithOne(o=>o.Platform)
            .HasForeignKey(k=>k.PlatformId);

            modelBuilder.Entity<Command>()
            .HasOne(o=>o.Platform)
            .WithMany(m=>m.Commands)
            .HasForeignKey(k=>k.PlatformId);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Command> Commands { get; set; }
        public DbSet<Platform> Platforms { get; set; }
    }
}