using System.Web.Mvc;

namespace ASF.ERP
{
    public class DeleteFileAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Flush();
            string filePath = (filterContext.Result as FilePathResult).FileName;
            System.IO.File.Delete(filePath);
        }
    }
}