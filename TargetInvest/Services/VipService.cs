using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TargetInvest.Models;
using TargetInvest.Repositories;

namespace TargetInvest.Services
{
    public class VipService : IVipService
    {
        private readonly IVipRepository _vipRepository;
        private readonly IMapper _mapper;

        public VipService(IVipRepository vipRepository, IMapper mapper)
        {
            _vipRepository = vipRepository;
            _mapper = mapper;
        }

        public List<VipViewModel> ListarVips()
        {
            var listaVips = _mapper.Map<List<VipViewModel>>(_vipRepository.ListarVips());
            return listaVips;
        }
    }
}
