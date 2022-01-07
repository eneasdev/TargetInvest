using AutoMapper;
using TargetInvest.Application.Models.InputModels;
using TargetInvest.Application.Models.ViewModels;
using TargetInvest.Domain.Entities;

namespace TargetInvest.Mapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<NovoClienteInputModel, Cliente>();
            CreateMap<Cliente, NovoClienteInputModel>();

            CreateMap<ClienteViewModel, Cliente>();
            CreateMap<Cliente, ClienteViewModel>()
                .ForMember(c => c.Renda, opt => opt.MapFrom(src => src.RendaMensal));

            CreateMap<EnderecoViewModel, Endereco>();
            CreateMap<Endereco, EnderecoViewModel>();

            CreateMap<VipViewModel, Vip>();
            CreateMap<Vip, VipViewModel>();
        }
    }
}
