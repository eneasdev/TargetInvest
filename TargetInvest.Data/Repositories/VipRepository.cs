using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TargetInvest.Entities;

namespace TargetInvest.Infrastructure.Repositories
{
    public class VipRepository : IVipRepository
    {
        private readonly TargetContext _targetContext;

        public VipRepository(TargetContext targetContext)
        {
            _targetContext = targetContext;
        }

        public Vip BuscarVip(int id)
        {
            return _targetContext.Vips.FirstOrDefault(v => v.Id == id);
        }
    }
}
