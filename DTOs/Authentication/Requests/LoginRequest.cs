using System;

namespace api.DTOs.Authentication
{
    public class LoginRequest
    {
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
