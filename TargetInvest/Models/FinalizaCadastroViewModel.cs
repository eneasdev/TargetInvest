using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TargetInvest.Models
{
    public class FinalizaCadastroViewModel
    {
        public bool OferecerPlanoVip { get; set; } = false;
        public bool Cadastrado { get; set; } = false;
    }
}
