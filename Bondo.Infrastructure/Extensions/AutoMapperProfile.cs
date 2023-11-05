using AutoMapper;
using Bondo.Application;
using Bondo.Application.Models.User;
using Bondo.Domain.Entities;

namespace Bondo.Infrastructure;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ApplicationUser, GetUserDto>().ReverseMap();
        // CreateMap<RegisterRequestModel, ApplicationUser>().ReverseMap();
        // CreateMap<ApplicationUser, UserDTO>();
    }
}