using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TargetInvest.Models
{
    public class ClienteCadastroViewModel
    {
        [Required(ErrorMessage = "Nome completo deve ser informado.")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Nome deve ter no mínimo 10 e no máximo 50 caracteres.")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "Por favor precisamos que informe um CPF para finalizar o cadastro.")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Por favor precisamos que informe uma renda para finalizar o cadastro.")]
        //[DisplayFormat(DataFormatString = "{0:C}")]
        public double RendaMensal { get; set; }

        [Required(ErrorMessage = "Por favor insira uma data de nascimento.")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{d:0}")]
        public DateTime DataNascimento { get; set; }

        [BindProperty]
        public bool Cadastrado { get; set; } = false;

        public EnderecoViewModel Endereco { get; set; }
    }
}
