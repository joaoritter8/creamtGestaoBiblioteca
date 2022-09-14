using System.ComponentModel.DataAnnotations;

namespace TesteBiblioteca.Models
{
    public class Categoria
    {
        [Key]
        public int CatID { get; set; }

        public string CatNome { get; set; } = string.Empty;

        public bool CatStatus { get; set; }
    }
}
