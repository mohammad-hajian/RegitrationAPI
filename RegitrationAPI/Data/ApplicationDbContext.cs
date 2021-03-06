﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RegitrationAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegitrationAPI.Data
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<UserTokenValidation> UserTokenValidations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Seed
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Leader",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    NormalizedName = "LEADER"
                }, new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Admin",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Manager",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    NormalizedName = "MANAGER"
                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "User",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    NormalizedName = "USER"
                });
            #endregion

            #region Lazy Loading
            builder.Entity<ApplicationUser>().Property(date => date.RegisterDate).HasDefaultValueSql("GETDATE()");
            builder.Entity<ApplicationUser>().Property(date => date.FirstName).HasDefaultValueSql("''");
            builder.Entity<ApplicationUser>().Property(date => date.LastName).HasDefaultValueSql("''");
            #endregion

            #region Relationships
            builder.Entity<UserTokenValidation>().HasOne(b => b.User).WithMany(b => b.UserTokenValidations).OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Change Name
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");
            builder.Entity<IdentityRole>().ToTable("Role");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");
            builder.Entity<ApplicationUser>().ToTable("User");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserToken");
            #endregion
        }
    }
}
