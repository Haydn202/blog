using System.Diagnostics;
using blog.Data;
using blog.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using blog.Models;

namespace blog.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _dbContext;
    private readonly IRepository _repo;

    public HomeController(ILogger<HomeController> logger, AppDbContext dbContext, IRepository repo)
    {
        _logger = logger;
        _dbContext = dbContext;
        _repo = repo;
    }

    public IActionResult Index()
    {
        var articles = _repo.GetAllArticlesAsync();
        return View(articles.Result);
    }

    public IActionResult Privacy()
    {
        return View();
    }
    
    public IActionResult Article(int id)
    {
        var article = _repo.GetArticleAsync(id);
        return View(article.Result);
    }
    
    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if(id == null)
            return View(new Article());
        else
        {
            var article = _repo.GetArticleAsync((int) id);
            return View(article.Result);
        }
        
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(Article article)
    {
        if (article.ArticleId > 0)
            _repo.UpdateArticle(article);
        else
            _repo.AddArticle(article);
        
        if(await _repo.SaveChangesAsync())
            return RedirectToAction("Index");
        else
        {
            return View(article);
        }
        
    }

    [HttpGet]
    public async Task<IActionResult> Remove(int id)
    {
        _repo.RemoveArticle(id);
        await _repo.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}