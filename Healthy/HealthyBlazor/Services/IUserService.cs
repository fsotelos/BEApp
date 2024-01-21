using HealthyBlazor.Model;

namespace HealthyBlazor.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();

        Task<User> GetUserById(int id);

        Task<User> AddUser(User user);

        Task<User> EditUser(User user);

        Task<bool> DeleteUser(int idUser);
    }
}