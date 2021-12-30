using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TargetInvest.Attributes;
using TargetInvest.Models;
using TargetInvest.Services;

namespace TargetInvest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VipsController : ControllerBase
    {
        private readonly IVipService _vipService;

        public VipsController(IVipService vipService)
        {
            _vipService = vipService;
        }

        [HttpGet("{Id}")]
        public IActionResult Get(int id)
        {
            var vipDetalhes = _vipService.BuscarVip(id);

            return Ok(vipDetalhes);
        }

        [HttpPost]
        public IActionResult Post([FromBody] VipRespostaViewModel vipRespostaViewModel)
        {
            var resposta = _vipService.VipResposta(vipRespostaViewModel);
            return Ok(resposta);
        }
    }
}
