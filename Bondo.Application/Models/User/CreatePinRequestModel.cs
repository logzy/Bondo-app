using Bondo.Shared;

namespace Bondo.Application.Models.User;
public class CreatePinRequestModel
{
    public string Pin { get; set; }
    public ValidationResult Validate()
    {
        // ValidateUserInfo validateUser = new ValidateUserInfo();
        return new ValidationResult(true, ""); //validateUser.IsValidPassword(Password).Result();
    }
}
