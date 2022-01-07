using System;
using System.Collections.Generic;
using TargetInvest.Application.Models.InputModels;
using TargetInvest.Application.Models.ViewModels;

namespace TargetInvest.Application.Interfaces
{
    public interface IClienteService
    {
        IndiceVipsViewModel IndiceVip();
        ClienteViewModel BuscarCliente(int id);
        EnderecoViewModel BuscarClienteEndereco(int id);
        List<ClienteViewModel> ListarPorRenda(double valor);
        EnderecoViewModel AtualizarEndereco(int id, EnderecoViewModel enderecoViewModel);
        List<ClienteViewModel> ListarPorDataCadastro(DateTime dataInicial, DateTime dataFinal);
        FinalizaCadastroViewModel Cadastrar(NovoClienteInputModel clienteViewModel, EnderecoViewModel enderecoViewModel);
    }
}
