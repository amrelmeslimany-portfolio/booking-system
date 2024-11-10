using api.DTOs.Authentication.Requests;
using api.DTOs.Authentication.Responses;
using api.Models.User;
using AutoMapper;

namespace api.DTOs.Authentication
{
    public class AuthenticationMapper : Profile
    {
        public AuthenticationMapper()
        {
            CreateMap<RegisterRequest, AppUserModel>()
                .ForSourceMember(src => src.Picture, opt => opt.DoNotValidate())
                .ForSourceMember(src => src.Role, opt => opt.DoNotValidate());

            CreateMap<AppUserModel, AuthResponse>();
        }
    }
}
