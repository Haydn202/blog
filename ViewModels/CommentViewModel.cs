using System.ComponentModel.DataAnnotations;

namespace blog.ViewModels;

public class CommentViewModel
{
    [Required]
    public int ArticleId { get; set; }
    [Required]
    public string Message { get; set; }
    [Required]
    public int MainCommentId { get; set; }
}