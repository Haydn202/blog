using System.ComponentModel.DataAnnotations;

namespace blog.ViewModels;

public class LoginViewModel
{
    public string UserName { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
}