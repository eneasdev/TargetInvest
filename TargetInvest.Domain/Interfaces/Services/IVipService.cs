namespace TargetInvest.Domain.Interfaces.Services
{
    public interface IVipService
    {
        VipViewModel BuscarVip(int id);
        bool VipResposta(VipRespostaViewModel vipRespostaViewModel);
    }
}
