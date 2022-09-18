using System.Diagnostics;
using blog.Data;
using blog.Data.Filemanager;
using blog.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using blog.Models;
using blog.Models.Comments;
using blog.ViewModels;

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

    public IActionResult Index(int pageNumber, string topic)
    {
        if (pageNumber < 1)
        {
            return RedirectToAction("Index", new {pageNumber = 1, topic});
        }
        
        var viewModel = _repo.GetAllArticlesAsync(topic, pageNumber).Result;
        
        return View(viewModel);
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

    public IActionResult Image(string image)
    {
        var fileType = image.Substring(image.LastIndexOf('.') + 1);
        return new FileStreamResult(_fileManager.ImageStream(image), $"image/{fileType}");
    }
    
    [HttpPost]
    public async Task<IActionResult> Comment(CommentViewModel cvm)
    {
        if (!ModelState.IsValid)
            return RedirectToAction("Article", new { id = cvm.ArticleId});

        if (cvm.MainCommentId == 0)
        {
            var comment = new MainComment()
            {
                ArticleId = cvm.ArticleId,
                Message = cvm.Message,
                Created = DateTime.Now
            };
            _repo.AddMainComment(comment);
        }
        else
        {
            var comment = new SubComment
            {
                MainCommentId = cvm.MainCommentId,
                Message = cvm.Message,
                Created = DateTime.Now
            };
            _repo.AddSubComment(comment);
        }
        await _repo.SaveChangesAsync();

        return RedirectToAction("Article", new { id = cvm.ArticleId});
    }
}