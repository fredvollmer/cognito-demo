using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace cognito_dotnet_angular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WhoAmIController : ControllerBase
    {
        private readonly ILogger<WhoAmIController> _logger;

        public WhoAmIController(ILogger<WhoAmIController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public User Get()
        {
            return UserClaimMapper.BuildUser(HttpContext.User);
        }
    }
}
