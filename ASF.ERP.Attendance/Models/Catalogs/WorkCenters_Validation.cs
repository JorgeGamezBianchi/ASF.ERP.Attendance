using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASF.ERP.Models
{
    [MetadataType(typeof(WorkCenters_Validation))]
    public partial class WorkCenters
    {
        //[Display(Name = "Centro de Trabajo")]
        //public String Fullname
        //{
        //    get
        //    {
        //        return Name + " - " + Reasons.Name + " - " + Site;
        //    }
        //}
    }

    public partial class WorkCenters_Validation
    {
        [Key]
        public int WorkCenterId { get; set; }

        [Display(Name = "Nombre del Centro de Trabajo")]
        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        [Display(Name = "Sitio")]
        [Required]
        [StringLength(30)]
        public string Site { get; set; }

        [Display(Name = "Razón")]
        [Required]
        public int ReasonId { get; set; }

        [Display(Name = "Inactivo")]
        [Required]
        public bool Inactive { get; set; }

        [Display(Name = "Habilitar todos los Centros de Costos")]
        [Required]
        public bool SeeAllCostCenters { get; set; }



        public Reasons Reasons { get; set; }

        public virtual ICollection<CostCenters> CostCenters { get; set; }
        
    }
}