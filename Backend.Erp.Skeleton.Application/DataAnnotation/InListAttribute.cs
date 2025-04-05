using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Backend.Erp.Skeleton.Application.DataAnnotation
{
    public class InListAttribute : ValidationAttribute
    {
        private readonly int[] _validValues;

        public InListAttribute(params int[] validValues)
        {
            _validValues = validValues;
        }

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
