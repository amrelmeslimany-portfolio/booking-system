using System.Security.Claims;
using api.DTOs.Authentication;
using api.DTOs.Authentication.Requests;
using api.DTOs.Authentication.Responses;
using api.Models.User;
using api.Services.Authentication;
using api.Services.Common;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(
        ITokenService tokenService,
        UserManager<AppUserModel> userManager,
        RoleManager<IdentityRole> roleManager,
        IUploadFile uploadFile,
        IMapper mapper
    ) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterRequest body)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var foundUser = await userManager.FindByEmailAsync(body.Email);

                if (foundUser != null)
                {
                    ModelState.AddModelError("Email", "Email already found");
                    return ValidationProblem(new ValidationProblemDetails(ModelState));
                }

                var user = mapper.Map<AppUserModel>(body);

                if (body.Picture != null)
                {
                    user.Picture = await uploadFile.Create(body.Picture, @"Files\Images");
                }

                var createdUser = await userManager.CreateAsync(user, body.Password);

                if (createdUser.Succeeded)
                {
                    var foundRole = await roleManager.FindByNameAsync(
                        Enum.GetName(
                            typeof(RegisterUserRoles),
                            body.Role ?? RegisterUserRoles.Customer
                        )!
                    );

                    var roleResult = await userManager.AddToRoleAsync(
                        user,
                        foundRole?.Name ?? "Customer"
                    );

                    if (roleResult.Succeeded)
                    {
                        AuthResponse response = mapper.Map<AuthResponse>(user);
                        var roles = await userManager.GetRolesAsync(user);
                        response.Role = roles[0];
                        response.Token = tokenService.Generate(user, foundRole!.Name!);
                        return Ok(response);
                    }
                    else
                    {
                        return BadRequest(roleResult.Errors);
                    }
                }
                else
                {
                    return BadRequest(createdUser.Errors);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest body)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var foundUser = await userManager.FindByEmailAsync(body.Email);

                if (
                    foundUser == null
                    || !await userManager.CheckPasswordAsync(foundUser, body.Password)
                )
                {
                    return BadRequest(
                        new ProblemDetails { Title = "Invalid user email or password" }
                    );
                }

                AuthResponse response = mapper.Map<AuthResponse>(foundUser);
                var foundRole = await userManager.GetRolesAsync(foundUser);
                var roles = await userManager.GetRolesAsync(foundUser);
                response.Role = roles[0];
                response.Token = tokenService.Generate(foundUser, foundRole[0]);
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
