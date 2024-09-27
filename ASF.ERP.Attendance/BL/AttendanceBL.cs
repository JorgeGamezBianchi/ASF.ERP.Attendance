using ASF.ERP.Attendance.Classes;
using ASF.ERP.Attendance.Models;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Net.Mail;
using System.Threading;
using System.Data.Entity.Infrastructure;
using Newtonsoft.Json;

namespace ASF.ERP.Attendance.BL
{
    public class AttendanceBL
    {
        public IList<AttendanceEvents> List()
        {
            using (ASF_APIEntities context = new ASF_APIEntities())
            {
                var events = from a in context.AttendanceEvents
                                .Include(s => s.AttendanceSessions)
                                .Include(s => s.AttendanceSessions.Collaborators)
                                .Include(s => s.CostCenters)
                                .Include(s => s.CostCenters.WorkCenters)
                                .Include(s => s.Collaborators.CollaboratorJobPositions)
                                .Include(s => s.CostCenters.WorkCenters.AttendanceLocations)
                                .OrderByDescending(s => s.Datetime)
                                select a;

                return events.ToArray<AttendanceEvents>();
            }
        }

        public AttendanceEvents GetDetails(int id)
        {
            using (ASF_APIEntities context = new ASF_APIEntities())
            {
                try
                {
                    List<AttendanceEvents> details = context.AttendanceEvents
                        .Include(s => s.AttendanceSessions)
                        .Include(s => s.AttendanceSessions.Collaborators)
                        .Include(s => s.CostCenters)
                        .Include(s => s.CostCenters.WorkCenters)
                        .Include(s => s.Collaborators.CollaboratorJobPositions)
                        .Include(s => s.CostCenters.WorkCenters.AttendanceLocations)
                        .Where(b => b.EventId == id).ToList<AttendanceEvents>();
                    return details[0];
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return null;
            }
        }


        public string ListAttendanceFilter(DataTableAjaxPostModel param)
        {
            using (ASF_APIEntities context = new ASF_APIEntities())
            {
                var predicateMain = PredicateBuilder.New<AttendanceEvents>();
                var predicateFilter = PredicateBuilder.New<AttendanceEvents>();
                var predicateFilterOld = predicateFilter;

                if (!string.IsNullOrEmpty(param.columns[0].search.value))
                    predicateFilter = predicateFilter.And(p => p.CollaboratorId.ToString().ToLower().Contains(param.columns[0].search.value.ToLower()));

                if (!string.IsNullOrEmpty(param.columns[1].search.value))
                    predicateFilter = predicateFilter.And(p => p.Collaborators.Firstname.ToString().ToLower().Contains(param.columns[1].search.value.ToLower()));

                if (!string.IsNullOrEmpty(param.columns[2].search.value))
                    predicateFilter = predicateFilter.And(p => p.Collaborators.CollaboratorJobPositions.Name.ToString().ToLower().Contains(param.columns[2].search.value.ToLower()));

                if (!string.IsNullOrEmpty(param.columns[3].search.value))
                    predicateFilter = predicateFilter.And(p => p.CostCenters.WorkCenters.Name.ToLower().Contains(param.columns[3].search.value.ToLower()));

                if (!string.IsNullOrEmpty(param.columns[4].search.value))
                    predicateFilter = predicateFilter.And(p => p.CostCenters.Name.ToLower().Contains(param.columns[4].search.value.ToLower()));

                if (!string.IsNullOrEmpty(param.columns[5].search.value))
                    predicateFilter = predicateFilter.And(p => p.Datetime.ToString().Contains(param.columns[5].search.value.ToLower()));

                if (!string.IsNullOrEmpty(param.columns[6].search.value))
                    predicateFilter = predicateFilter.And(p => p.AttendanceSessions.Collaborators.Firstname.ToString().ToLower().Contains(param.columns[6].search.value.ToLower()));

                if (!string.IsNullOrEmpty(param.columns[7].search.value))
                    predicateFilter = predicateFilter.And(p => p.CostCenters.WorkCenters.AttendanceLocations.Description.ToLower().Contains(param.columns[7].search.value.ToLower()));

                ExpressionStarter<AttendanceEvents> finalPredicate;
                if (predicateFilter == predicateFilterOld)
                    finalPredicate = predicateMain;
                else
                    finalPredicate = PredicateBuilder.And<AttendanceEvents>(predicateMain, predicateFilter);

                //IQueryable<AttendanceEvents> datafiltered = context.AttendanceEvents.AsNoTracking().AsExpandable();

                IEnumerable<AttendanceEvents> datafiltered = context.AttendanceEvents.AsNoTracking()
                    .Include(e => e.AttendanceSessions)
                    .AsExpandable().AsEnumerable()
                    .Where(finalPredicate).OrderByDescending(s => s.Datetime).ToList();

                // Aplicar el predicado si se ha construido algún filtro
                if (predicateFilter.IsStarted)
                    datafiltered = datafiltered.Where(predicateFilter);

                if (!string.IsNullOrEmpty(param.search.value))
                {
                    var searchVal = param.search.value.ToLower();
                    datafiltered = datafiltered.Where(p => p.CollaboratorId.ToString().ToLower().Contains(searchVal)
                        || p.Collaborators.Firstname.ToLower().Contains(searchVal)
                        || p.Collaborators.CollaboratorJobPositions.Name.ToLower().Contains(searchVal)
                        || p.CostCenters.WorkCenters.Name.ToLower().Contains(searchVal)
                        || p.CostCenters.Name.ToLower().Contains(searchVal)
                        || p.Datetime.ToString().ToLower().Contains(searchVal)
                        || p.AttendanceSessions.Collaborators.Firstname.ToString().ToLower().Contains(searchVal)
                        || p.CostCenters.WorkCenters.AttendanceLocations.Description.ToLower().Contains(searchVal));
                }

                // Ordenamiento
                var sortColumnIndex = param.order == null ? -1 : param.order[0].column;
                var sortDirection = param.order == null ? "asc" : param.order[0].dir;

                switch (sortColumnIndex)
                {
                    case 0:
                        datafiltered = sortDirection == "desc" ? datafiltered.OrderByDescending(c => c.CollaboratorId) : datafiltered.OrderBy(c => c.CollaboratorId);
                        break;
                    case 1:
                        datafiltered = sortDirection == "desc" ? datafiltered.OrderByDescending(c => c.Collaborators.Firstname) : datafiltered.OrderBy(c => c.Collaborators.Firstname);
                        break;
                    default:
                        datafiltered = datafiltered.OrderByDescending(s => s.EventId);
                        break;
                }

                var totalRecords = context.AttendanceEvents.Count();
                var totalRecordsFiltered = datafiltered.Count();
                var requisitionList = datafiltered.Skip(param.start).Take(param.length).ToList();
                var displayResult = from s in requisitionList
                                    select new
                                    {
                                        CollaboratorId = s.CollaboratorId,
                                        Fullname = s.Collaborators.Firstname,
                                        JobPosition = s.Collaborators.CollaboratorJobPositions.Name,
                                        WorkCentersName = s.CostCenters.WorkCenters?.Name,
                                        CostCentersName = s.CostCenters?.Name,
                                        LocalDatetime = s.Datetime.ToString("dd'/'MM'/'yyyy hh:mm:ss tt"),
                                        Supervisor = s.AttendanceSessions.Collaborators?.Firstname,
                                        Location = s.CostCenters.WorkCenters.AttendanceLocations.Description
                                    };

                return JsonConvert.SerializeObject(new
                {
                    data = displayResult,
                    recordsTotal = totalRecords,
                    recordsFiltered = totalRecordsFiltered
                });
            }
        }


        //public string ListAttendanceFilter(DataTableAjaxPostModel param)
        //{
        //    using (ASF_APIEntities context = new ASF_APIEntities())
        //    {
        //        var predicateMain = PredicateBuilder.New<AttendanceEvents>();
        //        var predicateFilter = PredicateBuilder.New<AttendanceEvents>();
        //        var predicateFilterOld = predicateFilter;

        //        predicateMain = predicateMain.And(p => p.CollaboratorId == this.GetUserLogged.UserId);

        //        if (!string.IsNullOrEmpty(param.columns[0].search.value))
        //            predicateFilter = predicateFilter.And(p => p.Collaborators.CollaboratorId.ToString().ToLower().Contains(param.columns[0].search.value.ToLower()));

        //        if (!string.IsNullOrEmpty(param.columns[1].search.value))
        //            predicateFilter = predicateFilter.And(p => p.Collaborators.Firstname.ToString().ToLower().Contains(param.columns[1].search.value.ToLower()));

        //        if (!string.IsNullOrEmpty(param.columns[2].search.value))
        //            predicateFilter = predicateFilter.And(p => p.Collaborators.CollaboratorJobPositions.Name.ToString().ToLower().Contains(param.columns[2].search.value.ToLower()));

        //        if (!string.IsNullOrEmpty(param.columns[3].search.value))
        //            predicateFilter = predicateFilter.And(p => p.CostCenters.WorkCenters.Name.ToLower().Contains(param.columns[3].search.value.ToLower()));

        //        if (!string.IsNullOrEmpty(param.columns[4].search.value))
        //            predicateFilter = predicateFilter.And(p => p.CostCenters.Name.ToLower().Contains(param.columns[4].search.value.ToLower()));

        //        if (!string.IsNullOrEmpty(param.columns[5].search.value))
        //            predicateFilter = predicateFilter.And(p => p.Datetime.ToString().Contains(param.columns[5].search.value.ToLower()));

        //        if (!string.IsNullOrEmpty(param.columns[6].search.value))
        //            predicateFilter = predicateFilter.And(p => p.AttendanceSessions.Collaborators.Firstname.ToString().ToLower().Contains(param.columns[6].search.value.ToLower()));

        //        if (!string.IsNullOrEmpty(param.columns[7].search.value))
        //            predicateFilter = predicateFilter.And(p => p.CostCenters.WorkCenters.AttendanceLocations.Description.ToLower().Contains(param.columns[7].search.value.ToLower()));

        //        ExpressionStarter<AttendanceEvents> finalPredicate;
        //        if (predicateFilter == predicateFilterOld)
        //            finalPredicate = predicateMain;
        //        else
        //            finalPredicate = PredicateBuilder.And<AttendanceEvents>(predicateMain, predicateFilter);

        //        IEnumerable<AttendanceEvents> datafiltered = context.AttendanceEvents.AsNoTracking()
        //            //.Include(e => e.AttendanceSessions)
        //            .AsExpandable().AsEnumerable()
        //            .Where(finalPredicate).OrderByDescending(s => s.Datetime).ToList();

        //        if (!string.IsNullOrEmpty(param.search.value))
        //        {
        //            var searchVal = param.search.value.ToLower();
        //            datafiltered = datafiltered.Where(p => p.CollaboratorId.ToString().ToLower().Contains(searchVal)
        //                || p.Collaborators.Firstname.ToLower().Contains(searchVal)
        //                || p.Collaborators.CollaboratorJobPositions.Name.ToLower().Contains(searchVal)
        //                || p.CostCenters.WorkCenters.Name.ToLower().Contains(searchVal)
        //                || p.CostCenters.Name.ToLower().Contains(searchVal)
        //                || p.Datetime.ToString().ToLower().Contains(searchVal)
        //                || p.AttendanceSessions.Collaborators.Firstname.ToString().ToLower().Contains(searchVal)
        //                || p.CostCenters.WorkCenters.AttendanceLocations.Description.ToLower().Contains(searchVal));
        //        }

        //        var sortColumnIndex = param.order == null ? -1 : param.order[0].column;
        //        var sortDirection = param.order == null ? "asc" : param.order[0].dir;
        //        switch (sortColumnIndex)
        //        {
        //            case 0:
        //                datafiltered = sortDirection == "desc" ? datafiltered.OrderByDescending(c => c.CollaboratorId) : datafiltered.OrderBy(c => c.CollaboratorId);
        //                break;
        //            case 1:
        //                datafiltered = sortDirection == "desc" ? datafiltered.OrderByDescending(c => c.Collaborators.Firstname) : datafiltered.OrderBy(c => c.Collaborators.Firstname);
        //                break;
        //            case 2:
        //                datafiltered = sortDirection == "desc" ? datafiltered.OrderByDescending(c => c.Collaborators.CollaboratorJobPositions?.Name) : datafiltered.OrderBy(c => c.Collaborators.CollaboratorJobPositions?.Name);
        //                break;
        //            case 3:
        //                datafiltered = sortDirection == "desc" ? datafiltered.OrderByDescending(c => c.CostCenters.WorkCenters?.Name) : datafiltered.OrderBy(c => c.CostCenters.WorkCenters?.Name);
        //                break;
        //            case 4:
        //                datafiltered = sortDirection == "desc" ? datafiltered.OrderByDescending(c => c.CostCenters.Name) : datafiltered.OrderBy(c => c.CostCenters.Name);
        //                break;
        //            case 5:
        //                datafiltered = sortDirection == "desc" ? datafiltered.OrderByDescending(c => c.Datetime) : datafiltered.OrderBy(c => c.Datetime);
        //                break;
        //            case 6:
        //                datafiltered = sortDirection == "desc" ? datafiltered.OrderByDescending(c => c.AttendanceSessions.Collaborators?.Firstname) : datafiltered.OrderBy(c => c.AttendanceSessions.Collaborators?.Firstname);
        //                break;
        //            case 7:
        //                datafiltered = sortDirection == "desc" ? datafiltered.OrderByDescending(c => c.CostCenters.WorkCenters.AttendanceLocations?.Description) : datafiltered.OrderBy(c => c.CostCenters.WorkCenters.AttendanceLocations?.Description);
        //                break;
        //            default:
        //                datafiltered = datafiltered.OrderByDescending(s => s.EventId);
        //                break;
        //        }
        //        var totalRecords = context.AttendanceEvents.AsNoTracking().AsExpandable().Where(predicateMain).Count();
        //        var totalRecordsFiltered = datafiltered.Count();
        //        var requisitionList = datafiltered.Skip(param.start).Take(param.length).ToList();
        //        var displayResult = from s in requisitionList
        //                            select new
        //                            {
        //                                s.CollaboratorId,
        //                                Fullname = s.Collaborators.Firstname,
        //                                JobPosition = s.Collaborators.CollaboratorJobPositions.Name,
        //                                WorkCentersName = s.CostCenters.WorkCenters?.Name,
        //                                CostCentersName = s.CostCenters?.Name,
        //                                LocalDatetime = s.Datetime.ToString("dd'/'MM'/'yyyy hh:mm:ss tt"),
        //                                Supervisor = s.AttendanceSessions.Collaborators?.Firstname,
        //                                Location = s.CostCenters.WorkCenters.AttendanceLocations.Description
        //                            };
        //        return JsonConvert.SerializeObject(new
        //        {
        //            data = displayResult,
        //            recordsTotal = totalRecords,
        //            recordsFiltered = totalRecordsFiltered
        //        });
        //    }
        //}



    }
}