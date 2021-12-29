using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TargetInvest.Attributes;
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

        [HttpGet]
        public IActionResult GetVipDetalhes()
        {
            var vipDetalhes = _vipService.ListarVips();

            return Ok(vipDetalhes);
        }

        [HttpPost]
        public IActionResult PostVipConfirmacao()
        {
            return Ok();
        }
    }
}
