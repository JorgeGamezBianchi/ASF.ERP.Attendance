using System;
using System.Web.Mvc;

public static class StringExtensions
{
    public static bool WordCount(this ControllerBase controller, string role)
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
}