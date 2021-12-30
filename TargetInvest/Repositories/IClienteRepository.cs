using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TargetInvest.Entities;

namespace TargetInvest.Repositories
{
    public interface IClienteRepository
    {
        bool Cadastrar(Cliente cliente);
        List<Cliente> ListarClientes();
        Cliente BuscarCliente(int id);
        Cliente Atualizar(Cliente cliente);
    }
}
