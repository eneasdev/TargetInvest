using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TargetInvest.Entities;

namespace TargetInvest.Repositories
{
    public interface IVipRepository
    {
        Vip BuscarVip(int id);
    }
}
