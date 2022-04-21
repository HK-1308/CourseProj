using CourseProj.Data.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CourseProj.Data
{
    public class DBContent: DbContext
    {
        public DBContent(DbContextOptions<DBContent> options):base(options)
        { }

        public DbSet<User> User { get; set; }

        public DbSet<Role> Role { get; set; }

        public DbSet<Collection> Collection { get; set; }

        public DbSet<Image> Image { get; set; }

        public DbSet<Item> Item { get; set; }

        public DbSet<Like> Like { get; set; }

        public DbSet<Comment> Comment { get; set; } 

        public DbSet<Tag> Tag { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            var salt = new byte[64 / 8];
            rng.GetBytes(salt);
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: "root",
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 128 / 8));


            modelBuilder.Entity<Role>().HasData(new Role[] { new Role { ID = 1, Name = "admin" }, new Role { ID = 2, Name = "user" } });
            modelBuilder.Entity<User>().HasData(new User[] { new User { ID = 1, Email = "root", Password = hashed, RoleId = 1, Unblocked = true,salt = salt } });         
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))  
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            base.OnModelCreating(modelBuilder);
        }
    }
}
