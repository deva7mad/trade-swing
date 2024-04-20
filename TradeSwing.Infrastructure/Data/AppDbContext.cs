using Microsoft.EntityFrameworkCore;
using TradeSwing.Domain.Entities;

namespace TradeSwing.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<ArticleEntity> Articles { get; set; }

    // ReSharper disable once ConvertToPrimaryConstructor
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}