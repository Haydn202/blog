using System.Globalization;
using blog.Helpers;
using blog.Models;
using blog.Models.Comments;
using blog.ViewModels;
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
        return await _context.Articles
            .ToListAsync();
    }
    
    public async Task<IndexViewModel> GetAllArticlesAsync(string topic, int pageNumber)
    {
        Func<Article, bool> InCategory = (article) => { return article.Topics.ToLower().Equals(topic.ToLower()); };
        
        const int pageSize = 25;
        var skipAmount = pageSize * (pageNumber - 1);

        var query = _context.Articles
            .Skip(skipAmount)
            .Take(pageSize);

        if (!string.IsNullOrEmpty(topic))
            query = query.Where(x => InCategory(x));
        
        var articleCount = _context.Articles.Count();

        var pageCount = (int) Math.Ceiling((double) articleCount / pageSize);

        return new IndexViewModel
        {
            PageNumber = pageNumber,
            NextPage = articleCount >  skipAmount + pageSize,
            Articles = query.ToList(),
            Topic = topic,
            PageCount = pageCount,
            Pages = PageHelper.PageNumbers(pageNumber, pageCount).ToList()
        };
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