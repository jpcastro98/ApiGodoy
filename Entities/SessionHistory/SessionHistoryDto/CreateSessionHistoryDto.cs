using System.ComponentModel.DataAnnotations;

namespace ApiGodoy.Entities.SessionHistory.SessionHistoryDto
{
    public class CreateSessionHistoryDto
    {
        [Required]
        public int UserId { get; set; }
    }
}
