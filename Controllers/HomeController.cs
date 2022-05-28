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
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}