using ApiGodoy.Entities.User.UserDto;
using ApiGodoy.Entities.UserData;
using ApiGodoy.Entities.UserData.UserDataDto;
using ApiGodoy.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiGodoy.Controllers
{
    [Route("api/user-data")]
    [ApiController]
    public class UserDataController : ControllerBase
    {
        private readonly UserDataService  _userDataService;


        public UserDataController(UserDataService userDataService)
        {
            _userDataService = userDataService;
            
        }
        // GET: api/<ValuesController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userDataService.GetAll());
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _userDataService.GetById(id));
        }

        // POST api/<ValuesController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Post([FromBody] CreateUserDataDto createUserDataDto)
        {
            var result = await _userDataService.Add(createUserDataDto);
            if (result == null) return Conflict(new { message = "El usuario ya existe." });
            return Ok(result);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}/update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateUserDataDto userDataDto)
        {
            var result = await _userDataService.Update(id, userDataDto);
            if (result == null) return Conflict(new { message = "No se pudo actualizar el usuario." });
            return Ok(result);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}/delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await _userDataService.Delete(id);
            return NoContent();
        }
    }
}
