using ASF.ERP.Attendance.BL;
using ASF.ERP.Attendance.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ASF.ERP.Attendance.Controllers
{
    public class AttendanceController : Controller
    {
        // GET: Attendance
        public ActionResult AttendanceListIndex()
        {
            return View();
        }

        [HttpPost]
        public string AttendanceListIndex(DataTableAjaxPostModel param)
        {
            AttendanceBL attendanceBL = new AttendanceBL();
            return attendanceBL.ListAttendanceFilter(param);
        }

        // GET: Attendance
        public ActionResult AttendanceReportIndex()
        {
            return View();
        }
    }
}