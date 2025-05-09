﻿using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementBackend.Models;
using TaskManagementBackend.Models.DTO;
using TaskManagementBackend.Repositories.Interface;
using TaskManagementBackend.Services;

namespace TaskManagementBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;

        public AuthController(IUserRepository userRepository,JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _userRepository.GetByUsernameAsync(model.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password,user.PasswordHash))
            {
                return Unauthorized("Invalid Credentials..");
            }

            var token = _jwtService.GenerateToken(user);

            var response = new LoginModelDto
            {
                Username = user.Username,
                Password = user.PasswordHash,
                Token = token
            };
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            var existingUser = await _userRepository.GetByUsernameAsync(registerModel.Username);

            if(existingUser!=null)
            {
                return BadRequest("Username already exists..");
            }

            var newUser = new User
            {
                Username= registerModel.Username,
                PasswordHash=BCrypt.Net.BCrypt.HashPassword(registerModel.Password),
                Role= registerModel.Role
            };

            var result=await _userRepository.CreateUserAsync(newUser);
            if (!result)
            {
                return BadRequest("User Registration failed..");
            }
            
            var response = new RegisterModelDto
            {
                Username=newUser.Username,
                Role=newUser.Role
            };


            return Ok(response);

        }
    }
}
