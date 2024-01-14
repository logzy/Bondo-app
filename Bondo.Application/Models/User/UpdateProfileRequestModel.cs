using Bondo.Shared;

namespace Bondo.Application.Models.User;
public class UpdateProfileRequestModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }

    public ValidationResult Validate(){
        ValidateUserInfo validateUser = new ValidateUserInfo();
        return validateUser.IsValidName(FirstName).IsValidName(LastName)
        .IsValidPhone(PhoneNumber).Result();
    }
}
