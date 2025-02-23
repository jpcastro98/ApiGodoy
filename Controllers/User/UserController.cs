using ApiGodoy.Entities.User.UserDto;
using ApiGodoy.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiGodoy.Controllers.User
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;


        public UserController(UserService userService)
        {
            _userService = userService;

        }
        // GET: api/user
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        
        public async Task<IActionResult> Get()
        {
            return Ok(await _userService.GetAll());
        }

        // GET api/user/5
        [HttpGet("{id}")]
        [Authorize]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/user
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Post([FromBody] CreateUserDto createUserDto)
        {
            var result = await _userService.Add(createUserDto);
            return Ok(new { message = "Usuario creado existosamente", user = result
            });
        }

        // PUT api/user/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateUserDto userDto)
        {
            if (userDto == null || id <= 0) return BadRequest("Datos inválidos.");
            var result = await _userService.Update(id, userDto);
            return Ok(new { message = result });
        }


        // DELETE api/user/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.Delete(id);
            if (!result) return NotFound($"Usuario con ID {id} no encontrado.");
            return Ok(new {message = "Usuario eliminado exitosamente"});
        }
    }
}
