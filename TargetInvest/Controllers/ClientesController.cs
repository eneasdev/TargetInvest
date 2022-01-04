using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TargetInvest.Application.Attributes;
using TargetInvest.Application.Interfaces;
using TargetInvest.Application.Models.InputModels;
using TargetInvest.Application.Models.ViewModels;

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
        [HttpGet("indice-vips")]
        public IActionResult GetIndiceVips()
        {
            var indiceVips = _clienteService.IndiceVip();

            return Ok(indiceVips);
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
        [HttpGet("{id}/endereco")]
        public IActionResult GetEndereco(int id)
        {
            var endereco = _clienteService.BuscarClienteEndereco(id);

            if (endereco == null) return BadRequest();

            return Ok(endereco);
        }

        [ApiKey]
        [HttpGet("renda/{valor}")]
        public IActionResult GetRenda(double Valor)
        {
            var listaPorRenda = _clienteService.ListarPorRenda(Valor);
            return Ok(listaPorRenda);
        }

        [ApiKey]
        [HttpGet("data-cadastro")]
        public IActionResult GetDataCadastro([FromBody] PesquisaDataCadastroViewModel dataCadastro)
        {
            var listaDataDeCadastro = _clienteService.ListarPorDataCadastro(dataCadastro.DataCadastroInicial, dataCadastro.DataCadastroFinal);
            return Ok(listaDataDeCadastro);
        }

        [HttpPost]
        public async Task<IActionResult> Post ([FromBody] NovoClienteInputModel clienteViewModel)
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
