using TargetInvest.Application.Models.ViewModels;

namespace TargetInvest.Application.Interfaces
{
    public interface IVipService
    {
        VipViewModel BuscarVip(int id);
        bool VipResposta(VipRespostaViewModel vipRespostaViewModel);
    }
}
