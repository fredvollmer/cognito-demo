using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace cognito_dotnet_angular.Middlewares
{
    public class AddApplicationUser
    {
        private readonly RequestDelegate next;

        public AddApplicationUser(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var usernameClaim = context.User.Claims.ToList().Find(c => c.Type == "username");
            if (usernameClaim != null)
            {
                using (var db = new UserContext())
                {
                    User user = await db.Users.FindAsync(usernameClaim.Value);
                    context.User.AddIdentity(new ClaimsIdentity(UserClaimMapper.BuildClaims(user)));
                }
            }
            await next(context);
        }
    }
}