using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var cliente = _clienteService.BuscarCliente(id);

            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody] ClienteViewModel viewModel)
        {
            if (viewModel == null)
                return BadRequest();

            _clienteService.Cadastrar(viewModel);

            return CreatedAtAction(nameof(GetById), new { id = viewModel.Id }, viewModel);
        }
    }
}
