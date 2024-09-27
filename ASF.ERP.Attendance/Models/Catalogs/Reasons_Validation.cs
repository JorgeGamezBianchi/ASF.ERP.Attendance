using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASF.ERP.Models
{
    [MetadataType(typeof(Reasons_Validation))]
    public partial class Reasons
    {
    }

    public partial class Reasons_Validation
    {
        [Key]
        public int ReasonId { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        [StringLength(150)]
        public string Name { get; set; }
    }
}