using System;
using System.Collections.Generic;
using System.Text;
using DataAccesLayer.Enteties;
using Microsoft.EntityFrameworkCore;
using DataAccesLayer.EntetiesConfiguration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace DataAccesLayer.EF
{
    public class AppDBContext : IdentityDbContext<User>
    {
        // Data context, show with which type of objects we will work
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<ForCompare> ForCompares { get; set; }
        public DbSet<Ad> Ads { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Image> Images { get; set; }
        // On creating an instance of ApplicationContext, program check if there is
        // a data base with the name from connection string, and if not create this DB
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            Database.EnsureDeleted(); // delete DB with old diagram
            Database.EnsureCreated(); // create DB with new diagram   
        }

        // Make connection to DB using method UseSqlServer
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = UAHP; Trusted_Connection = True; MultipleActiveResultSets=true");
        //}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new AddConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new FavoriteConfiguration());
            modelBuilder.ApplyConfiguration(new ForCompareConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new ImageConfiguration());
            modelBuilder.ApplyConfiguration(new IdentityRoleConfiguration());

            // Seeding

            var user = new User()
            {
                Name = "Mike",
                Surname = "Grilish",
                PhoneNumber = "0114422545",
                Email = "mgrislish@gmail.com",
                PasswordHash = "testPas_word",
                PhoneNumberConfirmed = false,
                EmailConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnabled = true,
                LockoutEnd = DateTime.Now,
                AccessFailedCount = 0,
                UserName = "MikeGrislish"
            };

            modelBuilder.Entity<User>().HasData(user);

            var ad = new Ad()
            {
                ID = 1,
                OwnerId = user.Id,
                Price = 200000,
                Region = "Chernivtsy",
                District = "Chernivtsy",
                City = "Chernivtsy",
                Street = "Maidan",
                HouseNumber = "47B",
                HouseType = "Дім",
                AreaOfHouse = 344,
                FloorAmount = 2,
                RoomNumber = 5,
                HouseYear = 2014,
                Pool = false,
                Balkon = true,
                PurchaseOportunity = true,
                Status = true,
                Description = "It is a house from Chernivtsy"
            };

            modelBuilder.Entity<Ad>().HasData(ad);

            modelBuilder.Entity<Tag>().HasData(
                new Tag("tag1"),
                new Tag("tag2"),
                new Tag("tag3"));
        }
    }
}
