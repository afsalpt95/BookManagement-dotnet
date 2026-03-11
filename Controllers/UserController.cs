using BookManagement.DTOs.UserDTOs;
using BookManagement.Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace BookManagement.Controllers
{

    [ApiController]
    [Route("")]
    public class UserController : ControllerBase
    {

        private readonly UserService _service;

        public UserController(UserService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.RegisterUser(dto);

            if (!result.Success)
            {
                return BadRequest(new { message = result.Message });
            }

            return Ok(new { message = result.Message });

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ( success , data , message) = await _service.LoginUser(loginDto);

            if (!success)
            {
                return Unauthorized(new { message = message });
            }

            return Ok(new
            {
                message = message,
                data = data
            });
        }


    }
}
