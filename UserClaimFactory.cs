using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace cognito_dotnet_angular
{
    public static class UserClaimFactory
    {
        public static User BuildUser(ClaimsPrincipal principal)
        {
            // Ensure an application user has been stored on this ClaimsPrincipal
            if (!principal.HasClaim(c => c.Type == User.ClaimType.Id))
            {
                throw new InvalidOperationException("ClaimsPrincipal does not have required claims for conversion to an Application User");
            }

            var claims = principal.Claims.ToDictionary(c => c.Type);

            return new User
            {
                Id = claims[User.ClaimType.Id].Value,
                Email = claims[User.ClaimType.Email].Value,
                FirstName = claims[User.ClaimType.FirstName].Value,
                LastName = claims[User.ClaimType.LastName].Value,
                TerriotyId = claims[User.ClaimType.TerritoryId].Value,
                Role = claims[User.ClaimType.Role].Value,
                IsEnabled = bool.Parse(claims[User.ClaimType.IsEnabled].Value),
            };
        }

        public static IEnumerable<Claim> BuildClaims(User user)
        {
            return new List<Claim>
            {
                new Claim(User.ClaimType.Id, user.Id),
                new Claim(User.ClaimType.Email, user.Email != null ? user.Email:""),
                new Claim(User.ClaimType.FirstName, user.FirstName),
                new Claim(User.ClaimType.LastName, user.LastName),
                new Claim(User.ClaimType.TerritoryId, user.TerriotyId),
                new Claim(User.ClaimType.IsEnabled, user.IsEnabled.ToString()),
                
                // Role claim has a specific type that allows it to be used in statements such as 
                //  [Authorize(Role = "admin")]
                // We also include in a namespaced claim. This is needed for conversion.
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(User.ClaimType.Role, user.Role),
            };
        }
    }
}