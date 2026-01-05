using System.Collections.Generic;
using UserManagement.Lib.Enums;

namespace UserManagement.Lib.Models
{
    public class UserRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UserPermissionFlag UserPermissionFlag { get; set; }
        public EntityStatus Status { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}