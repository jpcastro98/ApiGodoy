using System.ComponentModel.DataAnnotations;
using ApiGodoy.Entities.SessionHistory.SessionHistoryDto;
using ApiGodoy.Entities.UserData.UserDataDto;

namespace ApiGodoy.Entities.User.UserDto
{
    public class ResultUserDto
    {   
        public int Id { get; set; }
        public string Email { get; set; }

        public ResultUserDataDto UserData { get; set; }

        public ResultSessionHistoryDto SessionHistory { get; set; }
    }
}
