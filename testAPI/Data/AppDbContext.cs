using Microsoft.EntityFrameworkCore;
using System;

namespace testAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TaskItem>TaskItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.HasData(
                    new User
                    {
                        Id = 1,
                        Name = "TestUser",
                      
                    }
                );
            });
            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.Property(t => t.Title)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(t => t.Description)
                      .HasMaxLength(1000);

                entity.Property(t => t.Status)
                      .IsRequired();

                entity.Property(t => t.Priority)
                      .IsRequired();

                entity.Property(t => t.UserId)
                      .IsRequired();

    
            });
        }

    }
}
