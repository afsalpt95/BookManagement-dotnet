using BookManagement.DTOs.UserDTOs;
using BookManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.Controllers
{

    [ApiController]
    [Route("api")]
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

    }
}
