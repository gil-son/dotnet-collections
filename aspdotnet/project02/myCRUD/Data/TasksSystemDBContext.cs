using Microsoft.EntityFrameworkCore;
using myCRUD.Models;

namespace myCRUD.Data
{
    public class TasksSystemDBContext : DbContext
    {
        public TasksSystemDBContext(DbContextOptions<TasksSystemDBContext> options)
            : base(options)
        {   
        }

        public DbSet<UserModel> Users {get; set;}
        public DbSet<TaskModel> Tasks {get; set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}