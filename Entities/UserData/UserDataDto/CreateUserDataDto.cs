using System.ComponentModel.DataAnnotations;

namespace ApiGodoy.Entities.UserData.UserDataDto
{
    public class CreateUserDataDto
    {
        [Required(ErrorMessage = "El número de documento es obligatorio")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "El número de documento solo puede números")]
        public string IdentificationNumber { get; set; } = null!;

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios")]
        public string Names { get; set; } = null!;

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(100, ErrorMessage = "El apellido no puede tener más de 100 caracteres")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$", ErrorMessage = "El apellido solo puede contener letras y espacios")]
        public string LastNames { get; set; } = null!;

        [Required(ErrorMessage = "El ID de usuario es obligatorio")]
        public int UserId { get; set; }
    }
}
