using blog.Models;
using blog.Models.Comments;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace blog.Data;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    public DbSet<Article> Articles { get; set; }
    public DbSet<MainComment> MainComments { get; set; }
    public DbSet<SubComment> SubComment { get; set; }

}