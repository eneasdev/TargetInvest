using System.ComponentModel.DataAnnotations.Schema;

namespace TargetInvest.Domain.Entities
{
    public class Endereco
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Complemento { get; set; }

        public void Update(Endereco endereco)
        {
            Cep = endereco.Cep;
            Logradouro = endereco.Logradouro;
            Bairro = endereco.Bairro;
            Cidade = endereco.Cidade;
            Uf = endereco.Uf;
            Complemento = endereco.Complemento;
        }
    }
}