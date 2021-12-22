using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TargetInvest.Models;

namespace TargetInvest.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IValidacaoService _validacaoService;

        public ClienteService(IValidacaoService validacaoService)
        {
            _validacaoService = validacaoService;
        }

        public ClienteViewModel Cadastrar(ClienteViewModel viewModel)
        {
            if (_validacaoService.ValidaCPF(viewModel.Cpf) != true) return null;

            //Request ao ClienteRepository para fazer cadastro

            return viewModel;
        }
    }
}
