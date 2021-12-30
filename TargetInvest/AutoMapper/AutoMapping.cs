using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TargetInvest.Entities;
using TargetInvest.Models;

namespace TargetInvest.AutoMapper
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
