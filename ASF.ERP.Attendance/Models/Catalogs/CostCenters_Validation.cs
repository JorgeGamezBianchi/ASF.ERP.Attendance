using ASF.ERP.Action_Filters;
using ASF.ERP.Attendance.Models;
using ASF.ERP.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASF.ERP.Models
{
    [MetadataType(typeof(CostCenters_Validation))]
    public partial class CostCenters
    {
        String mPrefix1, mPrefix2, mPrefix3;
        [Display(Name = "Prefijo")]
        public string NamePrefix1 { get { return mPrefix1; } set { mPrefix1 = value; this.GenerateName(); } }

        [Display(Name = "Año")]
        [Required]
        public string NamePrefix2 { get { return mPrefix2; } set { mPrefix2 = value; this.GenerateName(); } }

        [Display(Name = "Identificador")]
        [Required]
        public string NamePrefix3 { get { return mPrefix3; } set { mPrefix3 = value; this.GenerateName(); } }

        //public string Fullname
        //{
        //    get { return Name + " - " + Description; }
        //}

        private void GenerateName()
        {
            string prefix1 = "", prefix2 = "", prefix3 = "";
            if (this.NamePrefix1 != null) prefix1 = this.NamePrefix1;
            if (this.NamePrefix2 != null) prefix2 = this.NamePrefix2;
            if (this.NamePrefix3 != null) prefix3 = this.NamePrefix3;
            //this.Name = this.NamePrefix1 + this.NamePrefix2 + "-" + this.NamePrefix3;
        }

        //public void SplitName()
        //{
        //    var prefixesModel = this.Name.Split(GeneralConstants.CostCenterSeparator);
        //    if (prefixesModel.Length > 1)
        //    {
        //        this.NamePrefix1 = GeneralConstants.CostCenterPrefix;
        //        this.NamePrefix2 = prefixesModel[0].Replace(this.NamePrefix1, "");
        //        this.NamePrefix3 = prefixesModel[1];
        //    }
        //}
    }

    public class CostCenters_Validation
    {
        [Key]
        public int CostCenterId { get; set; }

        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Display(Name = "Descripción")]
        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Centro de Trabajo")]
        [Required]
        public int WorkCenterId { get; set; }

        [Display(Name = "Estado")]
        [Required]
        public byte CostCenterStatusId { get; set; }

        [Display(Name = "Inactivo")]
        public bool Inactive { get; set; }

        [Display(Name = "Contiene información sensible")]
        public bool ContainsSensitiveInformation { get; set; }

        [Display(Name = "Es Genérico")]
        public bool IsGeneric { get; set; }

        [Display(Name = "Requisiciones Manuales")]
        public bool AllowManualRequisitions { get; set; }

        [Display(Name = "Centro de Trabajo")]



        public virtual WorkCenters WorkCenters { get; set; }
        public virtual CostCenterStatus CostCenterStatus { get; set; }
    }

    public enum ECostCenterStatus
    {
        Created = 1,    // Creado
        Active = 2,     // Vigente
        Closed = 3      // Cerrado
    }

    public enum ECostCenterRequisitionTypeFilter
    {
        OnlyManualRequisitions,
        OnlyBudgetRequisitions,
        All
    }
}