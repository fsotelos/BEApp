using Healthy.Business.Commands;
using Healthy.Domain.Entities;
using Healthy.Infraestructure.Repository.Definitions;
using Microsoft.EntityFrameworkCore;

namespace Healthy.Infraestructure.Repository.Implementations
{
    public class UserRepository : IUserRepostory
    {
        HealthyDbContext _db;
        public UserRepository(HealthyDbContext db)
        {
            _db = db;
        }
        public async Task CreateUser(RegisterUserCommand registerUserCommand)
        {
            _db.Users.Add(registerUserCommand as User);
            await _db.SaveChangesAsync();
        }

        public async  Task DeleteUser(User user)
        {
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
        }

        public User GetUserById(int id)
        {
            return _db.Users.FirstOrDefault(x => x.Id == id)!;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task UpdateUser(EditUserCommand editUserCommand)
        {
            _db.Users.Update(editUserCommand);
            await _db.SaveChangesAsync();
        }
    }
}
