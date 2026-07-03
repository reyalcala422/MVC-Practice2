using Microsoft.EntityFrameworkCore;
using MyPractice2.Model;


namespace MyPractice2.Data
{
    public class APIDbContext:DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext>options) :base(options){ }

        public DbSet<User> Users { get; set; }       

    }
}
