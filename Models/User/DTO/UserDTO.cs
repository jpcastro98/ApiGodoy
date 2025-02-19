using System.ComponentModel.DataAnnotations;

namespace ApiGodoy.Models.Dto
{
    public class UserDTO : CreateUserDataDTO
    {
        [Required(ErrorMessage = "El id de el usuario no es obligatorio")]
        public int Id { get; set; }

    }
}
