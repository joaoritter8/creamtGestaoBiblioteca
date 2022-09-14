using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TesteBiblioteca.Models
{
    public class Livro
    {
        [Key]
        public int LivroID { get; set; }

        public string LivroISBN { get; set; } = string.Empty;

        public string LivroTitulo { get; set; } = string.Empty;

        [ForeignKey("Pessoa")]
        public int PessoaID { get; set; }

        public virtual Pessoa? Autor { get; set; }

        public string LivroEditora { get; set; } = string.Empty;

        public string LivroEdicao { get; set; } = string.Empty;

        public DateTime? LivroAno { get; set; }

        [ForeignKey("Categoria")]
        public int CatID { get; set; }

        public virtual Categoria? Categoria { get; set; }

        [MaxLength(2)]
        public string LivroStatus { get; set; } = string.Empty;
    }
}
