using TargetInvest.Domain.Entities;

namespace TargetInvest.Domain.Interfaces.Repositories
{
    public interface IVipRepository
    {
        Vip BuscarVip(int id);
    }
}
