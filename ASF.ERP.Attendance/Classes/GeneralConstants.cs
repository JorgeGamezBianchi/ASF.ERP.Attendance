using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ASF.ERP.Classes
{
    public class GeneralConstants
    {
        public static String CostCenterPrefix = "ASF";
        public static char CostCenterSeparator = '-';
    }

    public static class commonFunctions
    {
        public static string SiteTimeZoneId = "Pacific Standard Time";
        public static string LocalTimeZoneId = "Central Standard Time (Mexico)";

        public static DateTime ConverToUTCTime(string zoneId)
        {
            DateTime easternTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);

            TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById(zoneId);
            return TimeZoneInfo.ConvertTimeToUtc(easternTime, easternZone);
        }

        public static DateTime ConverDateTimeSpecificRegion(DateTime dtmToConvert)
        {
            TimeZoneInfo timeInfo = TimeZoneInfo.FindSystemTimeZoneById(LocalTimeZoneId);
            return TimeZoneInfo.ConvertTimeFromUtc(dtmToConvert, timeInfo);
        }

        public static DateTime ConvertSiteToLocalTime()
        {
            //Convertimos la fecha-hora remotas a UTC
            DateTime remoteTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            TimeZoneInfo remoteZone = TimeZoneInfo.FindSystemTimeZoneById(SiteTimeZoneId);
            var utcConverted = TimeZoneInfo.ConvertTimeToUtc(remoteTime, remoteZone);

            //Convertimos la fecha-hora UTC a la zona local especificada
            TimeZoneInfo localZone = TimeZoneInfo.FindSystemTimeZoneById(LocalTimeZoneId);
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcConverted, localZone);

            return localTime;
        }

        public static DateTime ConvertUTCToLocalTime(DateTime utcTime)
        {
            //Convertimos la fecha-hora UTC a la zona local especificada
            TimeZoneInfo localZone = TimeZoneInfo.FindSystemTimeZoneById(LocalTimeZoneId);
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, localZone);

            return localTime;
        }

        public static int GetWeekOfYear(DateTime dt)
        {
            //CultureInfo myCI = new CultureInfo("es-MX");
            CultureInfo myCI = CultureInfo.CurrentCulture;
            Calendar myCal = myCI.Calendar;

            // Gets the DTFI properties required by GetWeekOfYear.
            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;

            // Se considesa un offset para evitar el salto de la semana 1 del año
            DateTime offsetDay = GetDay(dt, DayOfWeek.Sunday);

            return myCal.GetWeekOfYear(offsetDay, myCWR, myFirstDOW) - 1;
        }

        public static int GetWeekOfYear1(DateTime dt)
        {
            int res = 0;
            res = System.Globalization.CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
              dt, System.Globalization.CalendarWeekRule.FirstDay, System.Globalization.DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek);

            return res;
        }

        public static int GetWeekOfYearX(DateTime dt)
        {
            //CultureInfo myCI = new CultureInfo("es-MX");
            CultureInfo myCI = CultureInfo.CurrentCulture;
            Calendar myCal = myCI.Calendar;

            // Gets the DTFI properties required by GetWeekOfYear.
            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;

            // Se considesa un offset para evitar el salto de la semana 1 del año
            DateTime offsetDay = GetDay(dt, DayOfWeek.Monday);

            DateTime firtsMonth = GetFirstMondayOfYear(dt.Year);

            var difference = offsetDay.Subtract(firtsMonth).Days;

            var y = (difference + 7) / 7;

            return y;
        }

        public static Dictionary<int, DateTime> yearsDic = new Dictionary<int, DateTime>() {
            { 2020, new DateTime(2019,12,30) },
            { 2021, new DateTime(2021,01,04) },
            { 2022, new DateTime(2022,01,03) }
        };

        public static int GetWeekOfYear2(DateTime dt)
        {
            // Obtiene el día lunes de la fecha señalada
            DateTime offsetDay = GetDay(dt, DayOfWeek.Monday);

            try
            {
                DateTime firtsMonday = yearsDic[dt.Year];

                var difference = offsetDay.Subtract(firtsMonday).Days;

                var y = (difference + 7) / 7;

                return y;
            }
            catch (Exception)
            {
                return GetWeekOfYear(dt);
            }

        }

        private static DateTime GetFirstMondayOfYear(int year)
        {
            DateTime dt = new DateTime(year, 1, 1);

            while (dt.DayOfWeek != DayOfWeek.Monday)
            {
                dt = dt.AddDays(1);
            }

            return dt;
        }


        // Funcíon en caso de que el primer dia de la semana sea distinto de domingo
        private static DateTime GetDay(DateTime source, DayOfWeek dayOfWeek)
        {
            const int offsetSinceMondayIsFirstDayOfWeek = 7 - (int)DayOfWeek.Monday;
            return source.AddDays(((int)dayOfWeek + offsetSinceMondayIsFirstDayOfWeek) % 7
              - ((int)source.DayOfWeek + offsetSinceMondayIsFirstDayOfWeek) % 7);
        }
        // Funcíon en caso de que el primer dia de la semana sea domingo
        private static DateTime GetDay2(DateTime source, DayOfWeek dayOfWeek)
        {
            return source.AddDays((int)dayOfWeek - (int)source.DayOfWeek);
        }
    }

    public class StaticPermissions
    {
        public const string ViaticsCreate = "Viatics-Create";
        public const string BudgetsViewAllRecordsForUpdate = "Budgets-ViewAllRecordsForUpdate";
        public const string CostCentersViewSensitiveInformation = "CostCenters-ViewSensitiveInformation";
        public const string CostCentersAllowManualRequisitions = "CostCenters-AllowManualRequisitions";
        public const string QuotationsResponsibleUser = "Quotations-ResponsibleUser";
        public const string WarehouseMovementsExitRequest = "WarehouseMovements-ExitRequest";
        public const string CollaboratorsLocatorUser = "Collaborators-LocatorUser";
        public const string RequisitionsChangeProposalEditor = "Requisitions-ChangeProposalEditor";
        public const string AdministrationDirectionUser = "Administration-DirectionUser";
        public const string AdministrationSeePasswords = "Administration-SeePasswords";
        public const string WarehouseTransferViewClosedCCs = "WarehouseMovements-ViewClosedCCs";
        public const string ProductsEditFull = "Products-ProductEditFull";
        public const string ProductsInactivate = "Products-ProductInactivate";
        //public const string ExpensesCheckingAuthorizationUser = "ExpensesChecking-ExpensesReviewEdit";

        //Permisos estáticos que tienen controlador y acción relacionados
        public const string RequisitionsAuthorizationUser = "Requisitions-RequisitionAuthorizationEdit";
        public const string RequisitionsValidationUser = "Requisitions-RequisitionValidationEdit";
        public const string RequisitionsValidatorNormal = "Requisitions-RequisitionValidatorNormal";
        public const string RequisitionsValidatorSpecial = "Requisitions-RequisitionValidatorSpecial";
        public const string RequisitionsAttendUser = "Requisitions-RequisitionAttendEdit";
        public const string RequisitionsAttendServiceUser = "Requisitions-RequisitionAttendServiceEdit";
        public const string RequisitionsEnquiryDetails = "Requisitions-RequisitionEnquiryDetails";
        public const string RequisitionsPurchaseUser = "Requisitions-RequisitionChangeCommitmentDateByPurchasesUser";
        public const string PurchaseOrderDetails = "PurchaseOrders-PurchaseOrderDetails";
    }

    public class StaticRoles
    {
        public const string CollaboratorAdmin = "Seguridad-RolValidaRegistrosSHE";
        public const string CollaboratorUbi = "ColaboradoresUbicaciones-Actualizar";//leo
    }

}