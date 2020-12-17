﻿using System.Threading.Tasks;
using EventApp.Models.Communication;
using EventApp.Services;
 using Microsoft.AspNetCore.Authorization;
 using Microsoft.AspNetCore.Mvc;

namespace EventApp.Controllers
{
    [AllowAnonymous]
    [Produces("application/json")]
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest data)
        {
            var result = await _userService.LoginAsync(data);
            return Ok(result);
        }
        
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequest data)
        {
            var result = await _userService.RegisterAsync(data);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Init()
        {
            await _userService.InitAsync();
            return Ok();
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_userService.GetUsers());
        }
        
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            return Ok(_userService.Logout());
        }
    }
}
