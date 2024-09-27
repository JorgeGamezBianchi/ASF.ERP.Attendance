using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

public static class AuthExtendedMethods
{
    public static bool HasRole(this ControllerBase controller, string role)
    {
        bool bFound = false;
        try
        {
            //Check if the requesting user has the specified role...
            bFound = new AuthUser(controller.ControllerContext.HttpContext.User.Identity.Name).HasRole(role);
        }
        catch { }
        return bFound;
    }

    public static bool HasRoles(this ControllerBase controller, string roles)
    {
        bool bFound = false;
        try
        {
            //Check if the requesting user has any of the specified roles...
            //Make sure you separate the roles using ; (ie "Sales Manager;Sales Operator"
            bFound = new AuthUser(controller.ControllerContext.HttpContext.User.Identity.Name).HasRoles(roles);
        }
        catch { }
        return bFound;
    }

    public static bool HasPermission(this ControllerBase controller, string permission)
    {
        bool bFound = false;
        try
        {
            //Check if the requesting user has the specified application permission...
            bFound = new AuthUser(controller.ControllerContext.HttpContext.User.Identity.Name).HasPermission(permission);
        }
        catch { }
        return bFound;
    }

    public static bool IsSysAdmin(this ControllerBase controller)
    {
        bool bIsSysAdmin = false;
        try
        {
            //Check if the requesting user has the System Administrator privilege...
            bIsSysAdmin = new AuthUser(controller.ControllerContext.HttpContext.User.Identity.Name).IsSysAdmin;
        }
        catch { }
        return bIsSysAdmin;
    }


    public static int currentUserId(this ControllerBase controller)
    {
        int currentUserId = 0;
        try
        {
            currentUserId = new AuthUser(controller.ControllerContext.HttpContext.User.Identity.Name).User_Id;
        }
        catch { }
        return currentUserId;
    }
}