using myCRUD.Models;
using myCRUD.Repositories.Interfaces;
using myCRUD.Data;
using Microsoft.EntityFrameworkCore;

namespace myCRUD.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly TasksSystemDBContext _dbContext;
        public UserRepository(TasksSystemDBContext tasksSystemDBContext)
        {
            _dbContext = tasksSystemDBContext;
        }

        Task<UserModel> IUserRepository.addUser(UserModel user)
        {
            throw new NotImplementedException();
        }

        Task<bool> IUserRepository.Delete(int id)
        {
            throw new NotImplementedException();
        }

        async Task<List<UserModel>> IUserRepository.getAllUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        async Task<UserModel> IUserRepository.getUserById(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        Task<UserModel> IUserRepository.updateUser(UserModel user, int id)
        {
            throw new NotImplementedException();
        }
    }
}