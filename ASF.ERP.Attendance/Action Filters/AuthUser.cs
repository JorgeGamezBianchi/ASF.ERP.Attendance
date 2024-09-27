using ASF.ERP.Attendance.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

public class AuthUser
{
    public int User_Id { get; set; }
    public bool IsSysAdmin { get; set; }
    public string Username { get; set; }
    private List<UserRole> Roles = new List<UserRole>();

    public AuthUser(string _username)
    {
        this.Username = _username;
        this.IsSysAdmin = false;
        //GetDatabaseUserRolesPermissions();
    }

    //private void GetDatabaseUserRolesPermissions()
    //{
    //    using (ASF_APIEntities _data = new ASF_APIEntities())
    //    {
    //        Users _user = _data.Users.Where(u => u.Username == this.Username).FirstOrDefault();
    //        if (_user != null)
    //        {
    //            this.User_Id = _user.UserId;
    //            foreach (Roles _role in _user.Roles)
    //            {
    //                UserRole _userRole = new UserRole { Role_Id = _role.RoleId, RoleName = _role.RoleName };
    //                foreach (Permissions _permission in _role.Permissions)
    //                {
    //                    _userRole.Permissions.Add(new RolePermission { Permission_Id = _permission.PermissionId, PermissionDescription = _permission.PermissionDescription });
    //                }
    //                this.Roles.Add(_userRole);

    //                if (!this.IsSysAdmin)
    //                    this.IsSysAdmin = _role.IsSysAdmin;
    //            }
    //        }
    //    }
    //}

    public bool HasPermission(string requiredPermission)
    {
        bool bFound = false;
        foreach (UserRole role in this.Roles)
        {
            bFound = (role.Permissions.Where(p => p.PermissionDescription.ToLower() == requiredPermission.ToLower()).ToList().Count > 0);
            if (bFound)
                break;
        }
        return bFound;
    }

    public bool HasRole(string role)
    {
        return (Roles.Where(p => p.RoleName == role).ToList().Count > 0);
    }

    public bool HasRoles(string roles)
    {
        bool bFound = false;
        string[] _roles = roles.ToLower().Split(';');
        foreach (UserRole role in this.Roles)
        {
            try
            {
                bFound = _roles.Contains(role.RoleName.ToLower());
                if (bFound)
                    return bFound;
            }
            catch (Exception)
            {
            }
        }
        return bFound;
    }

    public List<RolePermission> GetPermissions()
    {
        List<RolePermission> list = new List<RolePermission>();
        foreach (var role in Roles)
        {
            list.AddRange(role.Permissions.ToList());
        }
        return list;
    }
}

public class UserRole
{
    public int Role_Id { get; set; }
    public string RoleName { get; set; }
    public List<RolePermission> Permissions = new List<RolePermission>();
}

public class RolePermission
{
    public int Permission_Id { get; set; }
    public string PermissionDescription { get; set; }
}