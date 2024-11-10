using System.ComponentModel.DataAnnotations;
using api.Config.Filters;

namespace api.DTOs.Authentication.Requests
{
    public class RegisterRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        [ValidateFile(ErrorMessage = "File types allowed are {1} and size is {2}")]
        public IFormFile? Picture { get; set; } = null;
        public RegisterUserRoles? Role { get; set; } = RegisterUserRoles.Customer;
    }

    public enum RegisterUserRoles
    {
        Customer,
        Owner,
    }
}
