using API.Models.ACL;

namespace API.InterFace
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(Users users);
    }
}
