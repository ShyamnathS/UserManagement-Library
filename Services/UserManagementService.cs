using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using UserManagement.Lib.Models;
using UserManagement.Lib.Enums;
using UserManagement.Lib.Helpers;

namespace UserManagement.Lib.Services
{
    public class UserManagementService
    {
        private readonly UserDbContext _db = new UserDbContext();

        // ================= ROLE CRUD =================
        public void CreateRole(string name, UserPermissionFlag perms, EntityStatus status)
        {
            _db.UserRoles.Add(new UserRole { Name = name, UserPermissionFlag = perms, Status = status });
            _db.SaveChanges();
        }

        public List<UserRole> GetAllRoles() => _db.UserRoles.ToList();

        public void UpdateRole(int id, string newName, UserPermissionFlag newPerms, EntityStatus newStatus)
        {
            var role = _db.UserRoles.Find(id);
            if (role != null)
            {
                role.Name = newName;
                role.UserPermissionFlag = newPerms;
                role.Status = newStatus;
                _db.SaveChanges();
            }
        }

        // ================= USER CRUD =================
        public void CreateUser(string username, string password, int roleId, EntityStatus status)
        {
            var (hash, salt) = PasswordHasher.HashPassword(password);
            _db.Users.Add(new User { UserName = username, Password = hash, Salt = salt, UserRoleId = roleId, Status = status });
            _db.SaveChanges();
        }

        public List<User> GetAllUsers() => _db.Users.Include(u => u.UserRole).ToList();

        public void UpdateUser(int id, string newUsername, int newRoleId, EntityStatus newStatus)
        {
            var user = _db.Users.Find(id);
            if (user != null)
            {
                user.UserName = newUsername;
                user.UserRoleId = newRoleId;
                user.Status = newStatus;
                _db.SaveChanges();
            }
        }

        // "Soft Delete" - Mentor asked for Status options, so we don't remove from DB
        public void DeleteUser(int id)
        {
            var user = _db.Users.Find(id);
            if (user != null)
            {
                user.Status = EntityStatus.Inactive;
                _db.SaveChanges();
            }
        }

        public User Login(string username, string password)
        {
            var user = _db.Users.Include(u => u.UserRole)
                        .FirstOrDefault(u => u.UserName == username && u.Status == EntityStatus.Available);
            if (user == null) return null;
            return PasswordHasher.VerifyPassword(password, user.Salt, user.Password) ? user : null;
        }
    }
}