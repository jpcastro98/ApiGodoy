﻿using System.ComponentModel.DataAnnotations;

namespace ApiGodoy.Entities.User.UserDto
{
    public class AuthUserDto
    {
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public string Password { get; set; }

    }
}
