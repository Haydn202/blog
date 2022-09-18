using blog.Models;

namespace blog.ViewModels;

public class IndexViewModel
{
     public List<Article?> Articles { get; set; }
     public int PageNumber { get; set; }
     public bool NextPage { get; set; }
     public string Topic { get; set; }
     public int PageCount { get; set; }
     public IEnumerable<int> Pages { get; set; }
}