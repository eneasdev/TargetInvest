using TargetInvest.Domain.Entities;

namespace TargetInvest.Domain.Repositories
{
    public interface IVipRepository
    {
        Vip BuscarVip(int id);
    }
}
