using System.ComponentModel.DataAnnotations;

namespace Catalogo.DTO
{
    public class ProdutoDtoUpdateRequest : IValidatableObject
    {
        [Required]
        [Range(1, 999, ErrorMessage = "Product stock has an initial limit of 1 to 999")]
        public int Estoque {  get; set; }
        public DateTime DataCadastro { get; set; }
        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            if (DataCadastro <= DateTime.Now)
            {
                yield return new ValidationResult("The date must be greater than the current date", new[] {nameof(this.DataCadastro)});
            }
        }
    }
}
