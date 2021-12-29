using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TargetInvest.Entities
{
    public class Cliente
    {
        public Cliente()
        {
            DataCadastro = DateTime.Now;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string Cpf { get; set; }
        public double RendaMensal { get; set; }
        public Endereco Endereco { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataCadastro { get; set; }
        public Vip Vip { get; set; }

        public bool OferecerPlanoVip()
        {
            if (RendaMensal >= 6000)
                return true;
            return false;
        }
    }
}
