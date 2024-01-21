﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Task = Models.Task;

namespace Repository.EFInitial
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Task> Task { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // didn't work
            // modelBuilder.Entity<Task>()
            //     .Property(t => t.UpdatedAt)
            //     .ValueGeneratedOnAddOrUpdate()
            //   //  .HasDefaultValueSql("DATETIME('now')")
            //     .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Save);
            
                
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
