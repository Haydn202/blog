using blog.Models;
using Microsoft.EntityFrameworkCore;

namespace blog.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    public DbSet<ArticleInfo> Articles { get; set; }
    public DbSet<ArticleContent> ArticleContents { get; set; }
    
}