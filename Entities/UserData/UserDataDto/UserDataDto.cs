using System.ComponentModel.DataAnnotations;

namespace ApiGodoy.Entities.UserData.UserDataDto
{
    public class UserDataDto: CreateUserDataDto
    {
       public int? Id { get; set; }
       public int? Score { get; set; }
    }
}
