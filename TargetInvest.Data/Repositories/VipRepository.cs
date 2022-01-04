using System.Linq;
using TargetInvest.Domain.Entities;
using TargetInvest.Domain.Repositories;

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
