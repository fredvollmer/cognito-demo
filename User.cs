using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
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

        public IEnumerable<Claim> ToClaims()
        {
            // string jsonUser = JsonSerializer.Serialize(this);
            // Dictionary<string, string> userFields = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonUser);

            // var claims = new List<Claim>();
            // foreach (var name in userFields)
            // {
            //     claims.Add(new Claim($"applicationuser:{name.Key}", name.Value));
            // }

            // return claims;

            return new List<Claim> {
                new (this.getClaimName(nameof(Id)), this.Id),
                new (this.getClaimName(nameof(Email)), this.Email != null ? this.Email:""),
                new (this.getClaimName(nameof(FirstName)), this.FirstName),
                new (this.getClaimName(nameof(LastName)), this.LastName),
                new (this.getClaimName(nameof(TerriotyId)), this.TerriotyId),
                new (this.getClaimName(nameof(IsEnabled)), this.IsEnabled.ToString()),
                
                // Role claim has a specific type that allows it to be used in statements such as 
                //  [Authorize(Role = "admin")]
                new Claim(ClaimTypes.Role, this.Role),
            };
        }

        private string getClaimName(string name)
        {
            return $"applicationuser:{name}";
        }
    }
}