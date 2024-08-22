using LaPizzeria.Core.Entities;
using LaPizzeria.Models;
using LaPizzeria.Repositories.Interfaces;
using LaPizzeria.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaPizzeria.Services.Implementation
{
    public class AuthService : Service<User>, IAuthService
    {
        IUserRepository _userRepo;
        public AuthService(IUserRepository repository): base(repository) 
        {
            _userRepo = repository;
        }

        public bool CreateUser(User user, string Role)
        {
            return _userRepo.CreateUser(user, Role);
        }

        public UserModel ValidateUser(string email, string password)
        {
            return _userRepo.ValidateUser(email, password);
        }
    }
}
