using System;
namespace UserManagement.Lib.Enums
{
    [Flags]
    public enum UserPermissionFlag
    {
        None = 0,
        Read = 1,
        Write = 2,
        Delete = 4,
        Admin = 8
    }
}