using System;
using api.Config.Enums;
using api.Models.User;

namespace api.Services.Authentication
{
    public interface ITokenService
    {
        public string Generate(AppUserModel user, string role);
    }
}
