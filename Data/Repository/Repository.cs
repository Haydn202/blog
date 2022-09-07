using blog.Models;
using blog.Models.Comments;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace blog.Data.Repository;

public class Repository : IRepository
{
    private AppDbContext _context;
    //private Logger<Repository> _logger;

    public Repository(AppDbContext context)
    {
        _context = context;
        //_logger = logger;
    }

    public async Task<Article?> GetArticleAsync(int id)
    {
        return await _context.Articles
            .Include(a => a.Comments)
            .ThenInclude(a => a.SubComments)
            .FirstOrDefaultAsync(a => a.ArticleId.Equals(id));
    }

    public async Task<List<Article?>> GetAllArticlesAsync()
    {
        return await _context.Articles.ToListAsync();
    }
    
    public async Task<List<Article?>> GetAllArticlesAsync(string topic)
    {
        return await _context.Articles.Where(a => a.Topics.ToLower().Contains(topic.ToLower())).ToListAsync();
    }

    public void AddArticle(Article? article)
    {
        _context.Articles.Add(article);
    }

    public void RemoveArticle(int id)
    {
        _context.Articles.Remove(GetArticleAsync(id).Result);
    }

    public void UpdateArticle(Article article)
    {
        _context.Articles.Update(article);
    }

    public async Task<bool> SaveChangesAsync()
    {
        if (await _context.SaveChangesAsync() > 0)
        {
            return true;
        }
        return false;
    }

    public void AddMainComment(MainComment comment)
    {
        _context.MainComments.Add(comment);
    }
    
    public void AddSubComment(SubComment comment)
    {
        _context.SubComment.Add(comment);
    }
}