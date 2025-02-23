using System.ComponentModel.DataAnnotations;
using ApiGodoy.Entities.UserData.UserDataDto;

namespace ApiGodoy.Entities.User.UserDto
{
    public class CreateUserDto
    {

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        [MaxLength(50, ErrorMessage = "La contraseña no puede tener más de 50 caracteres.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$",
        ErrorMessage = "La contraseña debe contener al menos una mayúscula, una minúscula, un número y un carácter especial.")]

        public string Password { get; set; }

        public CreateUserDataDto UserData { get; set; }
    }
}
