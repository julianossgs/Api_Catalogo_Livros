using System.ComponentModel.DataAnnotations;

namespace Api_Catalogo_Livros.Validation
{
    public class UpperCaseAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,
           ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var primeiraLetra = value.ToString()[0].ToString();
            if (primeiraLetra != primeiraLetra.ToUpper())
            {
                return new ValidationResult("Erro! A primeira letra do nome deve ser maiúscula!");
            }

            return ValidationResult.Success;
        }
    }
}
