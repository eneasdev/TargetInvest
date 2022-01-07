using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using TargetInvest.Application.Models.ViewModels;
using TargetInvest.Application.Services;
using TargetInvest.Domain.Entities;
using TargetInvest.Domain.Repositories;
using TargetInvest.Mapper;
using Xunit;

namespace TargetInvest.Application.Tests
{
    public class ClienteServiceDeveria
    {
        private static IMapper _mapper;

        public ClienteServiceDeveria()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new AutoMapping());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [Fact(DisplayName = "BuscarIndiceVip_QuandoListaClienteNaoEhNulla")]
        public void BuscarIndiceVipNaoNulla()
        {
            var mockClienteRepository = new Mock<IClienteRepository>();
            var sut = new ClienteService(mockClienteRepository.Object, _mapper);

            mockClienteRepository.Setup(c => c.ListarClientes()).Returns(ListaDeClientes());
            var indiceDeVips = sut.IndiceVip();

            Assert.True(indiceDeVips.Adesão == 4 && indiceDeVips.PotencialAdesão == 2);
        }

        [Fact(DisplayName = "FalharBuscarIndiceVip_QuandoListaClienteEhNulla")]
        public void BuscarIndiceVipQuandoListaClienteNulla()
        {
            var mockClienteRepository = new Mock<IClienteRepository>();
            var sut = new ClienteService(mockClienteRepository.Object, _mapper);

            var listaEsperada = ListaDeClientesViewModel();
            List<Cliente> lista = null;

            mockClienteRepository.Setup(c => c.ListarClientes()).Returns(lista);
            var indiceDeVips = sut.IndiceVip();

            Assert.True(indiceDeVips == null);
        }

        [Fact(DisplayName = "BuscarEnderecoDeClientePorIdQuandoIdPositivo")]
        public void BuscarClientePorEndereco()
        {
            var mockClienteRepository = new Mock<IClienteRepository>();
            var sut = new ClienteService(mockClienteRepository.Object, _mapper);
            var lista = ListaDeClientes();
            var enderecoEsperado = new EnderecoViewModel()
            {
                Id = 2,
                Cep = "20970-006",
                Bairro = "Jacare",
                Cidade = "Rio de Janeiro",
                Logradouro = "Amaro Rangel",
                Complemento = "Sobrado",
                Uf = "RJ"
            };

            mockClienteRepository.Setup(c => c.BuscarCliente(lista[1].Id)).Returns(lista[1]);
            var clienteEndereco = sut.BuscarClienteEndereco(lista[1].Id);

            Assert.True(enderecoEsperado.Id == clienteEndereco.Id);
        }

        [Theory(DisplayName = "NaoRetornarEnderecoDeClientePorIdQuandoIdZeroOuNegativo")]
        [InlineData(0)]
        [InlineData(-1)]
        public void BuscarClientePorEnderecoZeroOuNegativo(int id)
        {
            var mockClienteRepository = new Mock<IClienteRepository>();
            var sut = new ClienteService(mockClienteRepository.Object, _mapper);

            var clienteEndereco = sut.BuscarClienteEndereco(id);

            Assert.True(clienteEndereco == null);
        }

        [Fact(DisplayName = "BuscarClientePorIdQuandoIdPositivo")]
        public void BuscarClientePorIdQuandoIdPositivo()
        {
            var mockClienteRepository = new Mock<IClienteRepository>();
            var sut = new ClienteService(mockClienteRepository.Object, _mapper);
            var listaClientes = ListaDeClientes();

            mockClienteRepository.Setup(c => c.BuscarCliente(listaClientes[0].Id)).Returns(listaClientes[0]);
            var cliente = sut.BuscarCliente(1);

            Assert.True(cliente.Id == listaClientes[0].Id);
        }

        [Theory(DisplayName = "NaoRetornarClientePorIdQuandoIdZeroOuNegativo")]
        [InlineData(0)]
        [InlineData(-1)]
        public void BuscarClientePorZeroOuNegativo(int id)
        {
            var mockClienteRepository = new Mock<IClienteRepository>();
            var sut = new ClienteService(mockClienteRepository.Object, _mapper);

            var cliente = sut.BuscarCliente(id);

            Assert.True(cliente == null);
        }
        
        [Theory(DisplayName = "ListarClientesPorRendaQuandoValorPositivo")]
        [InlineData(1000)]
        [InlineData(3000)]
        [InlineData(6000)]
        public void ListarClientesPorRendaQuandoValorPositivo(double renda)
        {
            var mockClienteRepository = new Mock<IClienteRepository>();
            var sut = new ClienteService(mockClienteRepository.Object, _mapper);
            var listaPorRenda = ListaDeClientes().FindAll(c => c.RendaMensal >= renda);

            mockClienteRepository.Setup(c => c.ListarPorRenda(renda)).Returns(listaPorRenda);
            var cliente = sut.ListarPorRenda(renda);

            foreach(var c in cliente)
            {
                Assert.True(c.Renda >= renda);
            }
        }

        [Theory(DisplayName = "NaoListarClientesPorRendaQuandoValorZeroOuNegativo")]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void NaoListarClientesPorRendaQuandoValorZeroOuNegativo(double renda)
        {
            var mockClienteRepository = new Mock<IClienteRepository>();
            var sut = new ClienteService(mockClienteRepository.Object, _mapper);

            var cliente = sut.ListarPorRenda(renda);

            Assert.True(cliente == null);
        }

        [Fact(DisplayName = "AtualizarEnderecoClienteQuandoEnderecoRecebidoNaoNullo")]
        public void AtualizarEnderecoClienteQuandoEnderecoRecebidoNaoNullo()
        {
            var mockClienteRepository = new Mock<IClienteRepository>();
            var sut = new ClienteService(mockClienteRepository.Object, _mapper);
            var cliente = ListaDeClientes();
            var enderecos = ListaDeEnderecosViewModel();

            mockClienteRepository.Setup(c => c.BuscarCliente(1)).Returns(cliente[0]);
            mockClienteRepository.Setup(c => c.Atualizar(cliente[0])).Returns(cliente[0]);
            var novoClienteEndereco = sut.AtualizarEndereco(1, enderecos[1]);

            Assert.NotEqual("Jacare", cliente[0].Endereco.Bairro);
        }

        [Fact(DisplayName = "NaoAtualizarEnderecoClienteQuandoEnderecoRecebidoEhNullo")]
        public void NaoAtualizarEnderecoClienteQuandoEnderecoRecebidoEhNullo()
        {
            var mockClienteRepository = new Mock<IClienteRepository>();
            var sut = new ClienteService(mockClienteRepository.Object, _mapper);
            var cliente = ListaDeClientes();
            EnderecoViewModel endereco = null;

            var novoClienteEndereco = sut.AtualizarEndereco(1, endereco);

            Assert.True(novoClienteEndereco == null);
        }

        private List<Cliente> ListaDeClientes() 
        {
            var lista = new List<Cliente>()
            {
                new Cliente()
                {
                    Id = 1,
                    NomeCompleto = "Eneas Martins",
                    Cpf = "148.959.617-88",
                    RendaMensal = 6000,
                    DataNascimento = new DateTime(1994, 12, 12),
                    DataCadastro = DateTime.Now,
                    Endereco = new Endereco()
                    {
                        Id = 1,
                        Cep = "20970-006",
                        Bairro = "Jacare",
                        Cidade = "Rio de Janeiro",
                        Logradouro = "Amaro Rangel",
                        Complemento = "Sobrado",
                        Uf = "RJ"
                    },
                    Vip = new Vip()
                },

                new Cliente()
                {
                    Id = 2,
                    NomeCompleto = "Debora Martins",
                    Cpf = "148.959.617-88",
                    RendaMensal = 3000,
                    DataNascimento = new DateTime(1985, 12, 12),
                    DataCadastro = DateTime.Now,
                    Endereco = new Endereco()
                    {
                        Id = 2,
                        Cep = "20970-006",
                        Bairro = "Jacare",
                        Cidade = "Rio de Janeiro",
                        Logradouro = "Amaro Rangel",
                        Complemento = "Sobrado",
                        Uf = "RJ"
                    },
                },

                new Cliente()
                {
                    Id = 3,
                    NomeCompleto = "Marcos Martins",
                    Cpf = "148.959.617-88",
                    RendaMensal = 9000,
                    DataNascimento = new DateTime(1975, 12, 12),
                    DataCadastro = DateTime.Now,
                    Endereco = new Endereco()
                    {
                        Id = 3,
                        Cep = "20970-006",
                        Bairro = "Jacare",
                        Cidade = "Rio de Janeiro",
                        Logradouro = "Amaro Rangel",
                        Complemento = "Sobrado",
                        Uf = "RJ"
                    },
                },

                new Cliente()
                {
                    Id = 4,
                    NomeCompleto = "Thiago Martins",
                    Cpf = "148.959.617-88",
                    RendaMensal = 7000,
                    DataNascimento = new DateTime(1991, 12, 12),
                    DataCadastro = DateTime.Now,
                    Endereco = new Endereco()
                    {
                        Id = 4,
                        Cep = "20970-006",
                        Bairro = "Jacare",
                        Cidade = "Rio de Janeiro",
                        Logradouro = "Amaro Rangel",
                        Complemento = "Sobrado",
                        Uf = "RJ"
                    },
                    Vip = new Vip()
                },

                new Cliente()
                {
                    Id = 5,
                    NomeCompleto = "Pandao Martins",
                    Cpf = "148.959.617-88",
                    RendaMensal = 7000,
                    DataNascimento = new DateTime(1998, 12, 12),
                    DataCadastro = DateTime.Now,
                    Endereco = new Endereco()
                    {
                        Id = 5,
                        Cep = "20970-006",
                        Bairro = "Jacare",
                        Cidade = "Rio de Janeiro",
                        Logradouro = "Amaro Rangel",
                        Complemento = "Sobrado",
                        Uf = "RJ"
                    },
                    Vip = new Vip()
                },

                new Cliente()
                {
                    Id = 6,
                    NomeCompleto = "Thamiris Silva",
                    Cpf = "148.959.617-88",
                    RendaMensal = 7000,
                    DataNascimento = new DateTime(1998, 12, 12),
                    DataCadastro = DateTime.Now,
                    Endereco = new Endereco()
                    {
                        Id = 6,
                        Cep = "20970-006",
                        Bairro = "Jacare",
                        Cidade = "Rio de Janeiro",
                        Logradouro = "Amaro Rangel",
                        Complemento = "Sobrado",
                        Uf = "RJ"
                    },
                    Vip = new Vip()
                },

                new Cliente()
                {
                    Id = 7,
                    NomeCompleto = "Adriana Silva",
                    Cpf = "148.959.617-88",
                    RendaMensal = 5000,
                    DataNascimento = new DateTime(1998, 12, 12),
                    DataCadastro = DateTime.Now,
                    Endereco = new Endereco()
                    {
                        Id = 7,
                        Cep = "20970-006",
                        Bairro = "Jacare",
                        Cidade = "Rio de Janeiro",
                        Logradouro = "Amaro Rangel",
                        Complemento = "Sobrado",
                        Uf = "RJ"
                    },
                },

                new Cliente()
                {
                    Id = 8,
                    NomeCompleto = "Miguel Souza",
                    Cpf = "148.959.617-88",
                    RendaMensal = 4000,
                    DataNascimento = new DateTime(1995, 12, 12),
                    DataCadastro = DateTime.Now,
                    Endereco = new Endereco()
                    {
                        Id = 8,
                        Cep = "20970-006",
                        Bairro = "Jacare",
                        Cidade = "Rio de Janeiro",
                        Logradouro = "Amaro Rangel",
                        Complemento = "Sobrado",
                        Uf = "RJ"
                    },
                },

                new Cliente()
                {
                    Id = 9,
                    NomeCompleto = "Maria Silva",
                    Cpf = "148.959.617-88",
                    RendaMensal = 500,
                    DataNascimento = new DateTime(1950, 12, 12),
                    DataCadastro = DateTime.Now,
                    Endereco = new Endereco()
                    {
                        Id = 9,
                        Cep = "20970-006",
                        Bairro = "Jacare",
                        Cidade = "Rio de Janeiro",
                        Logradouro = "Amaro Rangel",
                        Complemento = "Sobrado",
                        Uf = "RJ"
                    },
                },

                new Cliente()
                {
                    Id = 10,
                    NomeCompleto = "Robson Silva",
                    Cpf = "148.959.617-88",
                    RendaMensal = 15000,
                    DataNascimento = new DateTime(1993, 12, 12),
                    DataCadastro = DateTime.Now,
                    Endereco = new Endereco()
                    {
                        Id = 10,
                        Cep = "20970-006",
                        Bairro = "Jacare",
                        Cidade = "Rio de Janeiro",
                        Logradouro = "Amaro Rangel",
                        Complemento = "Sobrado",
                        Uf = "RJ"
                    },
                }
            };
            return lista;
        }

        private List<ClienteViewModel> ListaDeClientesViewModel()
        {
            var lista = new List<ClienteViewModel>()
            {
                new ClienteViewModel()
                {
                    Id = 1,
                    NomeCompleto = "Eneas Martins",
                    Cpf = "148.959.617-88",
                    Renda = 6000,
                    DataNascimento = new DateTime(1994, 12, 12),
                    DataCadastro = DateTime.Now
                },

                new ClienteViewModel()
                {
                    Id = 2,
                    NomeCompleto = "Debora Martins",
                    Cpf = "148.959.617-88",
                    Renda = 3000,
                    DataNascimento = new DateTime(1985, 12, 12),
                    DataCadastro = DateTime.Now
                },

                new ClienteViewModel()
                {
                    Id = 3,
                    NomeCompleto = "Marcos Martins",
                    Cpf = "148.959.617-88",
                    Renda = 9000,
                    DataNascimento = new DateTime(1975, 12, 12),
                    DataCadastro = DateTime.Now
                },

                new ClienteViewModel()
                {
                    Id = 4,
                    NomeCompleto = "Thiago Martins",
                    Cpf = "148.959.617-88",
                    Renda = 7000,
                    DataNascimento = new DateTime(1991, 12, 12),
                    DataCadastro = DateTime.Now
                },

                new ClienteViewModel()
                {
                    Id = 5,
                    NomeCompleto = "Pandao Martins",
                    Cpf = "148.959.617-88",
                    Renda = 7000,
                    DataNascimento = new DateTime(1998, 12, 12),
                    DataCadastro = DateTime.Now
                },

                new ClienteViewModel()
                {
                    Id = 6,
                    NomeCompleto = "Thamiris Silva",
                    Cpf = "148.959.617-88",
                    Renda = 7000,
                    DataNascimento = new DateTime(1998, 12, 12),
                    DataCadastro = DateTime.Now
                },

                new ClienteViewModel()
                {
                    Id = 7,
                    NomeCompleto = "Adriana Silva",
                    Cpf = "148.959.617-88",
                    Renda = 5000,
                    DataNascimento = new DateTime(1998, 12, 12),
                    DataCadastro = DateTime.Now
                },

                new ClienteViewModel()
                {
                    Id = 8,
                    NomeCompleto = "Miguel Souza",
                    Cpf = "148.959.617-88",
                    Renda = 4000,
                    DataNascimento = new DateTime(1995, 12, 12),
                    DataCadastro = DateTime.Now
                },

                new ClienteViewModel()
                {
                    Id = 9,
                    NomeCompleto = "Maria Silva",
                    Cpf = "148.959.617-88",
                    Renda = 500,
                    DataNascimento = new DateTime(1950, 12, 12),
                    DataCadastro = DateTime.Now
                },

                new ClienteViewModel()
                {
                    Id = 10,
                    NomeCompleto = "Robson Silva",
                    Cpf = "148.959.617-88",
                    Renda = 15000,
                    DataNascimento = new DateTime(1993, 12, 12),
                    DataCadastro = DateTime.Now
                }
            };
            return lista;
        }

        private List<EnderecoViewModel> ListaDeEnderecosViewModel()
        {
            var lista = new List<EnderecoViewModel>()
            {
                new EnderecoViewModel()
                {
                    Id = 1,
                    Cep = "20970-006",
                    Logradouro = "Amaro Rangel",
                    Bairro = "Jacare",
                    Complemento = "Sobrado",
                    Cidade = "Rio de Janeiro",
                    Uf = "RJ"
                },

                new EnderecoViewModel()
                {
                    Id = 2,
                    Cep = "20771-004",
                    Logradouro = "Dom Hélder Câmara",
                    Bairro = "Cachambi",
                    Complemento = "",
                    Cidade = "Rio de Janeiro",
                    Uf = "RJ"
                },
            };
            return lista;
        }
    }
}
