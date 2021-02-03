namespace cognito_dotnet_angular.Controllers
{
    public class CreateUserRequest
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TerriotyId { get; set; }
        public string Role { get; set; }
    }
}