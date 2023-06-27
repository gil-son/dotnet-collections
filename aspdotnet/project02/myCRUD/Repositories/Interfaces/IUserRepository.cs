using myCRUD.Models;

namespace myCRUD.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserModel>> getAllUsers();
        Task<UserModel> getUserById(int id);
        Task<UserModel> addUser(UserModel user);
        Task<UserModel> updateUser(UserModel user, int id);
        Task<bool> Delete(int id);
    }
}