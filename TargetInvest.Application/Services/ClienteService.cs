using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TargetInvest.Application.Interfaces;
using TargetInvest.Application.Models.InputModels;
using TargetInvest.Application.Models.ViewModels;
using TargetInvest.Domain.Entities;
using TargetInvest.Domain.Repositories;

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
            try
            {
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

                var indiceVips = new IndiceVipsViewModel
                {
                    Adesão = listaAdesao.Count,
                    PotencialAdesão = listaPotencialAdesao.Count
                };

                return indiceVips;
            }
            catch
            {
                return null;
            }
        }

        public EnderecoViewModel BuscarClienteEndereco(int id)
        {
            if (id <= 0) return null;

            var cliente = _clienteRepository.BuscarCliente(id);

            Endereco endereco = cliente.Endereco;

            return _mapper.Map<EnderecoViewModel>(endereco);
        }

        public ClienteViewModel BuscarCliente(int id)
        {
            if (id <= 0) return null;

            var cliente = _clienteRepository.BuscarCliente(id);

            return _mapper.Map<ClienteViewModel>(cliente);
        }

        public List<ClienteViewModel> ListarPorRenda(double renda)
        {
            if (renda <= 0) return null;

            var clientesPorRenda = _mapper.Map<List<ClienteViewModel>>
                (_clienteRepository.ListarPorRenda(renda));

            return clientesPorRenda;
        }

        public EnderecoViewModel AtualizarEndereco(int id, EnderecoViewModel enderecoViewModel)
        {
            if (enderecoViewModel == null) return null;

            var cliente = _clienteRepository.BuscarCliente(id);

            cliente.Endereco.Update(_mapper.Map<Endereco>(enderecoViewModel));

            var clienteAtualizado = _clienteRepository.Atualizar(cliente);

            return _mapper.Map<EnderecoViewModel>(clienteAtualizado.Endereco);
        }
        public List<ClienteViewModel> ListarPorDataCadastro(DateTime dataInicial, DateTime dataFinal)
        {
            var clientesPorDataCadastro = _mapper.Map<List<ClienteViewModel>>
                (_clienteRepository.ListarClientes()
                .Where(c => c.DataCadastro >= dataInicial && c.DataCadastro <= dataFinal));

            return clientesPorDataCadastro;
        }

        public FinalizaCadastroViewModel Cadastrar(NovoClienteInputModel clienteViewModel, EnderecoViewModel enderecoViewModel)
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
