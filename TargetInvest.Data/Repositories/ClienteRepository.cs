using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TargetInvest.Domain.Entities;
using TargetInvest.Domain.Repositories;

namespace TargetInvest.Infrastructure.Repositories
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
            return _targetContext.Clientes
                .Include(c => c.Endereco)
                .Include(c => c.Vip)
                .ToList();
        }
        public List<Cliente> ListarPorRenda(double renda)
        {
            return _targetContext.Clientes
                .Include(c => c.Endereco)
                .Include(c => c.Vip)
                .Where(c => c.RendaMensal >= renda)
                .ToList();
        }

        public Cliente BuscarCliente(int id)
        {
            return _targetContext.Clientes
                .Include(c => c.Endereco)
                .FirstOrDefault(c => c.Id == id);
        }

        public bool Cadastrar(Cliente cliente)
        {
            try
            {
                _targetContext.Clientes.Add(cliente);
                _targetContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Cliente Atualizar(Cliente cliente)
        {
            _targetContext.Clientes.Update(cliente);
            _targetContext.SaveChanges();
            return cliente;
        }
    }
}
