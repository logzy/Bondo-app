using Bondo.Shared;

namespace Bondo.Application.Models.User;
public class PasswordResetRequestModel
{
    public string Id { get; set; }
    public string Password { get; set; }
    public string Token { get; set; }

    public ValidationResult Validate()
    {
        ValidateUserInfo validateUser = new ValidateUserInfo();
        return validateUser.IsValidPassword(Password).Result();
    }
}
