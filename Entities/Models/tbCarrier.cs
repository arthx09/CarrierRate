using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public partial class tbCarrier
    {
        public tbCarrier()
        {
            this.tbRates = new List<tbRate>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage="Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "NickName is required.")]
        public string NickName { get; set; }
        public bool Status { get; set; }
        public bool Deleted { get; set; }
        public virtual ICollection<tbRate> tbRates { get; set; }
    }
}
