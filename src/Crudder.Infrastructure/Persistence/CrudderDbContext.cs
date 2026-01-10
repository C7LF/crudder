using Microsoft.EntityFrameworkCore;
using Crudder.Domain.Entities;

namespace Crudder.Infrastructure.Persistence;

public class CrudderDbContext(DbContextOptions<CrudderDbContext> options) : DbContext(options)
{

    // DbSets
    public DbSet<TodoItem> TodoItems { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Label> Labels { get; set; } = null!;
    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User -> Todos (one-to-many)
        modelBuilder.Entity<TodoItem>()
            .HasOne(t => t.User)
            .WithMany(u => u.TodoItems)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // User -> Labels (one-to-many)
        modelBuilder.Entity<Label>()
            .HasOne(l => l.User)
            .WithMany(u => u.Labels)
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // User -> RefreshTokens (one-to-many)
        modelBuilder.Entity<RefreshToken>()
            .HasOne(rt => rt.User)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Todo <-> Label (many-to-many)
        modelBuilder.Entity<TodoItem>()
            .HasMany(t => t.Labels)
            .WithMany(l => l.Todos)
            .UsingEntity<Dictionary<string, object>>(
                "TodoLabels",
                j => j.HasOne<Label>().WithMany().HasForeignKey("LabelId"),
                j => j.HasOne<TodoItem>().WithMany().HasForeignKey("TodoItemId")
            );
    }
}

