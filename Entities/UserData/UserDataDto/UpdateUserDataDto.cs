using System.ComponentModel.DataAnnotations;
using ApiGodoy.Entities.UserData.UserDataDto;

namespace ApiGodoy.Entities.User.UserDto
{
    public class UpdateUserDataDto
    {
        [Required(ErrorMessage = "El número de documento es obligatorio")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "El número de documento solo puede números")]
        public string? IdentificationNumber { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios")]
        public string? Names { get; set; } = null!;
        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(100, ErrorMessage = "El apellido no puede tener más de 100 caracteres")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$", ErrorMessage = "El apellido solo puede contener letras y espacios")]
        public string? LastNames { get; set; } = null!;


    }
}
