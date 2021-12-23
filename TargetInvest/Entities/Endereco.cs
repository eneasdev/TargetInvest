namespace TargetInvest.Entities
{
    public class Endereco
    {
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Complemento { get; set; }
    }
}