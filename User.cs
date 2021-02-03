using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace cognito_dotnet_angular
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=cognito-demo.db");
    }

    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TerriotyId { get; set; }
        public string Role { get; set; }
        public bool IsEnabled { get; set; }
    }
}