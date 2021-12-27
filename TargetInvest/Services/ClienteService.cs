﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TargetInvest.Entities;
using TargetInvest.Models;
using TargetInvest.Repositories;

namespace TargetInvest.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public ClienteService(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public List<ClienteViewModel> ListarClientes()
        {
            var listaClientes = _mapper.Map<List<ClienteViewModel>>(_clienteRepository.ListarClientes());

            return listaClientes;
        }

        public ClienteViewModel BuscarCliente(int id)
        {
            if (id <= 0) return null;

            var cliente = _clienteRepository.BuscarCliente(id);

            return _mapper.Map<ClienteViewModel>(cliente);
        }

        public FinalizaCadastroViewModel Cadastrar(ClienteViewModel clienteViewModel, EnderecoViewModel enderecoViewModel)
        {
            if (ValidaCPF(clienteViewModel.Cpf) != true) return null;

            Cliente novoCliente = _mapper.Map<Cliente>(clienteViewModel);
            novoCliente.Endereco = _mapper.Map<Endereco>(enderecoViewModel);

            GerarId(novoCliente);

            _clienteRepository.Cadastrar(novoCliente);

            var finalizaCadastro = new FinalizaCadastroViewModel();
            finalizaCadastro.Cadastrado = true;
            if(novoCliente.RendaMensal >= 6000)
            {
                finalizaCadastro.OferecerPlanoVip = true;
            }
            return finalizaCadastro;
        }

        public void GerarId(Cliente cliente)
        {
            var listaClientes = _clienteRepository.ListarClientes();

            var ultimo = listaClientes.AsEnumerable().LastOrDefault();

            if (ultimo != null)
            {
                cliente.Id = ultimo.Id + 1;
                cliente.Endereco.ClienteId = cliente.Id;

            }
            else
            {
                cliente.Id = 1;
                cliente.Endereco.ClienteId = cliente.Id;
            }
        }

        public bool ValidaCPF(string vrCPF)
        {
            string valor = vrCPF.Replace(".", "");

            valor = valor.Replace("-", "");

            if (valor.Length != 11)

                return false;

            bool igual = true;

            for (int i = 1; i < 11 && igual; i++)
            {
                if (valor[i] != valor[0])

                    igual = false;
            }

            if (igual || valor == "12345678909")

                return false;

            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)
            {
                numeros[i] = int.Parse(

                  valor[i].ToString());
            }

            int soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += (10 - i) * numeros[i];
            }

            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)

                    return false;
            }

            else if (numeros[9] != 11 - resultado)

                return false;

            soma = 0;

            for (int i = 0; i < 10; i++)
            {
                soma += (11 - i) * numeros[i];
            }

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)

                    return false;
            }
            else if (numeros[10] != 11 - resultado)

                return false;

            return true;
        }
    }
}