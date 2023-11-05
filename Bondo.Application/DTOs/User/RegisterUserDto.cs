using Bondo.Application.Common.Mappings;
using Bondo.Domain.Entities;

namespace Bondo.Application;
public class RegisterUserDto : IMapFrom<ApplicationUser>
{
    public string Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string PhoneNumber { get; set; }
}
