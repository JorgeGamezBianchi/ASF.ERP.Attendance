using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

//[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthAttribute : AuthorizeAttribute
{
    public override void OnAuthorization(AuthorizationContext filterContext)
    {
        if (filterContext.HttpContext.Request.IsAuthenticated == false)
            HandleUnauthorizedRequest(filterContext);
        else
        {
            //Create permission string based on the requested controller name and action name in the format 'controllername-action'
            string requiredPermission = String.Format("{0}-{1}", filterContext.ActionDescriptor.ControllerDescriptor.ControllerName, filterContext.ActionDescriptor.ActionName);
            
            //Create an instance of our custom user authorization object passing requesting user's 'Windows Username' into constructor
            AuthUser requestingUser = new AuthUser(filterContext.RequestContext.HttpContext.User.Identity.Name);
            
            //Check if the requesting user has the permission to run the controller's action
            if (!requestingUser.HasPermission(requiredPermission) & !requestingUser.IsSysAdmin)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                    filterContext.Result = new JsonResult { Data = "Controller-UnauthorizedRequest", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                else
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "action", "Unauthorized" }, { "controller", "Account" } });
            }
        }
    }

    protected override void HandleUnauthorizedRequest(AuthorizationContext context)
    {
        if (context.HttpContext.Request.IsAjaxRequest())
        {
            var urlHelper = new UrlHelper(context.RequestContext);
            context.HttpContext.Response.StatusCode = 403;
            context.Result = new JsonResult
            {
                Data = new
                {
                    Error = "NotAuthorized",
                    LogInUrl = urlHelper.Action("login", "account")
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        else
        {
            base.HandleUnauthorizedRequest(context);
        }
    }
}
