using ASF.ERP.Action_Filters;
using ASF.ERP.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using static ASF.ERP.Models.Collaborators;

namespace ASF.ERP.Models
{

    [MetadataType(typeof(Collaborators_Validation))]
    public partial class Collaborators
    {
        //[NotMapped]
        //[Display(Name = "Nombre")]
        //public string Fullname
        //{
        //    get { return this.Firstname + " " + this.Lastname + " " + this.Lastname2; }
        //}

        [NotMapped]
        [Required]
        [Display(Name = "Centro de Trabajo")]
        public int WorkCenterId { get; set; }
    }



    public class Collaborators_Validation
    {
        [Key]
        public int CollaboratorId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nombre(s)")]
        public string Firstname { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Apellido Paterno")]
        public string Lastname { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Apellido Materno")]
        public string Lastname2 { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "e-mail")]
        public string EMail { get; set; }

        [Required]
        [Display(Name = "No. Colaborador")]
        [Range(0, short.MaxValue, ErrorMessage = "El valor debe estar entre 0 y 32767")]
        public short Tag { get; set; }

        [Required(ErrorMessage = "Ingrese Usuario Localizador")]
        [Display(Name = "Nombre del Usuario Localizador")]
        public int CoordinatorLocationId { get; set; }

        [Required(ErrorMessage = "Ingrese Puesto de trabajo")]
        [Display(Name = "Puesto de Trabajo")]
        public int JobPositionId { get; set; }

        [Required(ErrorMessage = "Ingrese Municipio")]
        [Display(Name = "Municipio")]
        public int ResidenceId { get; set; }

        [Required(ErrorMessage = "Ingrese Fecha de Ingreso")]
        [Display(Name = "Fecha de Ingreso")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime AdmissionDate { get; set; }

        [Required(ErrorMessage = "Ingrese NSS")]
        [Display(Name = "NSS")]
        [StringLength(11)]
        [MinLength(11, ErrorMessage = "El campo NSS no contiene 11 digitos")]
        [MaxLength(11)]
        public string NSS { get; set; }

        [Required(ErrorMessage = "Ingrese CURP")]
        [Display(Name = "CURP")]
        [StringLength(18)]
        public string CURP;

        [Required(ErrorMessage = "Ingrese Cuenta")]
        [Display(Name = "Cuenta")]
        [RegularExpression("^([0-9]{10}|[0-9]{18})$", ErrorMessage = "La longitud del campo Cuenta debe ser de 10 o 18 digitos")]
        public string BAccount { get; set; }

        [Required(ErrorMessage = "Ingrese RFC")]
        [Display(Name = "RFC")]
        [StringLength(13)]
        [RegularExpression("[A-Z]{4}[0-9]{6}[a-zA-Z0-9_]{3}", ErrorMessage = "RFC invalido. Ingrese formato de tipo AAAA-000000-XXX")]
        public string RFC { get; set; }

        [Required(ErrorMessage = "Ingrese Residencia")]
        [Display(Name = "Residencia")]
        public int? StateId { get; set; }

        [Display(Name = "Numero de Credito Infonavit")]
        [MaxLength(10)]
        [RequiredIf("ApplyInfonavitData", false, "Ingrese Crédito Infonavit")]
        public string CreditNumber { get; set; }

        [Range(1, 50)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(2,2)")]
        [RequiredIf("ApplyInfonavitData", false, "Ingrese Factor de Descuento")]
        [Display(Name = "Factor de descuento")]
        public decimal DiscountFactor { get; set; }

        [Display(Name = "Tipo de descuento")]
        [RequiredIf("ApplyInfonavitData", false, "Seleccione tipo de descuento")]
        public int DiscountType { get; set; }

        [Display(Name = "No Aplican Datos Infonavit")]
        public bool? ApplyInfonavitData { get; set; }

        [Required]
        [Display(Name = "Usuario Inactivo")]
        public bool Inactive { get; set; }

    }
}