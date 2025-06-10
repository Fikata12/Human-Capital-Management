using HCM.Data.Models;
using Microsoft.EntityFrameworkCore;
using static HCM.Common.GeneralApplicationConstants;

namespace HCM.Data
{
    public class HcmDbContext : DbContext
    {
        public HcmDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Person> People { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UsersRoles { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Department>().HasQueryFilter(e => !e.IsDeleted);

            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<User>()
                .HasOne(u => u.Person)
                .WithOne(p => p.User)
                .HasForeignKey<User>(u => u.PersonId);

            modelBuilder.Entity<Person>()
                .HasOne(p => p.Department)
                .WithMany(d => d.People)
                .HasForeignKey(p => p.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Department>()
                .HasOne(d => d.Manager)
                .WithMany()
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = Guid.Parse("84ba831a-edd5-49b6-93a3-a4eb05227866"), Name = EmployeeRoleName },
                new Role { Id = Guid.Parse("dd84c620-3b80-4591-acef-0cca6f91278f"), Name = ManagerRoleName },
                new Role { Id = Guid.Parse("b2198aa7-72a0-4b1f-9065-3f1cb18d26a1"), Name = HRAdminRoleName }
            );
        }
    }
}
