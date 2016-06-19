using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public partial class tbRate
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Carrier is required.")]
        [Display(Name = "Carrrier")]
        public int IdCarrier { get; set; }
        [Required(ErrorMessage = "Rate is required.")]
        [Range(typeof(decimal), "0", "9999.99")] 
        public Nullable<decimal> Rate { get; set; }
        public int IdUser { get; set; }
        public virtual tbCarrier tbCarrier { get; set; }
        public virtual tbUser tbUser { get; set; }
    }
}
