using System.ComponentModel.DataAnnotations;

namespace TesteBiblioteca.Models
{
    public class Pessoa
    {
        [Key]
        public int PessoaID { get; set; }

        public string PessoaNome { get; set; } = string.Empty;

        public string? PessoaCPF { get; set; }

        public string? PessoaRG { get; set; }

        public string? PessoaEndereco { get; set; }

        public string? PessoaTelefone { get; set; }

        public string? PessoaEmail { get; set; }

        [MaxLength(2)]
        public string PessoaTipo { get; set; } = string.Empty;

        public bool PessoaStatus { get; set; }
    }
}
