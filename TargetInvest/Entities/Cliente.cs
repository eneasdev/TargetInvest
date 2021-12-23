using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Key]
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string Cpf { get; set; }
        public double RendaMensal { get; set; }
        public Endereco Endereco { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Vip { get; set; } = false;

        public bool OferecerPlanoVip(Cliente cliente)
        {
            if (cliente.RendaMensal >= 6000)
                return true;
            return false;
        }

        public void AceitarPlano()
        {
            Vip = true;
        }
    }
}
