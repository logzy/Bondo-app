using System.ComponentModel.DataAnnotations;

namespace Bondo.Application.Models.User;
public class LoginRequestModel
{
    [Required]
    public string UserName { get; set; } // email or phone
    [Required]
    public string Password { get; set; }
    // public bool RememberMe { get; set; }
}
