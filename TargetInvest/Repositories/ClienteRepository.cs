﻿using Microsoft.EntityFrameworkCore;
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
            return _targetContext.Clientes
                .Include(c => c.Endereco)
                .ToList();
        }

        public List<Cliente> ListaPorDataDeCadastro(DateTime dataInicial, DateTime dataFinal)
        {
            return _targetContext.Clientes
                .Where(c => c.DataCadastro >= dataInicial && c.DataCadastro <= dataFinal)
                .ToList();
        }

        public Cliente BuscarCliente(int id)
        {
            return _targetContext.Clientes.FirstOrDefault(c => c.Id == id);
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
