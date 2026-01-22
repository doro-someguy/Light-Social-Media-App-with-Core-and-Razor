using Microsoft.EntityFrameworkCore;
namespace App.Models;

public class ForumDB : DbContext
{
    public ForumDB(DbContextOptions<ForumDB> options)
        : base(options) { }

    public DbSet<Topic> topics => Set<Topic>();
    public DbSet<Account> accounts => Set<Account>();
    public DbSet<Comment> comments => Set<Comment>();
}
