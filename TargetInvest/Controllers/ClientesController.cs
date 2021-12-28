using Microsoft.AspNetCore.Http;
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
    [ApiKey]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        
        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var cliente = _clienteService.BuscarCliente(id);

            if (cliente == null) return NotFound();

            return Ok(cliente);
        }

        //[HttpGet]
        //public IActionResult GetVipDetalhes()
        //{
        //    return Ok();
        //}
        //
        //[HttpPost]
        //public IActionResult PostVipConfirmacao()
        //{
        //    return Ok();
        //}

        [HttpPost]
        public async Task<IActionResult> Post ([FromBody] ClienteViewModel clienteViewModel)
        {
            var enderecoViewModel = await InicilizeAPI(clienteViewModel.Cep);

            if (clienteViewModel == null) return BadRequest();

            var FinalizaCadastroViewModel = _clienteService.Cadastrar(clienteViewModel, enderecoViewModel);

            if (FinalizaCadastroViewModel == null) return BadRequest();

            return Ok(FinalizaCadastroViewModel);
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
