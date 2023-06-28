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


        async Task<UserModel> IUserRepository.addUser(UserModel user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        async Task<bool> IUserRepository.Delete(int id)
        {
            UserModel userById = await ((IUserRepository)this).getUserById(id);

           if(userById==null)
           {
                throw new Exception($"User from id:{id} don't exist in the databases");
           }

           _dbContext.Users.Remove(userById);
           await _dbContext.SaveChangesAsync();

           return true;
        }

        async Task<List<UserModel>> IUserRepository.getAllUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        async Task<UserModel> IUserRepository.getUserById(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        async Task<UserModel> IUserRepository.updateUser(UserModel user, int id)
        {
           UserModel userById = await ((IUserRepository)this).getUserById(id);

           if(userById==null)
           {
                throw new Exception($"User from id:{id} don't exist in the databases");
           }

           userById.Name = user.Name;
           userById.Email = user.Email;

           _dbContext.Users.Update(userById);
           await _dbContext.SaveChangesAsync();

           return userById;
        }
    }
}