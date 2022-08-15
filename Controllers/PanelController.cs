using AutoMapper;
using blog.Data;
using blog.Data.Filemanager;
using blog.Data.Repository;
using blog.Models;
using blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace blog.Controllers;

[Authorize(Roles = "Admin")]
public class PanelController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _dbContext;
    private readonly IRepository _repo;
    private readonly IMapper _mapper;
    private readonly IFileManager _fileManager;

    public PanelController(ILogger<HomeController> logger, AppDbContext dbContext, IRepository repo, IFileManager fileManager,IMapper mapper)
    {
        _logger = logger;
        _dbContext = dbContext;
        _repo = repo;
        _mapper = mapper;
        _fileManager = fileManager;
    }
    
    public IActionResult Index()
    {
        var articles = _repo.GetAllArticlesAsync();
        return View(articles.Result);
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if(id == null)
            return View(new ArticleViewModel());
        else
        {
            var article = _repo.GetArticleAsync((int) id).Result;
            return View(new ArticleViewModel()
            {
                ArticleId = article.ArticleId,
                Title = article.Title,
                Author = article.Author,
                Description = article.Description,
                CurrentThumbnail = article.ThumbnailUrl,
                Topics = article.Topics,
                WrittenOn = article.WrittenOn,
                Content = article.Content,
                Tags = article.Tags
            });
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(ArticleViewModel articleVM)
    {
        var article = _mapper.Map<Article>(articleVM);
        article.ThumbnailUrl = await _fileManager.SaveImage(articleVM.Thumbnail);

        if (article.ArticleId > 0)
            _repo.UpdateArticle(article);
        else
            _repo.AddArticle(article);
        
        if(await _repo.SaveChangesAsync())
            return RedirectToAction("Index");
        else
        {
            return View(_mapper.Map<ArticleViewModel>(article));
        }
    }

    [HttpGet]
    public async Task<IActionResult> Remove(int id)
    {
        _repo.RemoveArticle(id);
        await _repo.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}