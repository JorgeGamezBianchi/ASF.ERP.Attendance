using ASF.ERP.Attendance.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASF.ERP.Models
{
    [MetadataType(typeof(AttendanceEvents_Validation))]
    public partial class AttendanceEvents
    {
        
    }

    public partial class AttendanceEvents_Validation
    {
        [Key]
        [Display(Name = "Evento")]
        public int EventId { get; set; }

        [Display(Name = "Sesión")]
        public int SessionId { get; set; }

        [Display(Name = "Collaborador")]
        public int CollaboratorId { get; set; }

        [Display(Name = "Centro de Costos")]
        public int CostCenterId { get; set; }

        [Display(Name = "Fecha y Hora")]
        public DateTime Datetime { get; set; }



        public AttendanceSessions AttendanceSessions { get; set; }
        public Collaborators Collaborators { get; set; }
        public virtual ICollection<CostCenters> CostCenters { get; set; }

    }
}