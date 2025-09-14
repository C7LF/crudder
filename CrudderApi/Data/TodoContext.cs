using Microsoft.EntityFrameworkCore;
using CrudderApi.Models;

namespace CrudderApi.Data
{
    public class TodoContext(DbContextOptions<TodoContext> options) : DbContext(options)
    {
        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Label> Labels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User -> Todos
            modelBuilder.Entity<TodoItem>()
                .HasOne(t => t.User)
                .WithMany(u => u.TodoItems)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User -> Labels
            modelBuilder.Entity<Label>()
                .HasOne(label => label.User)
                .WithMany(user => user.Labels)
                .HasForeignKey(label => label.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Many-to-many Todo <-> Label

            modelBuilder.Entity<TodoItem>()
                .HasMany(t => t.Labels)
                .WithMany(l => l.Todos)
                .UsingEntity(j => j.ToTable("TodoLabels"));
        }
    }
}