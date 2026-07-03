using Microsoft.EntityFrameworkCore;
using MyPractice2.Model;


namespace MyPractice2.Data
{
    public class APIDbContext:DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext>options) :base(options){ }

        public DbSet<User> Users { get; set; }      
        
        public DbSet<Color> Colors { get; set; }
        public DbSet<UserColor> UserColors { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserColor>()
            .HasKey(x=>new{
            x.UserId,
            x.ColorId
            });

            modelBuilder.Entity<UserColor>()
            .HasOne(x => x.User)
            .WithMany(c => c.UserColors)
            .HasForeignKey(x => x.UserId);


            modelBuilder.Entity<UserColor>()
             .HasOne(x => x.Color)
             .WithMany(x=>x.UserColors)
             .HasForeignKey(x => x.ColorId);
        }

    }
}
