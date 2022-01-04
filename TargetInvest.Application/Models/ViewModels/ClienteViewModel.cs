using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TargetInvest.Application.Models.ViewModels
{
    public class ClienteViewModel
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string Cpf { get; set; }
        public double Renda { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
