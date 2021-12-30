using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TargetInvest.Entities;
using TargetInvest.Models;
using TargetInvest.Repositories;

namespace TargetInvest.Services
{
    public class VipService : IVipService
    {
        private readonly IVipRepository _vipRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public VipService(IVipRepository vipRepository, IClienteRepository clienteRepository, IMapper mapper)
        {
            _vipRepository = vipRepository;
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public VipViewModel BuscarVip(int id)
        {
            if (id <= 0) return null;

            var vip = _vipRepository.BuscarVip(id);

            return _mapper.Map<VipViewModel>(vip);
        }

        public bool VipResposta(VipRespostaViewModel vipRespostaViewModel)
        {
            var cliente = _clienteRepository.BuscarCliente(vipRespostaViewModel.ClienteId);
            var vip = _vipRepository.BuscarVip(1);
            if ( vipRespostaViewModel.Resposta == true)
            {
                cliente.Vip = vip;
                _clienteRepository.Atualizar(cliente);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
