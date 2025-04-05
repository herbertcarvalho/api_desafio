using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Backend.Erp.Skeleton.Domain.Extensions
{
    public class InListAttribute(params int[] validValues) : ValidationAttribute
    {
        private readonly int[] _validValues = validValues;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is not int intValue)
                return new ValidationResult("Tipo inválido.");

            if (!_validValues.Contains(intValue))
                return new ValidationResult($"Os valores válidos são {string.Join(",", _validValues)}");

            return ValidationResult.Success;
        }
    }
}
