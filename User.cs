using System;
using System.Collections.Generic;
using System.Linq;
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
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TerriotyId { get; set; }
        public string Role { get; set; }
        public bool IsEnabled { get; set; }

        public IEnumerable<Claim> ToClaims()
        {
            return new List<Claim> {
                new (this.getClaimName(nameof(Id)), this.Id),
                new (this.getClaimName(nameof(Email)), this.Email != null ? this.Email:""),
                new (this.getClaimName(nameof(FirstName)), this.FirstName),
                new (this.getClaimName(nameof(LastName)), this.LastName),
                new (this.getClaimName(nameof(TerriotyId)), this.TerriotyId),
                new (this.getClaimName(nameof(IsEnabled)), this.IsEnabled.ToString()),
                
                // Role claim has a specific type that allows it to be used in statements such as 
                //  [Authorize(Role = "admin")]
                // We also include in a namespaced claim. This is needed for conversion.
                new Claim(ClaimTypes.Role, this.Role),
                new Claim(getClaimName(nameof(Role)), this.Role),
            };
        }

        private string getClaimName(string name)
        {
            return $"applicationuser:{name}";
        }
    }

    public static class ClaimsPrincipalExtensions
    {
        public static User ApplicationUser(this ClaimsPrincipal principal)
        {
            // Ensure an application user has been stored on this ClaimsPrincipal
            if (!principal.HasClaim(c => c.Type == "applicationuser:Id"))
            {
                throw new InvalidOperationException("ClaimsPrincipal does not have required claims for conversion to an Application User");
            }

            var claims = principal.Claims.ToDictionary(c => c.Type);

            return new User
            {
                Id = claims["applicationuser:Id"].Value,
                Email = claims["applicationuser:Email"].Value,
                FirstName = claims["applicationuser:FirstName"].Value,
                LastName = claims["applicationuser:LastName"].Value,
                TerriotyId = claims["applicationuser:TerritoryId"].Value,
                Role = claims["applicationuser:Role"].Value,
                IsEnabled = bool.Parse(claims["applicationuser:IsEnabled"].Value),
            };
        }
    }
}