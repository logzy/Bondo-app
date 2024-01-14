using Bondo.Shared;

namespace Bondo.Application.Models.User;
public class ResetPinRequestModel
{
    public string OldPin { get; set; }
    public string NewPin { get; set; }

    public ValidationResult Validate()
    {
        // ValidateUserInfo validateUser = new ValidateUserInfo();
        return new ValidationResult(true, ""); //validateUser.IsValidPassword(Password).Result();
    }
}
