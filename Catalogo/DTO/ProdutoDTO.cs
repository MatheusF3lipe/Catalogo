using Catalogo.Models;
using Catalogo.Validations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Catalogo.DTO
{
    public class ProdutoDTO
    {
        public int ProdutoId { get; set; }
        [Required]
        [StringLength(300)]
        public string? Nome { get; set; }
        [Required]
        [StringLength(300)]
        public string? Descricao { get; set; }
        [Required]
        public decimal Preco { get; set; }
        [Required]
        [StringLength(300, ErrorMessage = "Quantity above permitted ")]
        public string? ImagemUrl { get; set; }
        [Range(1, 1000, ErrorMessage = "The value inserted is not avaliable in range")]
        public int CategoriaId { get; set; }
    }
}
