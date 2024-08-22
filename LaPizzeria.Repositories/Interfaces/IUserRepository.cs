using LaPizzeria.Core.Entities;
using LaPizzeria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaPizzeria.Repositories.Interfaces
{
    public interface IUserRepository: IRepository<User>
    {
        bool CreateUser(User user, string Role);
        UserModel ValidateUser(string email, string password);
    }
}
