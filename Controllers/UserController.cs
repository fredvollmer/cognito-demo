using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace cognito_dotnet_angular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private static AmazonCognitoIdentityProviderClient _cognitoClient = new AmazonCognitoIdentityProviderClient();

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> Create(CreateUserRequest req)
        {
            Task dbInsertTask;
            var cognitoCreationTask = _cognitoClient.AdminCreateUserAsync(new AdminCreateUserRequest
            {
                UserPoolId = "us-east-1_wi3kBOkom",
                Username = req.Id,
                UserAttributes = new List<AttributeType>
                {
                    new() {Name = "email", Value = req.Email},
                }
            });

            using (var db = new UserContext())
            {
                db.Add<User>(map(req));
                dbInsertTask = db.SaveChangesAsync();
            }

            await dbInsertTask;
            await cognitoCreationTask;

            return Created(nameof(Get), new { userId = req.Id });
        }

        private User map(CreateUserRequest source)
        {
            return new User
            {
                Id = source.Id,
                FirstName = source.FirstName,
                LastName = source.LastName,
                TerriotyId = source.TerriotyId,
                Role = source.Role,
                IsEnabled = true,
            };
        }

        [HttpGet]
        [Route("{userId}")]
        //[Authorize]
        public async Task<User> Get(String userId)
        {
            using (var db = new UserContext())
            {
                return await db.Users.FindAsync(userId);
            }
        }

        [HttpPost]
        [Route("{userId}/enable")]
        //[Authorize]
        public async Task<IActionResult> Enable(String userId)
        {
            var cognitoTask = _cognitoClient.AdminEnableUserAsync(new AdminEnableUserRequest
            {
                UserPoolId = "us-east-1_wi3kBOkom",
                Username = userId,
            });

            _logger.LogInformation($"Enable {userId}");

            Task dbInsertTask;
            using (var db = new UserContext())
            {
                var user = await db.Users.FindAsync(userId);
                user.IsEnabled = true;
                dbInsertTask = db.SaveChangesAsync();
            }

            await dbInsertTask;
            await cognitoTask;

            return NoContent();
        }

        [HttpPost]
        [Route("{userId}/disable")]
        //[Authorize]
        public async Task<IActionResult> Disable(String userId)
        {
            // Task dbInsertTask;
            var cognitoTask = _cognitoClient.AdminDisableUserAsync(new AdminDisableUserRequest
            {
                UserPoolId = "us-east-1_wi3kBOkom",
                Username = userId,
            });

            _logger.LogInformation($"Disable {userId}");

            // using (var db = new UserContext())
            // {
            //     db.Add(req);
            //     dbInsertTask = db.SaveChangesAsync();
            // }

            // await dbInsertTask;
            await cognitoTask;

            return NoContent();
        }

        [HttpDelete]
        [Route("{userId}")]
        //[Authorize]
        public async Task<IActionResult> Delete(String userId)
        {
            // Task dbInsertTask;
            var cognitoTask = _cognitoClient.AdminDeleteUserAsync(new AdminDeleteUserRequest
            {
                UserPoolId = "us-east-1_wi3kBOkom",
                Username = userId,
            });

            _logger.LogInformation($"Deleted {userId}");

            // using (var db = new UserContext())
            // {
            //     db.Add(req);
            //     dbInsertTask = db.SaveChangesAsync();
            // }

            // await dbInsertTask;
            await cognitoTask;

            return NoContent();
        }
    }
}
