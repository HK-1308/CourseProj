﻿using CourseProj.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public DbSet<Item> Item { get; set; }

        public DbSet<Like> Like { get; set; }

        public DbSet<Comment> Comment { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Role>().HasData(new Role[] { new Role { ID = 1, Name = "admin" }, new Role { ID = 2, Name = "user" } });
            modelBuilder.Entity<User>().HasData(new User[] { new User { ID = 69, Email = "admin@mail.ru", Password = "123456", RoleId = 1, Unblocked = true } });         
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))  
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            base.OnModelCreating(modelBuilder);
        }
    }
}
