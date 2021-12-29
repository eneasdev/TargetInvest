using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TargetInvest.Models;

namespace TargetInvest.Services
{
    public interface IClienteService
    {
        FinalizaCadastroViewModel Cadastrar(ClienteViewModel clienteViewModel, EnderecoViewModel enderecoViewModel);
        List<ClienteViewModel> ListarClientes();
        ClienteViewModel BuscarCliente(int id);

    }
}
