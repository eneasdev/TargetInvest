using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TargetInvest.Entities;

namespace TargetInvest.Repositories
{
    public interface IClienteRepository
    {
        void Cadastrar(Cliente cliente);

        Cliente BuscarCliente(int id);
    }
}
