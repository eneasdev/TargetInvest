﻿using System;
using System.Collections.Generic;
using System.Linq;
using TargetInvest.Domain.Interfaces.Services;

namespace TargetInvest.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        private const int valorMinimoParaSerVip = 6000;

        public ClienteService(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public IndiceVipsViewModel IndiceVip()
        {
            var listaAdesao = new List<Cliente>();
            var listaPotencialAdesao = new List<Cliente>();
            var listaMaiorSeisMil = _clienteRepository.ListarClientes()
                .Where(c => c.RendaMensal >= valorMinimoParaSerVip);

            foreach (var cliente in listaMaiorSeisMil)
            {
                if (cliente.Vip != null)
                {
                    listaAdesao.Add(cliente);
                }
                else
                {
                    listaPotencialAdesao.Add(cliente);
                }
            }

            var indiceVips = new IndiceVipsViewModel();

            indiceVips.Adesão = listaAdesao.Count();
            indiceVips.PotencialAdesão = listaPotencialAdesao.Count();

            return indiceVips;
        }
        public List<ClienteCadastroViewModel> ListarClientes()
        {
            var listaClientes = _mapper.Map<List<ClienteCadastroViewModel>>(_clienteRepository.ListarClientes());

            return listaClientes;
        }

        public EnderecoViewModel BuscarClienteEndereco(int id)
        {
            if (id <= 0) return null;

            var cliente = _clienteRepository.BuscarCliente(id);

            Endereco endereco = cliente.Endereco;

            return _mapper.Map<EnderecoViewModel>(endereco);
        }

        public ClienteCadastroViewModel BuscarCliente(int id)
        {
            if (id <= 0) return null;

            var cliente = _clienteRepository.BuscarCliente(id);

            return _mapper.Map<ClienteCadastroViewModel>(cliente);
        }

        public List<ClienteViewModel> ListarPorRenda(double valor)
        {
            var clientesPorRenda = _mapper.Map<List<ClienteViewModel>>
                (_clienteRepository.ListarClientes().Where(c => c.RendaMensal >= valor));

            return clientesPorRenda;
        }

        public void AtualizarEndereco(int id, EnderecoViewModel enderecoViewModel)
        {
            var cliente = _clienteRepository.BuscarCliente(id);

            cliente.Endereco.Update(_mapper.Map<Endereco>(enderecoViewModel));

            _clienteRepository.Atualizar(cliente);
        }
        public List<ClienteViewModel> ListarPorDataCadastro(DateTime dataInicial, DateTime dataFinal)
        {
            var clientesPorDataCadastro = _mapper.Map<List<ClienteViewModel>>
                (_clienteRepository.ListarClientes()
                .Where(c => c.DataCadastro >= dataInicial && c.DataCadastro <= dataFinal));

            return clientesPorDataCadastro;
        }

        public FinalizaCadastroViewModel Cadastrar(ClienteCadastroViewModel clienteViewModel, EnderecoViewModel enderecoViewModel)
        {
            if (ValidaCPF(clienteViewModel.Cpf) != true) return null;

            Cliente novoCliente = _mapper.Map<Cliente>(clienteViewModel);
            novoCliente.Endereco = _mapper.Map<Endereco>(enderecoViewModel);

            var finalizaCadastro = new FinalizaCadastroViewModel
            {
                Cadastrado = _clienteRepository.Cadastrar(novoCliente),
                OferecerPlanoVip = novoCliente.OferecerPlanoVip()
            };

            return finalizaCadastro;
        }

        private bool ValidaCPF(string vrCPF)
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