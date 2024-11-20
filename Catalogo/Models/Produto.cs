namespace Catalogo.Models;
using Catalogo.Validations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

[Table("Produtos")]
public class Produto : IValidatableObject
{
    [Key]
    public int ProdutoId { get; set; }
    [ValidarAttribute]
    public string? Nome { get; set; }
    [Required]
    [StringLength(300)]
    public string? Descricao {  get; set; }
    [Required]
    [Column(TypeName="decimal(10,2)")]
    public decimal Preco {  get; set; }
    [Required]
    [StringLength(300,ErrorMessage = "Quantity above permitted ")]
    public string? ImagemUrl { get; set; }
    [Range(1,1000,ErrorMessage = "The value inserted is not avaliable in range")]
    public float Estoque {  get; set; }
    public DateTime Cadastro { get; set; }
    public int CategoriaId {  get; set; }
    [JsonIgnore]
    public Categoria? Categoria { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if(!string.IsNullOrEmpty(this.Nome))
        {
            var primeiraLetra = this.Nome[0].ToString();
            if (primeiraLetra != primeiraLetra.ToUpper())
                yield return new ValidationResult("The fisrt letter most be capitalized", new[] { nameof(this.Nome) });
        }
        if(this.Estoque <= 0)
        {
            yield return new ValidationResult("The length cannot be less than zero ", new[] {nameof(this.Estoque)});
        }
    }
}
