using KingFashion.Models.Orders;
using KingFashion.Models.Transactions;
using KingFashion.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingFashion
{
    public class KingFashionDBContext : IdentityDbContext<User>
    {
        public KingFashionDBContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Ignore<Order>();
            modelBuilder.Ignore<Transaction>();
            modelBuilder.Ignore<Models.Carts.Cart>();
            SeedingAspNetUser(modelBuilder);
            SeedingAspNetRole(modelBuilder);
            SeedingAspNetUserRole(modelBuilder);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if(tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }    
        }
        private void SeedingAspNetUser(ModelBuilder modelBuilder)
        {
            User user = new User()
            {
                Id = "2c0fca4e-9376-4a70-bcc6-35bebe497866",
                UserName = "kingfashionc0321g1@gmail.com",
                Email = "kingfashionc0321g1@gmail.com",
                NormalizedEmail = "kingfashionc0321g1@gmail.com",
                NormalizedUserName = "kingfashionc0321g1@gmail.com",
                LockoutEnabled = false
            };
            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            var passwordHash = passwordHasher.HashPassword(user, "Asdf1234!");
            user.PasswordHash = passwordHash;

            modelBuilder.Entity<User>().HasData(user);
        }
        private void SeedingAspNetRole(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Id = "c0c6661b-0964-4e62-8083-3cac6a6741ec",
                    Name = "SystemAdmin",
                    NormalizedName = "SystemAdmin",
                    ConcurrencyStamp = "1"
                },
                new IdentityRole()
                {
                    Id = "32ffd287-205f-43a2-9f0d-80sc5309fb47",
                    Name = "Customer",
                    NormalizedName = "Customer",
                    ConcurrencyStamp = "2"
                });
        }
        private void SeedingAspNetUserRole(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "c0c6661b-0964-4e62-8083-3cac6a6741ec",
                    UserId = "2c0fca4e-9376-4a70-bcc6-35bebe497866"
                });
        }
    }
}
