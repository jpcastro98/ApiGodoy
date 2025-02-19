using ApiGodoy.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiGodoy.Database
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {   
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserData> UsersData { get; set; }
    
    }
}
