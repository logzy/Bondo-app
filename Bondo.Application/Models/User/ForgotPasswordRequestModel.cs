using Bondo.Shared;

namespace Bondo.Application.Models.User;
public class ForgotPasswordRequestModel
{
    public string Email { get; set; }
    public ValidationResult Validate(){
        ValidateUserInfo validateUser = new ValidateUserInfo();
        return validateUser.IsValidEmail(Email).Result();
    }
}
