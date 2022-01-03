﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TargetInvest.Attributes;
using TargetInvest.Entities;
using TargetInvest.Models;
using TargetInvest.Services;

namespace TargetInvest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        
        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [ApiKey]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var cliente = _clienteService.BuscarCliente(id);

            if (cliente == null) return NotFound();

            return Ok(cliente);
        }

        [ApiKey]
        [HttpGet("indice-vips")]
        public IActionResult Get(IndiceVipsViewModel indiceVipsViewModel)
        {
            var endereco = _clienteService.BuscarClienteEndereco(id);

            if (endereco == null) return BadRequest();

            return Ok(endereco);
        }

        [ApiKey]
        [HttpGet("{id}/endereco")]
        public IActionResult Get(int id)
        {
            var endereco = _clienteService.BuscarClienteEndereco(id);

            if (endereco == null) return BadRequest();

            return Ok(endereco);
        }

        [ApiKey]
        [HttpGet("data-cadastro")]
        public IActionResult Get([FromBody] PesquisaDataCadastro dataCadastro)
        {
            var listaDataDeCadastro = _clienteService.ListarPorDataCadastro(dataCadastro.DataCadastroInicial, dataCadastro.DataCadastroFinal);
            return Ok(listaDataDeCadastro);
        }

        [ApiKey]
        [HttpGet("renda/{valor}")]
        public IActionResult Get(double Valor)
        {
            var listaPorRenda = _clienteService.ListarPorRenda(Valor);
            return Ok(listaPorRenda);
        }

        [HttpPost]
        public async Task<IActionResult> Post ([FromBody] ClienteCadastroViewModel clienteViewModel)
        {
            var enderecoViewModel = await InicilizeAPI(clienteViewModel.Cep);

            if (clienteViewModel == null) return BadRequest();

            var FinalizaCadastroViewModel = _clienteService.Cadastrar(clienteViewModel, enderecoViewModel);

            if (FinalizaCadastroViewModel == null) return BadRequest();

            return Ok(FinalizaCadastroViewModel);
        }

        [ApiKey]
        [HttpPut("{id}/endereco")]
        public IActionResult Put(int id, [FromBody] EnderecoViewModel enderecoViewModel)
        {
            _clienteService.AtualizarEndereco(id, enderecoViewModel);
            return Ok();
        }

        private async Task<EnderecoViewModel> InicilizeAPI(string cep)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://viacep.com.br/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage resp = await client.GetAsync("ws/" + cep + "/json/");

            var dados = await resp.Content.ReadAsStringAsync();

            var dadosAlterados = dados.Replace("localidade", "cidade");

            var endereco = JsonConvert.DeserializeObject<EnderecoViewModel>(dadosAlterados);

            return endereco;
        }
    }
}
