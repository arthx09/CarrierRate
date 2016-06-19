using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Support;

namespace Business
{
    public class RateBll : Repository<tbRate>
    {
        public override tbRate Insert(tbRate obj)
        {
            if (base.Exists(t => t.IdUser == obj.IdUser && t.IdCarrier == obj.IdCarrier))
                throw new MyException("User already rated this carrier!", MessageBox.Erro.danger);
            return base.Insert(obj);
        }
    }
}
