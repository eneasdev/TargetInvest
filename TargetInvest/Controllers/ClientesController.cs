using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
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
        HttpClient client = new HttpClient();

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        public async Task<EnderecoViewModel> InicilizeAPI(string cep)
        {
            client.BaseAddress = new Uri("https://viacep.com.br");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage resp = await client.GetAsync("ws/" + cep + "/json/");

            var dados = await resp.Content.ReadAsStringAsync();

            var endereco = JsonConvert.DeserializeObject<EnderecoViewModel>(dados);

            return endereco;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var cliente = _clienteService.BuscarCliente(id);

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Post ([FromBody] ClienteViewModel viewModel)
        {
            viewModel.Endereco = await InicilizeAPI(viewModel.Endereco.Cep);
                        
            if (viewModel == null) return BadRequest();

            var isNull = _clienteService.Cadastrar(viewModel);

            if (isNull == null) return BadRequest();

            return Ok(viewModel);
        }

    }
}
