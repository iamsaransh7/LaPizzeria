using LaPizzeria.Core.Entities;
using LaPizzeria.Models;

namespace LaPizzeria.Services.Interfaces
{
    public interface IAuthService: IService<User>
    {
        bool CreateUser(User user, string Role);
        UserModel ValidateUser(string email, string password);
    }
}
