using System;
using Microsoft.EntityFrameworkCore;
using Recipe.Data.Auth;

namespace Recipe.Data
{
    public class AuthContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<RolePermission> RolePermissions { get; set; }
        
        public AuthContext(DbContextOptions<RecipeContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash)
                .HasMaxLength(50);
            modelBuilder.Entity<User>()
                .Property(u => u.Login)
                .HasMaxLength(200);
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);
            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    Id = Guid.NewGuid(),
                    Login = "test@gmail.com",
                    PasswordHash = "SomePasswordHash",
                    RoleId = 1
                });

            modelBuilder.Entity<UserRole>()
                .HasKey(r => r.Id);
            modelBuilder.Entity<UserRole>()
                .Property(r => r.Name)
                .HasMaxLength(200);
            modelBuilder.Entity<UserRole>()
                .HasMany<RolePermission>()
                .WithOne()
                .HasForeignKey(p => p.RoleId);
            modelBuilder.Entity<UserRole>()
                .HasData(new UserRole
                {
                    Id = 1,
                    Name = "Administrator"
                }, new UserRole
                {
                    Id = 2,
                    Name = "Newby"
                }, new UserRole
                {
                    Id = 3,
                    Name = "Master of cooking"
                });

            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new
                {
                    rp.RoleId,
                    rp.PermissionId
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}