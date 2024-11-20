using System.ComponentModel.DataAnnotations;

namespace Catalogo.Validations
{
    public class ValidarAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value == null || String.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var primeiraLetra = value.ToString()[0].ToString();
            if (primeiraLetra != primeiraLetra.ToUpper())
                return new ValidationResult("The first character of the letter must be capitalized");

            return ValidationResult.Success;
        }
    }
}
