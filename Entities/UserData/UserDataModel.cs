using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiGodoy.Entities.SessionHistory;
using ApiGodoy.User;

namespace ApiGodoy.Entities.UserData
{
    public class UserDataModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        public string IdentificationNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string Names { get; set; }

        [Required]
        [StringLength(100)]
        public string LastNames { get; set; }

        public int Score { get; set; }
        public int UserId { get; set; }
        public UserModel User { get; set; } = null!;

        
    }
}
