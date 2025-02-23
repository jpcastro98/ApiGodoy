using ApiGodoy.Entities.SessionHistory;
using ApiGodoy.Entities.UserData;
using ApiGodoy.User;
using Microsoft.EntityFrameworkCore;

namespace ApiGodoy.Database
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<UserDataModel> UsersData { get; set; }
        public DbSet<SessionHistoryModel> SessionHistory { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /*modelBuilder.Entity<UserModel>().HasData(new UserModel
            {
                email = "admin@example.com",
                password = "Admin123!",
                userData = new UserDataModel {
                    names = "Administrador",
                    lastNames = "Apellido Admin",
                    identificationNumber = 1234567

                },
                CreatedAt = DateTime.UtcNow
            });*/
        }

    }
}
