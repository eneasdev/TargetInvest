using AutoMapper;
using TargetInvest.Domain.Entities;
using TargetInvest.Models;

namespace TargetInvest.Mapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<ClienteCadastroViewModel, Cliente>();
            CreateMap<Cliente, ClienteCadastroViewModel>();

            CreateMap<ClienteViewModel, Cliente>();
            CreateMap<Cliente, ClienteViewModel>();

            CreateMap<EnderecoViewModel, Endereco>();
            CreateMap<Endereco, EnderecoViewModel>();

            CreateMap<VipViewModel, Vip>();
            CreateMap<Vip, VipViewModel>();
        }
    }
}
