using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Support;

namespace Business
{
    public class CarrierBll : Repository<tbCarrier> 
    {
        public override tbCarrier Insert(tbCarrier obj)
        {
            if (base.Exists(t => t.NickName == obj.NickName && t.Deleted == false))
                throw new MyException("Already exists a carrier with this nickname!", MessageBox.Erro.danger);
            return base.Insert(obj);
        }

        public override void Update(tbCarrier obj)
        {
            if (base.Exists(t => t.NickName == obj.NickName && t.Id != obj.Id && t.Deleted == false))
                throw new MyException("Already exists a carrier with this nickname!", MessageBox.Erro.danger);
            base.Update(obj);
        }

        public override void Delete(tbCarrier obj)
        {
            obj.Deleted = true;
            base.Update(obj);
        }
    }
}
