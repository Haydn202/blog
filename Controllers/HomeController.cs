using System.Diagnostics;
using blog.Data;
using blog.Data.Filemanager;
using blog.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using blog.Models;

namespace blog.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _dbContext;
    private readonly IRepository _repo;
    private readonly IFileManager _fileManager;

    public HomeController(ILogger<HomeController> logger, AppDbContext dbContext, IRepository repo, IFileManager fileManager)
    {
        _fileManager = fileManager;
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
    
    [HttpGet("/Image/{image}")]
    public IActionResult Image(string image)
    {
        var fileType = image.Substring(image.LastIndexOf('.') + 1);
        return new FileStreamResult(_fileManager.ImageStream(image), $"image/{fileType}");
    }
}