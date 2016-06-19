using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class tbUser
    {
        public tbUser()
        {
            this.tbRates = new List<tbRate>();
        }

        public int Id { get; set; }
        public string User { get; set; }
        public byte[] Password { get; set; }
        public virtual ICollection<tbRate> tbRates { get; set; }
    }
}
