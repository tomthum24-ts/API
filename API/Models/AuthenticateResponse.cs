namespace API.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(UserViewModel user, string token)
        {
            Id = user.ID;
            FirstName = user.Name;
            Username = user.UserName;
            Token = token;
        }
    }
}
