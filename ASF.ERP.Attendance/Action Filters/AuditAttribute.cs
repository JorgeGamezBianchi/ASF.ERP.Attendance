using ASF.ERP.Attendance.BL;
using ASF.ERP.Attendance.Models;
using System;
using System.Web.Mvc;
using System.Web.Security;

namespace ASF.ERP
{
    public class AuditAttribute : ActionFilterAttribute
    {
        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    //Almacena la solicitud en un objeto accesible
        //    var request = filterContext.HttpContext.Request;
        //    // Genera una auditoría
        //    SystemAudit audit = new SystemAudit()
        //    {
        //        //Obtiene el nombre de usuario
        //        UserName = (request.IsAuthenticated) ? filterContext.HttpContext.User.Identity.Name : "Anonymous",
        //        //Dirección IP de la solicitud
        //        IPAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress,
        //        //URL a la que se accedió
        //        AreaAccessed = request.RawUrl,
        //        //Crea la estampa de tiempo
        //        TimeAccessed = DateTime.UtcNow,
        //        //Obtiene el tiempo restante antes de que la sesión expire
        //        TimeoutTotalSeconds = FormsAuthentication.Timeout.TotalSeconds
        //    };

        //    //Guarda la auditoría en la base de adtos
        //    AdministrationBL adminBL = new AdministrationBL();
        //    adminBL.SystemAuditCreate(audit);

        //    // Finishes executing the Action as normal 
        //    base.OnActionExecuting(filterContext);
        //}
    }
}