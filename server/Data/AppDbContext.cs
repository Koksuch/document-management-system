using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentItem> DocumentItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DocumentItem>()
                .HasOne(di => di.Document)
                .WithMany(d => d.Items)
                .HasForeignKey(di => di.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}