using blog.Models;

namespace blog.Data.Repository;

public interface IRepository
{
    public Task<Article?> GetArticleAsync(int id);
    Task<List<Article?>> GetAllArticlesAsync();
    void AddArticle(Article? article);
    void RemoveArticle(int id);
    void UpdateArticle(Article article);

    Task<bool> SaveChangesAsync();
}