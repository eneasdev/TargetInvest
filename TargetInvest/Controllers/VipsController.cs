using Microsoft.AspNetCore.Mvc;
using TargetInvest.Application.Interfaces;
using TargetInvest.Application.Models.ViewModels;

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
