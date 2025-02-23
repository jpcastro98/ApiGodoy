using System.ComponentModel.DataAnnotations;
using ApiGodoy.User;

namespace ApiGodoy.Entities.SessionHistory
{
    public class SessionHistoryModel
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }

        public UserModel User {  get; set; } 
        public DateTime LastLogin { get; set; } = DateTime.UtcNow;

    }
}
