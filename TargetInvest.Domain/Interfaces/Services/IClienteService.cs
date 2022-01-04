using System;
using System.Collections.Generic;

namespace TargetInvest.Domain.Interfaces.Services
{
    public interface IClienteService
    {
        IndiceVipsViewModel IndiceVip();
        List<ClienteCadastroViewModel> ListarClientes();
        ClienteCadastroViewModel BuscarCliente(int id);
        EnderecoViewModel BuscarClienteEndereco(int id);
        List<ClienteViewModel> ListarPorRenda(double valor);
        void AtualizarEndereco(int id, EnderecoViewModel enderecoViewModel);
        List<ClienteViewModel> ListarPorDataCadastro(DateTime dataInicial, DateTime dataFinal);
        FinalizaCadastroViewModel Cadastrar(ClienteCadastroViewModel clienteViewModel, EnderecoViewModel enderecoViewModel);
    }
}
