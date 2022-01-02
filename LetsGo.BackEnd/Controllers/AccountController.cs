using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Common;
using LetsGo.BackEnd.Services;
using Mawid.BackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PlusAction.BackEnd.Common;

namespace LetsGo.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AccountController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(model);

                if (result.Result)
                    return Ok(result);

                return BadRequest(result);
            }
            return BadRequest("Model is not valid");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUserAsync(model);

                if (!string.IsNullOrEmpty(result.Result))
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest("Some properties are invalid");
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(Guid userId, string token)
        {
            if (User == null || string.IsNullOrEmpty(token))
            {
                return NotFound();
            }

            var result = await _userService.ConfirmEmailAsync(userId, token);
            if (result.Result)
            {
                return Redirect($"{_configuration["AppUrl"]}/ConfirmEmail.html");
            }
            return BadRequest(result);
        }

        [AllowAnonymous]
        [HttpGet("profileImage/{userId}")]
        public IActionResult ImageAsync(Guid userId)
        {
            var user =  _userService.GetUser(userId);
            string path = user.ImageURL;
            string contentType = user.ImageContentType;
            var image = System.IO.File.OpenRead(path);
            return File(image, contentType);
        }

        [HttpPost("updatePhoto")]
        [Authorize]
        public async Task<IActionResult> UpdatePhoto([FromBody] UploadedDocument uploadedDocument)
        {
            Guid? userId = Guid.Parse(User.Identity.GetUserId());

            var result = await _userService.UpdateProfilePictureAsync(userId, uploadedDocument);
            if (result.Result)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
