using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.AppUser;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController(UserManager<AppUser> userManager) : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager = userManager;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (string.IsNullOrWhiteSpace(registerDto.Password) || string.IsNullOrWhiteSpace(registerDto.FullName))
                    return BadRequest(new { message = "Password and FullName are required" });

                var appUser = new AppUser
                {
                    UserName = registerDto.Username ?? string.Empty,
                    Email = registerDto.Email ?? string.Empty,
                    FullName = registerDto.FullName ?? string.Empty,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(new { message = "User registered successfully", userId = appUser.Id, username = appUser.UserName, fullName = appUser.FullName });
                    }
                    else
                    {
                        return StatusCode(500, new { message = "Failed to assign role", errors = roleResult.Errors.Select(e => e.Description) });
                    }
                }
                else
                {
                    return StatusCode(500, new { message = "Failed to create user", errors = createdUser.Errors.Select(e => e.Description) });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = "An error occurred", error = e.Message, innerException = e.InnerException?.Message });
            }
        }
    }
}