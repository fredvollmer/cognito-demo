using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
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
        private string getClaimName(string name)
        {
            return $"applicationuser:{name}";
        }

        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TerriotyId { get; set; }
        public string Role { get; set; }
        public bool IsEnabled { get; set; }

        public IEnumerable<Claim> ToClaims()
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(this.getClaimName(nameof(Id)), this.Id));
            claims.Add(new Claim(this.getClaimName(nameof(Email)), this.Email));
            claims.Add(new Claim(this.getClaimName(nameof(FirstName)), this.FirstName));
            claims.Add(new Claim(this.getClaimName(nameof(LastName)), this.LastName));
            claims.Add(new Claim(this.getClaimName(nameof(TerriotyId)), this.TerriotyId));
            claims.Add(new Claim(this.getClaimName(nameof(Role)), this.Role));
            claims.Add(new Claim(this.getClaimName(nameof(IsEnabled)), this.IsEnabled.ToString()));
            return claims;
        }
    }
}