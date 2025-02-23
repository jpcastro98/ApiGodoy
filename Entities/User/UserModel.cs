using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiGodoy.Entities.SessionHistory;
using ApiGodoy.Entities.UserData;

namespace ApiGodoy.User
{
    public class UserModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress] 
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public UserDataModel UserData { get; set; } = null!;
        public virtual ICollection<SessionHistoryModel> SessionHistory { get; set; }
    }
}
