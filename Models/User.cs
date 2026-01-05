using UserManagement.Lib.Enums;

namespace UserManagement.Lib.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public EntityStatus Status { get; set; }
        public int UserRoleId { get; set; }
        public virtual UserRole UserRole { get; set; }
    }
}