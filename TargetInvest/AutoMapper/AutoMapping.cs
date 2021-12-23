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
            CreateMap<ClienteViewModel, Cliente>();
            CreateMap<Cliente, ClienteViewModel>();

            CreateMap<EnderecoViewModel, Endereco>();
            CreateMap<Endereco, EnderecoViewModel>();
        }
    }
}
