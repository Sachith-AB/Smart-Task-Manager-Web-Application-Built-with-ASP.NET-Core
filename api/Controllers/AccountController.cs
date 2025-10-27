using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.AppUser;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager) : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly ITokenService _tokenService = tokenService;

        private readonly SignInManager<AppUser> _signInManager = signInManager;

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
                        var token = _tokenService.CreateToken(appUser);

                        var response = new AuthResponseDto
                        {
                            UserId = appUser.Id,
                            Username = appUser.UserName,
                            Email = appUser.Email,
                            FullName = appUser.FullName,
                            Token = token,
                            Message = "User registered successfully"
                        };

                        return Ok(response);
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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(loginDto.Username) || string.IsNullOrWhiteSpace(loginDto.Password))
            {
                return BadRequest(new { message = "Username and password are required" });
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            var response = new AuthResponseDto
            {
                UserId = user.Id ?? string.Empty,
                Username = user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                FullName = user.FullName ?? string.Empty,
                Token = _tokenService.CreateToken(user),
                Message = "Login successful"
            };

            return Ok(response);
        }

    }
}