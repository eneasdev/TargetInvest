using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TargetInvest.Data;
using TargetInvest.Entities;

namespace TargetInvest.Repositories
{
    public class VipRepository : IVipRepository
    {
        private readonly TargetContext _targetContext;

        public VipRepository(TargetContext targetContext)
        {
            _targetContext = targetContext;
        }

        public List<Vip> ListarVips()
        {
            return _targetContext.Vips.ToList();
        }
    }
}
