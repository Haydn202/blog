using blog.Models;
using blog.Models.Comments;
using blog.ViewModels;

namespace blog.Data.Repository;

public interface IRepository
{
    public Task<Article?> GetArticleAsync(int id);
    Task<List<Article?>> GetAllArticlesAsync();
    Task<IndexViewModel> GetAllArticlesAsync(string topic, int pageNumber);
    void AddArticle(Article? article);
    void RemoveArticle(int id);
    void UpdateArticle(Article article);

    Task<bool> SaveChangesAsync();
    void AddSubComment(SubComment comment);
    void AddMainComment(MainComment comment);
    
}