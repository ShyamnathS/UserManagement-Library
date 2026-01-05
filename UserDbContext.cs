using System.Data.Entity;
using UserManagement.Lib.Models;

namespace UserManagement.Lib
{
    public class UserDbContext : DbContext
    {
        // This name "UserMgmtDB" must match the App.config later
        public UserDbContext() : base("name=UserMgmtDB") { }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}