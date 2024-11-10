using System;

namespace api.DTOs.Authentication.Responses
{
    public class AuthResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string? Picture { get; set; }
        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }
    }
}
