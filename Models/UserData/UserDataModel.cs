using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiGodoy.Models
{
    public class UserData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string IdentificationNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string Names { get; set; }

        [Required]
        [StringLength(100)]
        public string LastNames { get; set; }
       
        public int UserId { get; set; }
        public User User { get; set; } = null!;

    }
}
