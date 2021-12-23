using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TargetInvest.Data;
using TargetInvest.Entities;
using TargetInvest.Models;

namespace TargetInvest.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly TargetContext _targetContext;

        public ClienteRepository(TargetContext targetContext)
        {
            _targetContext = targetContext;
        }

        public List<Cliente> ListarClientes()
        {
            return _targetContext.Clientes.ToList();
        }

        public Cliente BuscarCliente(int id)
        {
            return _targetContext.Clientes.FirstOrDefault(c => c.Id == id);
        }

        public void Cadastrar(Cliente cliente)
        {
            _targetContext.Clientes.Add(cliente);
            _targetContext.SaveChanges();
        }
    }
}
