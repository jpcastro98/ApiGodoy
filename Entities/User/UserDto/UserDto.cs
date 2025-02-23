using System.ComponentModel.DataAnnotations;

namespace ApiGodoy.Entities.User.UserDto
{
    public class UserDto: CreateUserDto
    {
        [Required(ErrorMessage = "El id de el usuario no es obligatorio")]
        public int Id { get; set; }

    }
}
