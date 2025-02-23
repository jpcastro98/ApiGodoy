using System.ComponentModel.DataAnnotations;

namespace ApiGodoy.Entities.UserData.UserDataDto
{
    public class ResultUserDataDto
    {
        public int Id { get; set; }

        public string IdentificationNumber { get; set; }

        public string Names { get; set; } = null!;

        public string LastNames { get; set; } = null!;

        public int Score { get; set; }
    }
}
