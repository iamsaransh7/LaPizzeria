using LaPizzeria.Core;
using LaPizzeria.Core.Entities;
using LaPizzeria.Models;
using LaPizzeria.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace LaPizzeria.Repositories.Implementation
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext db): base(db) { }

        public bool CreateUser(User user, string Role)
        {
            try
            {
                Role role = _db.Roles.Where(r => r.Name == Role).FirstOrDefault();

                if (role != null) 
                {
                    user.Password = BC.HashPassword(user.Password);
                    user.Roles.Add(role);

                    _db.Users.Add(user);
                    _db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return false;
        }

        public UserModel ValidateUser(string email, string password)
        {
            try
            {
                User user = _db.Users.Include(u => u.Roles).Where(u => u.Email == email).FirstOrDefault();

                if (user != null) 
                {
                    bool isVerified = BC.Verify(password, user.Password);

                    if (isVerified) 
                    {
                        UserModel userModel = new UserModel 
                        {
                            Id = user.Id,
                            Name = user.Name,
                            Email = user.Email,
                            PhoneNumber = user.PhoneNumber,
                            Roles = user.Roles.Select(r => r.Name).ToArray()
                        };

                        return userModel;
                    }
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
