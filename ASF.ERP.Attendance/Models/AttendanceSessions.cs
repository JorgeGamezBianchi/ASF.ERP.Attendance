//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASF.ERP.Attendance.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AttendanceSessions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AttendanceSessions()
        {
            this.AttendanceEvents = new HashSet<AttendanceEvents>();
        }
    
        public int SessionId { get; set; }
        public int CollaboratorId { get; set; }
        public System.DateTime StartAS { get; set; }
        public System.DateTime EndAS { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttendanceEvents> AttendanceEvents { get; set; }
        public virtual Collaborators Collaborators { get; set; }
    }
}
