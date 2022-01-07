using System.Collections.Generic;
using TargetInvest.Domain.Entities;

namespace TargetInvest.Domain.Repositories
{
    public interface IClienteRepository
    {
        bool Cadastrar(Cliente cliente);
        List<Cliente> ListarClientes();
        List<Cliente> ListarPorRenda(double renda);
        Cliente BuscarCliente(int id);
        Cliente Atualizar(Cliente cliente);
    }
}
