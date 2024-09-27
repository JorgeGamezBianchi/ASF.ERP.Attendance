using ASF.ERP.Attendance.BL;
using ASF.ERP.Attendance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ASF.ERP.Attendance.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            AttendanceBL eventsBL = new AttendanceBL();
            IList<AttendanceEvents> events = eventsBL.List();

            return View(events);
        }

        //[HttpPost]
        //public string AttendanceListIndex(DataTableAjaxPostModel param)
        //{
        //    AttendanceBL eventsBL = new AttendanceBL();
        //    return eventsBL.ListAttendanceFilter(param);
        //}


        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";
        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";
        //    return View();
        //}


        //-----------------ACCIONES-----------------


        [HttpGet]
        public ActionResult AttendanceInfoDetails(int? id)
        {
            try
            {
                if (id == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                AttendanceBL eventsBL = new AttendanceBL();
                AttendanceEvents events = eventsBL.GetDetails(id.Value);

                if (events == null)
                    return HttpNotFound();

                return View(events);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }


    }
}