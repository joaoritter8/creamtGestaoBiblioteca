using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TesteBiblioteca.Models;

namespace SolucaoCrea.Models
{
    public class Movimentacao
    {
        [Key]
        public int MovimentacaoID { get; set; }

        [ForeignKey("Livro")]
        public int LivroID { get; set; }

        public virtual Livro? Livro { get; set; }

        [ForeignKey("Pessoa")]
        public int PessoaID { get; set; }

        public virtual Pessoa? Pessoa { get; set; }
        
        public int ProfID { get; set; }        

        public DateTime DataEmprestimo { get; set; }

        public DateTime? DataMaxima { get; set; }

        public DateTime? DataDevolucao { get; set; }

        public string MovimentacaoStatus { get; set; } = string.Empty;
    }

}
