using Microsoft.EntityFrameworkCore;
using MyPractice2.Model;
using MyPractice2.Model.Brand;


namespace MyPractice2.Data
{
    public class APIDbContext:DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext>options) :base(options){ }

        public DbSet<User> Users { get; set; }      
        
        public DbSet<Color> Colors { get; set; }
        public DbSet<UserColor> UserColors { get; set; }


        public DbSet<UserBrand> UserBrands { get; set; }
        public DbSet<Brand> Brands { get; set; }            




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




            modelBuilder.Entity<UserBrand>()
            .HasKey(x => new {
            x.UserBrandId,
            x.BrandId
            });

            modelBuilder.Entity<UserBrand>()
            .HasOne(x => x.UserIdBrand)
            .WithMany(c => c.UserBrands)
            .HasForeignKey(x=>x.UserBrandId);

            modelBuilder.Entity<UserBrand>()
            .HasOne(x => x.Brand)
            .WithMany(c=>c.UserBrands)
            .HasForeignKey(x=>x.BrandId);
            ;
        }

    }
}
