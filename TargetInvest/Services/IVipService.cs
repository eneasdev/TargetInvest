﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TargetInvest.Models;

namespace TargetInvest.Services
{
    public interface IVipService
    {
        List<VipViewModel> ListarVips();
    }
}
