using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TargetInvest.Models;

namespace TargetInvest.Services
{
    public interface IClienteService
    {
        FinalizaCadastroViewModel Cadastrar(ClienteCadastroViewModel clienteViewModel, EnderecoViewModel enderecoViewModel);
        List<ClienteCadastroViewModel> ListarClientes();
        List<ClienteViewModel> ListarPorDataCadastro(DateTime dataInicial, DateTime dataFinal);
        List<ClienteViewModel> ListarPorRenda(double valor);
        ClienteCadastroViewModel BuscarCliente(int id);
        EnderecoViewModel BuscarClienteEndereco(int id);

    }
}
